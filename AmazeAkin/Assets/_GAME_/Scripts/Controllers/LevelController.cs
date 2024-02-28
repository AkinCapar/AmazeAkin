using System.Collections;
using System.Collections.Generic;
using Amaze.Data;
using Amaze.Models;
using Amaze.Settings;
using Amaze.Views;
using UnityEngine;
using Zenject;

namespace Amaze.Controllers
{
    public class LevelController
    {
        private BallView _ballView;
        private LevelView _levelView;
        private List<PathTileView> _tilesToBeCompleted;
        
        #region Injection

        private PrefabSettings _prefabSettings;
        private LevelSettings _levelSettings;
        private LevelModel _levelModel;
        private SignalBus _signalBus;
        private LevelView.Factory _levelViewFactory;
        private BallView.Factory _ballViewFactory;

        public LevelController(PrefabSettings prefabSettings
            , LevelSettings levelSettings
            , SignalBus signalBus
            , LevelModel levelModel
            , LevelView.Factory levelViewFactory
            , BallView.Factory ballViewFactory)
        {
            _prefabSettings = prefabSettings;
            _levelSettings = levelSettings;
            _signalBus = signalBus;
            _levelModel = levelModel;
            _levelViewFactory = levelViewFactory;
            _ballViewFactory = ballViewFactory;
        }

        #endregion
        
        
        public void Initialize()
        {
            _signalBus.Subscribe<LevelInitializedSignal>(OnLevelInitializedSignal);
            CreateLevel();
        }

        private void CreateLevel()
        {
            LevelData currentLevelData = _levelSettings.levels[_levelModel.CurrentLevel()];
            _levelView = _levelViewFactory.Create(currentLevelData.LevelPrefab);
            _levelView.Initialize();
        }


        private void CompleteLevel()
        {
            _levelModel.IncreaseCurrentLevel(1);
            CreateLevel();
        }

        private void OnLevelInitializedSignal(LevelInitializedSignal signal)
        {
            _tilesToBeCompleted = signal.PathTileViews;
            _ballView = _ballViewFactory.Create(_prefabSettings.ballView);
            _ballView.Initialize(signal.StartPosition);
        }

        public void ManagePaintedTile(PathTileView tile)
        {
            if (_tilesToBeCompleted.Contains(tile))
            { 
                _tilesToBeCompleted.Remove(tile);
            }

            if (_tilesToBeCompleted.Count < 1)
            {
                Debug.Log("LEVEL COMPLETED!");
                _signalBus.Fire<PathTilesCompletedSignal>();
            }
        }

        public void Dispose()
        {            
            _signalBus.Unsubscribe<LevelInitializedSignal>(OnLevelInitializedSignal);
        }
    }
}
