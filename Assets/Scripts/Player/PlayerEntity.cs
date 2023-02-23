using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        
        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;
        
        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private LayerMask _groundLayerMask;
        
        private Rigidbody2D _playerRb;
        private CapsuleCollider2D _playerCollider2D;
        private float _raycastDistance = 1.49f;
        
        private void Start()
        {
            _playerRb = GetComponent<Rigidbody2D>();
            _playerCollider2D = GetComponent<CapsuleCollider2D>();
        }
        
        public void Jump()
        {
            if (IsOnGroundCheck()) 
                _playerRb.AddForce(_jumpForce * Vector2.up,ForceMode2D.Impulse);
        }
        public void MoveHorizontaly(float direction)
        {
            SetDirection(direction);
            Vector2 velocity = _playerRb.velocity;
            velocity.x = direction * _horizontalSpeed;
            _playerRb.velocity = velocity;
        }
        private void SetDirection(float direction)
        {
            if ((_faceRight && direction < 0) || (!_faceRight && direction > 0))
            {
                Flip();
            }
        }
        private void Flip()
        {
            transform.Rotate(0,180,0);
            _faceRight = !_faceRight;
        }
        
        private bool IsOnGroundCheck()
        {
           RaycastHit2D groundRayHit =  Physics2D.Raycast(_playerCollider2D.bounds.center, Vector2.down,_raycastDistance,_groundLayerMask);
           return groundRayHit.collider != null;
        }
        
    }
}
