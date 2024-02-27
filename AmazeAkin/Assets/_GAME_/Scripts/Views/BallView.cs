using System;
using System.Collections;
using System.Collections.Generic;
using Amaze.Utilities;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Amaze.Views
{
    public class BallView : MonoBehaviour
    {
        #region Injection

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
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
            Move(signal.Direction);
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

        private void Move(InputDirections direction)
        {
            Vector3 destination;
            switch (direction)
            {
                case InputDirections.Up:
                    destination = CalculateDestination(Vector3.up);
                    if ((destination - transform.position).magnitude > 1)
                    {
                        Debug.Log("hmm: " + (destination - transform.position).magnitude);
                        transform.position = destination - Vector3.up;
                    }
                    break;
                case InputDirections.Down:
                    destination = CalculateDestination(-Vector3.up);
                    if ((destination - transform.position).magnitude > 1)
                    {
                        Debug.Log("hmm: " + (destination - transform.position).magnitude);
                        transform.position = destination + Vector3.up;
                    }
                    break;
                case InputDirections.Right:
                    destination = CalculateDestination(Vector3.right);
                    if ((destination - transform.position).magnitude > 1)
                    {
                        Debug.Log("hmm: " + (destination - transform.position).magnitude);
                        transform.position = destination - Vector3.right;
                    }
                    break;
                case InputDirections.Left:
                    destination = CalculateDestination(-Vector3.right);
                    if ((destination - transform.position).magnitude > 1)
                    {
                        Debug.Log("hmm: " + (destination - transform.position).magnitude);
                        transform.position = destination + Vector3.right;
                    }
                    break;
            }
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