using System;
using System.Collections;
using Core.Enum;
using Core.Tools;using Cinemachine;
using Core.Animations;
using Core.Movement.Controller;
using Core.Movement.Data;
using StatsSystem;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimatorController _playerAnimator;

        [Header("HorizontalMovement")]
        [SerializeField] private DirectionalMoverData _directionalMoverData;

        [Header("Jump")] 
        [SerializeField] private JumpData _jumpData;
        
        [Header("Camera")]
        [SerializeField] private DirectionalCameraPair _directionalCameras;
        [SerializeField] private CinemachineVirtualCamera _startCamera;
        [SerializeField] private CinemachineVirtualCamera _endCamera;
        
        private Rigidbody2D _playerRb;
        private CapsuleCollider2D _playerCollider2D;

        private DirectionalMover _directionalMover;
        private Jumper _jumper;
      
        public void Initialize(IStatValueGiver statValueGiver)
        {
            _playerRb = GetComponent<Rigidbody2D>();
            _playerCollider2D = GetComponent<CapsuleCollider2D>();
            
            _directionalMover = new DirectionalMover(_playerRb,_directionalMoverData,statValueGiver);
            _jumper = new Jumper(_playerRb, _playerCollider2D, _jumpData,statValueGiver);
            //ShowLevel();
        }

        private void Update()
        {
            UpdateAnimations();
            UpdateCameras();
        }
        
        public void Jump() => _jumper.Jump();
        public void MoveHorizontaly(float direction) => _directionalMover.MoveHorizontaly(direction);
        private void EndJump() => _jumper.EndJump();
        
        private void UpdateAnimations()
        {
            _playerAnimator.PlayAnimation(AnimationType.Idle,true);
            _playerAnimator.PlayAnimation(AnimationType.Run,_directionalMover.IsMoving);
            _playerAnimator.PlayAnimation(AnimationType.Jump,_jumper.IsJumping);
        }

        private void UpdateCameras()
        {
            foreach (var cameraPair in _directionalCameras.DirectionalCameras)
            {
                cameraPair.Value.enabled = cameraPair.Key == _directionalMover.Direction;
            }
        }

        private void ShowLevel()
        {
            StartCoroutine(ShowLevelTimer());
        }

        private void SwitchShowLevelCameras()
        {
            _endCamera.enabled =!_endCamera.enabled ;
            _startCamera.enabled =!_startCamera.enabled;
        }
        
        private IEnumerator ShowLevelTimer()
        {
            yield return new WaitForSecondsRealtime(1);
            SwitchShowLevelCameras();
            yield return new WaitForSecondsRealtime(5);
            SwitchShowLevelCameras();
            yield return new WaitForSecondsRealtime(5);
            _startCamera.enabled = false;
        }
    }
}
