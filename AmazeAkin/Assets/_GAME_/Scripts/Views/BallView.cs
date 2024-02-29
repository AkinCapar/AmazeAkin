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
        private bool _pathIsCompleted;
        
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
            _signalBus.Subscribe<PathTilesCompletedSignal>(OnPathTilesCompletedSignal);

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

        private void OnPathTilesCompletedSignal()
        {
            _pathIsCompleted = true;
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
                .OnComplete(() =>
                {
                    _ballIsMoving = false;
                    LevelCompletedAnimation();
                });
        }

        private void LevelCompletedAnimation()
        {
            if (_pathIsCompleted)
            {
                _ballIsMoving = true;
                transform.parent = null;
                transform.DOMove(new Vector3(0, 0, - 5), 1f)
                    .OnComplete(() =>
                {
                    transform.DOMoveZ(-9.2f, .5f).OnComplete((() =>
                    {
                        _levelController.CompleteLevel();
                    }));
                });
            }
        }

        public void NewLevelAnimation(Transform startPos)
        {
            transform.DOMoveZ(-5, .5f)
                .OnComplete((() =>
                {
                    gameObject.transform.DOMove(startPos.position, 1f)
                        .OnComplete(() =>
                        {
                            _ballIsMoving = false;
                            _pathIsCompleted = false;
                        });
                    gameObject.transform.parent = startPos;
                    gameObject.transform.localEulerAngles = new Vector3(-90, 0, 0);
                }));
        }

        public void Dispose() 
        {
            _signalBus.Unsubscribe<InputReceivedSignal>(OnInputRecievedSignal);
        }

        public class Factory : PlaceholderFactory<Object, BallView>
        {
        }
    }
}