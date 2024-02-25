using System.Collections;
using System.Collections.Generic;
using Amaze.Data;
using Amaze.Models;
using Amaze.Settings;
using UnityEngine;
using Zenject;

namespace Amaze.Controllers
{
    public class LevelController
    {

        private LevelView _levelView;
        
        #region Injection

        private PrefabSettings _prefabSettings;
        private LevelSettings _levelSettings;
        private LevelModel _levelModel;
        private SignalBus _signalBus;
        private LevelView.Factory _levelViewFactory;

        public LevelController(PrefabSettings prefabSettings
            , LevelSettings levelSettings
            , SignalBus signalBus
            , LevelModel levelModel
            , LevelView.Factory levelViewFactory)
        {
            _prefabSettings = prefabSettings;
            _levelSettings = levelSettings;
            _signalBus = signalBus;
            _levelModel = levelModel;
            _levelViewFactory = levelViewFactory;
        }

        #endregion
        
        
        public void Initialize()
        {
            CreateLevel();
        }

        private void CreateLevel()
        {
            Debug.Log("akin1234");
            LevelData currentLevelData = _levelSettings.levels[_levelModel.CurrentLevel()];
            _levelView = _levelViewFactory.Create(currentLevelData.LevelPrefab);
        }


        private void CompleteLevel()
        {
            //_levelModel.IncreaseCurrentLevel(1);
            CreateLevel();
        }

        public void Dispose()
        {            
        }
    }
}
