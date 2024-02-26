using System;
using System.Collections;
using System.Collections.Generic;
using Amaze.Settings;
using UnityEngine;
using Zenject;

namespace Amaze.Controllers
{
    public class InputController: ITickable
    {
        private Vector3 _mouseButtonDownPos;
        private Vector3 _mouseButtonUpPos;
        
        #region Injection

        private LevelSettings _levelSettings;

        public InputController(LevelSettings levelSettings)
        {
            _levelSettings = levelSettings;
        }

        #endregion
        
        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseButtonDownPos = Input.mousePosition;
            }

            if ((Input.mousePosition - _mouseButtonDownPos).magnitude > _levelSettings.inputSensitivity)
            {
                Debug.Log("INPUT DETECTED" + " " + (Input.mousePosition - _mouseButtonDownPos).magnitude);
            }
        }
    }
}
