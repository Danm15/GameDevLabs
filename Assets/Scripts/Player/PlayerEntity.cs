using System;
using Core.Enum;
using Core.Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private Direction _direction;
        
        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private LayerMask _groundLayerMask;
        
        [Header("Camera")]
        [SerializeField] private DirectionalCameraPair _directionalCameras;

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
            if ((_direction == Direction.Right && direction < 0) || (_direction == Direction.Left && direction > 0))
            {
                Flip();
            }
        }
        private void Flip()
        {
            transform.Rotate(0,180,0);
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
            CameraChange();
        }
        
        private bool IsOnGroundCheck()
        {
           RaycastHit2D groundRayHit =  Physics2D.Raycast(_playerCollider2D.bounds.center, Vector2.down,_raycastDistance,_groundLayerMask);
           return groundRayHit.collider != null;
        }

        private void CameraChange()
        {
            foreach (var cameraPair in _directionalCameras.DirectionalCameras)
            {
                cameraPair.Value.enabled = cameraPair.Key == _direction;
            }
        }
        
    }
}
