using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private GameUIInputView _gameUIInput;
        
        private PlayerBrain _playerBrain;
        private ExternalDevicesInputReader _externalDevicesInput;

        private void Awake()
        {
            _externalDevicesInput = new ExternalDevicesInputReader();
            _playerBrain = new PlayerBrain(_playerEntity, new List<IEntityInputSource> 
            { 
                _externalDevicesInput,
                _gameUIInput
            });   
        }

        private void Update()
        {
            _externalDevicesInput.OnUpdate();
        }
        
        private void FixedUpdate()
        {
            _playerBrain.OnFixedUpdate();
        }
    }
}