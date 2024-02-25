using System.Collections;
using System.Collections.Generic;
using Amaze.Data;
using Amaze.Utilities;
using UnityEngine;

namespace Amaze.Settings
{
    [CreateAssetMenu(fileName = nameof(LevelSettings), menuName = Constants.MenuNames.SETTINGS + nameof(LevelSettings))]
    public class LevelSettings : ScriptableObject
    {
        public List<LevelData> levels;
    }
}
