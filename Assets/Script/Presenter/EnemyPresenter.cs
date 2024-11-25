using Cysharp.Threading.Tasks;
using gaw241124.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241124.Presenter
{
    public class EnemyPresenter : IInitializable
    {
        [Inject] IEnemyModel _model;

        public void Initialize()
        {
            _model.InitializeModel();
        }
    }
}