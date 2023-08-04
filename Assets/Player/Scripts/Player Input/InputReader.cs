using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

namespace PlayerSettings
{
    [CreateAssetMenu(fileName = "Input Reader")]
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

                SetActionMap("PlayerDefault");
                
            }
        }

        private void SetActionMap(string newInputActionMap)
        {
            foreach (InputActionMap inputActionMap in _playerController.asset.actionMaps)
            {
                if (inputActionMap.name.Equals(newInputActionMap))
                {
                    Debug.Log(inputActionMap.name + " is Active");
                    inputActionMap.Enable();
                }
                else
                {
                    Debug.Log(inputActionMap.name + " is Inactive");
                    inputActionMap.Disable();
                }
            }
        }

        #region Player Event Actions

        //Directional Movement
        public event Action<Vector2> MoveEvent;

        //Player Action Buttons
        public event Action JumpEvent;
        public event Action PrimaryFireEvent;
        public event Action AlternateFireEvent;

        //Mouse Look
        public event Action<float> LookXEvent;
        public event Action<float> LookYEvent;

        //UI
        public event Action PauseEvent;
        public event Action ResumeEvent;
        

        #endregion

        #region Default Mapping

        public void OnMove(InputAction.CallbackContext context) => MoveEvent?.Invoke(context.ReadValue<Vector2>());

        public void OnLookY(InputAction.CallbackContext context) => LookYEvent?.Invoke(context.ReadValue<float>());

        public void OnLookX(InputAction.CallbackContext context) => LookXEvent?.Invoke(context.ReadValue<float>());

        public void OnJump(InputAction.CallbackContext context)
        {

            if (context.phase == InputActionPhase.Performed)
            {
                Debug.Log("is Jumping");
                JumpEvent?.Invoke();
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

        public void OnSwapWeapon(InputAction.CallbackContext context)
        {

        }

        #endregion


        #region UI Mapping

        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                ResumeEvent?.Invoke();
                SetActionMap("PlayerDefault");
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
                SetActionMap("UI");
            }
        }


        #endregion

    }
}
