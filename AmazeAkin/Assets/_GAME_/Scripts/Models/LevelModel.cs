using UnityEngine;

namespace Amaze.Models
{
    public class LevelModel
    {
        private int _currentLevel;

        public int CurrentLevel()
        {
            return _currentLevel < 10 ? _currentLevel : Random.Range(0, 10);
        }

        public void IncreaseCurrentLevel(int increaseAmount)
        {
            _currentLevel += increaseAmount;
        }

        public void SetCurrentLevel(int value)
        {
            _currentLevel = value;
        }
    }
}
