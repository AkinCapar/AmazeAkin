using System;
using System.Collections;
using System.Collections.Generic;
using Amaze.Settings;
using Amaze.Utilities;
using UnityEngine;
using Zenject;

namespace Amaze.Controllers
{
    public class InputController : ITickable
    {
        private Vector3 _mouseButtonDownPos;
        private Vector3 _mouseButtonUpPos;
        private bool _calculateInput;

        #region Injection

        private LevelSettings _levelSettings;
        private SignalBus _signalBus;

        public InputController(LevelSettings levelSettings
            , SignalBus signalBus)
        {
            _levelSettings = levelSettings;
            _signalBus = signalBus;
        }

        #endregion

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _calculateInput = true;
                _mouseButtonDownPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _calculateInput = false;
            }

            CalculateInput();
        }

        private void CalculateInput()
        {
            if (!_calculateInput)
            {
                return;
            }

            Vector3 mouseMovement = Input.mousePosition - _mouseButtonDownPos;

            if (mouseMovement.magnitude > _levelSettings.inputSensitivity)
            {
                if (Mathf.Abs(mouseMovement.x) > Mathf.Abs(mouseMovement.y))
                {
                    _signalBus.Fire(new InputReceivedSignal((mouseMovement.x > 0) ? InputDirections.Right : InputDirections.Left));
                }

                else
                {
                    _signalBus.Fire(new InputReceivedSignal((mouseMovement.y > 0) ? InputDirections.Up : InputDirections.Down));
                }

                _calculateInput = false;
            }
        }
    }
}