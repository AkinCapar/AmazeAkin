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
        [Tooltip("Lower is more sensitive")]
        public float inputSensitivity;
        public float ballMoveSpeed;
        public float pathTileJumpPower;
        public float pathTileJumpSpeed;
        public List<LevelData> levels;
    }
}
