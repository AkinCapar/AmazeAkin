using System.Collections;
using System.Collections.Generic;
using Amaze.Utilities;
using Amaze.Views;
using UnityEngine;

namespace Amaze.Settings
{
    [CreateAssetMenu(fileName = nameof(PrefabSettings), menuName = Constants.MenuNames.SETTINGS + nameof(PrefabSettings))]
    public class PrefabSettings : ScriptableObject
    {
        public BallView ballView;
    }
}
