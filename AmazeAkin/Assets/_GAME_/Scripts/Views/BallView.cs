using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Amaze.Views
{
    public class BallView : MonoBehaviour
    {
        public void Initialize(Transform startPos)
        {
            gameObject.transform.position = startPos.position;
            gameObject.transform.parent = startPos;
        }
        public class Factory : PlaceholderFactory<Object, BallView>
        {
        }
    }
}
