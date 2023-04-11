using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Updater;
using InputReader;

namespace Player
{
    public class PlayerBrain : IDisposable
    {
        private readonly PlayerEntity _playerEntity;
        private readonly List<IEntityInputSource> _inputSources;
        
        public PlayerBrain(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _inputSources = inputSources;
            ProjectUpdater.Instance.FixedUpdateCalled += OnFixedUpdate;
        }
        public void Dispose() => ProjectUpdater.Instance.FixedUpdateCalled -= OnFixedUpdate;
        
        private void OnFixedUpdate()
        {
            _playerEntity.MoveHorizontaly(GetHorizontalDirection());
            if(isJump)
                _playerEntity.Jump();

            foreach (var input in _inputSources)
            {
                input.ResetOneTimeActions();
            }
        }

        private float GetHorizontalDirection()
        {
            foreach (var input in _inputSources)
            {
                if(input.Direction == 0)
                    continue;

                return input.Direction;
            }

            return 0;
        }

        private bool isJump => _inputSources.Any(source => source.Jump);
    }
}