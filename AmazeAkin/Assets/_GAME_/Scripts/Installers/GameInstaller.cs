using System.Collections;
using System.Collections.Generic;
using Amaze.Controllers;
using Amaze.Models;
using Amaze.Settings;
using DreamBlast.Controllers;
using UnityEngine;
using Zenject;

namespace Amaze.Installer
{
    public class GameInstaller : MonoInstaller
    {
        #region Injection
        
        [Inject]
        private void Construct()
        {
        }

        #endregion
        public override void InstallBindings()
        {
            GameSignalsInstaller.Install(Container);
            
            
            //MODELS
            Container.Bind<LevelModel>().AsSingle();
            
            //CONTROLLERS
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
            Container.Bind<LevelController>().AsSingle();
            
            //FACTORIES
            Container.BindFactory<Object, LevelView, LevelView.Factory>().FromFactory<PrefabFactory<LevelView>>();
        }
        
        /*private void InstallBubbles()
        {
            Container.BindFactory<BubbleData, Transform, int, BubbleView, BubbleView.Factory>()
                .FromPoolableMemoryPool<BubbleData, Transform, int, BubbleView, BubbleView.Pool>(poolBinder => poolBinder
                    .WithInitialSize(100)
                    .FromComponentInNewPrefab(_prefabSettings.bubbleView)
                    .UnderTransform(_bubbleContainer));
        }*/
    }
}
