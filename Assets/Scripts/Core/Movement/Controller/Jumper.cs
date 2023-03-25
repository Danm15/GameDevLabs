using Core.Movement.Data;
using StatsSystem;
using StatsSystem.Enum;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class Jumper
    {
        private readonly Rigidbody2D _playerRb;
        private readonly JumpData _jumpData;
        private readonly CapsuleCollider2D _playerCollider2D;
        private readonly IStatValueGiver _statValueGiver;
        
        public bool IsJumping { get; private set; }
        
        public Jumper(Rigidbody2D playerRb ,CapsuleCollider2D playerCollider2D, JumpData jumpData,IStatValueGiver statValueGiver)
        {
            _playerRb = playerRb;
            _playerCollider2D = playerCollider2D;
            _jumpData = jumpData;
            _statValueGiver = statValueGiver;
        }
        
        public void Jump()
        {
            if (IsOnGroundCheck())
            {
                IsJumping = true;
                _playerRb.AddForce(_statValueGiver.GetStatValue(StatType.JumpForce)* Vector2.up,ForceMode2D.Impulse);
            }
        }
        public void EndJump()
        {
            IsJumping = false;
        }
        
        private bool IsOnGroundCheck()
        {
            RaycastHit2D groundRayHit =  Physics2D.Raycast(_playerCollider2D.bounds.center, Vector2.down,_jumpData.RaycastDistance,_jumpData.GroundLayerMask);
            return groundRayHit.collider != null;
        }
    }
}