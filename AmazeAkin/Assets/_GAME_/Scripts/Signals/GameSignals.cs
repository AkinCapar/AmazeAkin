using System.Collections.Generic;
using Amaze.Utilities;
using Amaze.Views;
using UnityEngine;

namespace Amaze
{
    public readonly struct LevelInitializedSignal
    {
        public readonly Transform StartPosition;
        public readonly List<PathTileView> PathTileViews;
        
        public LevelInitializedSignal(Transform startPos, List<PathTileView> pathTileViews)
        {
            StartPosition = startPos;
            PathTileViews = pathTileViews;
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

    public readonly struct PathTilesCompletedSignal
    {
        
    }
}
