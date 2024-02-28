using System;
using Amaze.Controllers;
using Amaze.Settings;
using Amaze.Utilities;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using DG.Tweening;

namespace Amaze.Views
{
    public class BallView : MonoBehaviour
    {
        private bool _ballIsMoving;
        
        #region Injection

        private SignalBus _signalBus;
        private LevelSettings _levelSettings;
        private LevelController _levelController;

        [Inject]
        private void Construct(SignalBus signalBus
            , LevelSettings levelSettings
            , LevelController levelController)
        {
            _signalBus = signalBus;
            _levelSettings = levelSettings;
            _levelController = levelController;
        }

        #endregion

        public void Initialize(Transform startPos)
        {
            _signalBus.Subscribe<InputReceivedSignal>(OnInputRecievedSignal);

            gameObject.transform.position = startPos.position;
            gameObject.transform.parent = startPos;
            gameObject.transform.localEulerAngles = new Vector3(-90, 0, 0);
        }

        private void Update()
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 100, Color.yellow);
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * 100, Color.yellow);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 100, Color.yellow);
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.right) * 100, Color.yellow);
        }

        private void OnInputRecievedSignal(InputReceivedSignal signal)
        {
            if (!_ballIsMoving)
            { 
                CalculateAndMove(signal.Direction);
            }
        }

        private Vector3 CalculateDestination(Vector3 direction)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag(Constants.Tags.BorderTag))
                {
                    return hit.collider.gameObject.transform.position;
                }
            }
            
            return transform.position;
        }

        private void CalculateAndMove(InputDirections direction)
        {
            Vector3 destination;
            switch (direction)
            {
                case InputDirections.Up:
                    destination = CalculateDestination(Vector3.up);
                    if ((destination - transform.position).magnitude > 1)
                    {
                        Move(destination, -Vector3.forward);
                    }
                    break;
                case InputDirections.Down:
                    destination = CalculateDestination(-Vector3.up);
                    if ((destination - transform.position).magnitude > 1)
                    {
                        Move(destination, Vector3.forward);
                    }
                    break;
                case InputDirections.Right:
                    destination = CalculateDestination(Vector3.right);
                    if ((destination - transform.position).magnitude > 1)
                    {
                        Move(destination, Vector3.right);
                    }
                    break;
                
                case InputDirections.Left:
                    destination = CalculateDestination(-Vector3.right);
                    if ((destination - transform.position).magnitude > 1)
                    {
                        Move(destination, -Vector3.right);
                    }
                    break;
            }
        }

        private void Move(Vector3 destination, Vector3 direction)
        {
            _ballIsMoving = true;
            int moveAmount = Mathf.RoundToInt((destination - transform.position).magnitude);
            Vector3 finalDestination = transform.localPosition + (moveAmount - 1) * direction;
            transform.DOLocalMove(finalDestination, moveAmount / _levelSettings.ballMoveSpeed)
                .OnComplete(() => { _ballIsMoving = false;});
        }

        public void Dispose() //TODO Level fail olduğunda dispose etmeyi unutma.
        {
            _signalBus.Unsubscribe<InputReceivedSignal>(OnInputRecievedSignal);
        }

        public class Factory : PlaceholderFactory<Object, BallView>
        {
        }
    }
}