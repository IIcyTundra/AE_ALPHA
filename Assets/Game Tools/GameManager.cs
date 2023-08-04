using System.Collections;
using System.Collections.Generic;
using PlayerSettings;
using UnityEngine;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private GameObject PauseMenu;
        private void Start()
        {
            _inputReader.PauseEvent += HandlePause;
            _inputReader.ResumeEvent += HandleResume;
        }

        private void HandlePause()
        {
            PauseMenu.SetActive(true);
        }

        private void HandleResume()
        {
            PauseMenu.SetActive(false);
        }
    }
}