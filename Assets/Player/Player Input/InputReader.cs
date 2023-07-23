using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

namespace PlayerSettings
{
    [CreateAssetMenu(fileName = "InputReader")]
    public class InputReader : ScriptableObject, PlayerController.IPlayerDefaultActions, PlayerController.IUIActions
    {
        private PlayerController _playerController;

        private void OnEnable()
        {
            if (_playerController == null)
            {
                _playerController = new PlayerController();
                
                _playerController.PlayerDefault.SetCallbacks(this);
                _playerController.UI.SetCallbacks(this);
                
                SetGameplay();
            }
        }

        public void SetGameplay()
        {
            _playerController.PlayerDefault.Enable();
            _playerController.UI.Disable();
        }
        
        public void SetUI()
        {
            _playerController.PlayerDefault.Disable();
            _playerController.UI.Enable();
        }

        #region Player Actions
        
            //Directional Movement
            public event Action<Vector2> MoveEvent;
            
            //Player Action Buttons
            public event Action JumpEvent;
            public event Action JumpCancelledEvent; 

            public event Action PrimaryFireEvent;

            public event Action AlternateFireEvent; 
            
            //UI
            public event Action PauseAction;
            public event Action ResumeAction; 
            
        #endregion
        
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                JumpEvent?.Invoke();
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                JumpCancelledEvent?.Invoke();
            }
        }
        
        public void OnPrimaryFire(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PrimaryFireEvent?.Invoke();
            }
        }

        public void OnAlternateFire(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                AlternateFireEvent?.Invoke();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseAction?.Invoke();
                SetUI();
            }
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                ResumeAction?.Invoke();
                SetGameplay();
            }
        }
    }
    
}