using System;
using UnityEngine;
using UnityEngine.UI;

namespace InputReader
{
    public class GameUIInputView : MonoBehaviour,IEntityInputSource
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Button _jumpButton;
        
        public float Direction => Mathf.Abs(_joystick.Horizontal) >= 0.5 ? Mathf.Sign(_joystick.Horizontal) : 0;
        
        public bool Jump { get; private set; }
        
        private void Awake()
        {
            _jumpButton.onClick.AddListener(() => Jump = true);
        }

        private void OnDestroy()
        {
            _jumpButton.onClick.RemoveAllListeners();
        }
        
        public void ResetOneTimeActions()
        {
            Jump = false;
        }
    }
}