using Amaze.Utilities;
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

    public readonly struct InputReceivedSignal
    {
        public readonly InputDirections Direction;
        public InputReceivedSignal(InputDirections direction)
        {
            Direction = direction;
        }
    }
}
