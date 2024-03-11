using System;
using System.Collections;
using System.Collections.Generic;
using Amaze.Controllers;
using Amaze.Utilities;
using UnityEngine;
using Zenject;

namespace Amaze.Controllers
{
    public class GameController : IInitializable, ITickable, IDisposable
    {
        private GameStates _gameState = GameStates.WaitingToStart;
        private LevelController _levelController;

        public GameController(LevelController levelController)
        {
            _levelController = levelController;
        }

        public void Initialize()
        {
            
        }

        public void Tick()
        {
            switch (_gameState)
            {
                case GameStates.WaitingToStart:
                {
                    UpdateStarting();
                    break;
                }
                case GameStates.Playing:
                {
                    UpdatePlaying();
                    break;
                }
            }
        }

        private void UpdatePlaying()
        {
            
        }

        private void UpdateStarting()
        {
            if (_gameState != GameStates.WaitingToStart)
            {
                return;
            }

            StartGame();
        }

        private void StartGame()
        {
            if(_gameState != GameStates.WaitingToStart) { return;}
            
            _levelController.Initialize();
            _gameState = GameStates.Playing;
        }

        public GameStates GameState
        {
            get { return _gameState; }
        }

        public void Dispose()
        {
            _levelController.Dispose();
        }
    }
}
