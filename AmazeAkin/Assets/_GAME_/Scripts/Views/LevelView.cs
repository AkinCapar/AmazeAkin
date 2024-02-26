using System.Collections;
using System.Collections.Generic;
using Amaze;
using UnityEngine;
using Zenject;

namespace Amaze.Views
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Transform startPosition;

        #region Injection

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        #endregion

        public void Initialize()
        {
            _signalBus.Fire(new LevelInitializedSignal(startPosition));
        }

        public class Factory : PlaceholderFactory<Object, LevelView>
        {
        }
    }
}
