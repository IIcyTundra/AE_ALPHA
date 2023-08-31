using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSettings
{
    [CreateAssetMenu(fileName = "Input Reader")]
    public class InputReader : ScriptableObject, PlayerController.IPlayerDefaultActions, PlayerController.IPauseMenuActions
    {
        private PlayerController _playerController;
        private void OnEnable()
        {
            if (_playerController == null)
            {
                _playerController = new PlayerController();

                _playerController.PlayerDefault.SetCallbacks(this);
                _playerController.PauseMenu.SetCallbacks(this);

                SetPlayerMap();
                
            }
        }

        private void SetPlayerMap()
        {
            _playerController.PlayerDefault.Enable();
            _playerController.PauseMenu.Disable();

        }

        private void SetUIMap()
        {
            _playerController.PlayerDefault.Disable();
            _playerController.PauseMenu.Enable();
        }

        #region Player Event Actions

        //Directional Movement
        public event Action<Vector2> MoveEvent;

        //Player Action Buttons
        public event Action JumpEvent;
        public event Action <float> PrimaryFireStartedEvent;
        public event Action PrimaryFireEndedEvent;
        public event Action AlternateFireEvent;
        public event Action SwapWeaponFireEvent;

        //Mouse Look
        public event Action<float> LookXEvent;
        public event Action<float> LookYEvent;

        //PauseMenu
        public event Action PauseEvent;
        public event Action CancelEvent;
        

        #endregion

        #region Default Mapping

        public void OnMove(InputAction.CallbackContext context) => MoveEvent?.Invoke(context.ReadValue<Vector2>());

        public void OnLookY(InputAction.CallbackContext context) => LookYEvent?.Invoke(context.ReadValue<float>());

        public void OnLookX(InputAction.CallbackContext context) => LookXEvent?.Invoke(context.ReadValue<float>());

        public void OnJump(InputAction.CallbackContext context)
        {

            if (context.phase == InputActionPhase.Performed)
            {
                //Debug.Log("is Jumping");
                JumpEvent?.Invoke();
            }

        }

        public void OnPrimaryFire(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
                PrimaryFireStartedEvent?.Invoke(context.ReadValue<float>());
            else if (context.phase == InputActionPhase.Canceled)
                PrimaryFireEndedEvent?.Invoke();

        }

        public void OnAlternateFire(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                AlternateFireEvent?.Invoke();
            }
        }

        public void OnSwapWeapon(InputAction.CallbackContext context)
        {
            //if (context.phase == InputActionPhase.Performed)
            //{
            //    AlternateFireEvent?.Invoke();
            //}
        }

        public void OnSwapWeaponFire(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                SwapWeaponFireEvent?.Invoke();
            }
        }


        #endregion


        #region PauseMenu Mapping

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                CancelEvent?.Invoke();
                SetPlayerMap();
            }
        }


        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
                SetUIMap();
            }
        }

        #endregion

    }
}
