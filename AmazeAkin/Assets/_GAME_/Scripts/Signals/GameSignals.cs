using UnityEngine;

namespace Amaze
{
    public readonly struct LevelInitializedSignal
    {
        public readonly Transform StartPosition;
        
        public LevelInitializedSignal(Transform startPos)
        {
            StartPosition = startPos;
        }
    }
}
