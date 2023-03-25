using Core.Enum;
using Core.Movement.Data;
using StatsSystem;
using StatsSystem.Enum;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Movement.Controller
{
    public class DirectionalMover
    {
        private readonly Rigidbody2D _playerRb;
        private readonly Transform _transform;
        private readonly DirectionalMoverData _directionalMoverData;
        private readonly IStatValueGiver _statValueGiver;

        private float _movement;
        public Direction Direction { get; private set; }
        public bool IsMoving => _movement != 0;
        
        public DirectionalMover(Rigidbody2D playerRb , DirectionalMoverData directionalMoverData,IStatValueGiver statValueGiver)
        {
            _playerRb = playerRb;
            _transform = _playerRb.transform;
            _directionalMoverData = directionalMoverData;
            _statValueGiver = statValueGiver;
        }
        public void MoveHorizontaly(float direction)
        {
            _movement = direction;
            SetDirection(direction);
            Vector2 velocity = _playerRb.velocity;
            velocity.x = direction * _statValueGiver.GetStatValue(StatType.Speed);
            _playerRb.velocity = velocity;
        }
        
        private void SetDirection(float direction)
        {
            if ((Direction == Direction.Right && direction < 0) ||
                (Direction == Direction.Left && direction > 0))
            {
                Flip();
            }
        }
        
        private void Flip()
        {
            _transform.Rotate(0,180,0);
            Direction = Direction == Direction.Right ? Direction.Left : Direction.Right;
        }
    }
}