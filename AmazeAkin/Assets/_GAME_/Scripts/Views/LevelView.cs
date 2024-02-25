using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelView : MonoBehaviour
{
    
    
    public class Factory : PlaceholderFactory<Object, LevelView>
    {
    }
}
