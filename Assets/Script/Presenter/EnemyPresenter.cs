using Cysharp.Threading.Tasks;
using gaw241124.Model;
using gaw241124.View;
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
        [Inject] IEnemyStoneHundler _enemyStoneHundler;
        [Inject] IEnemyInitialStoneView _initialStoneView;
        [Inject] Func<IEnemyInitialStoneItemView, EnemyInitialStoneArgs> _factory;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void Initialize()
        {
            _initialStoneView.ItemFinded.Subscribe(x => _enemyStoneHundler.RegisterEnemyInitialStone(_factory.Invoke(x))).AddTo(_disposable);

            _initialStoneView.InitializeView();
            _enemyStoneHundler.InitializeModel();
        }


    }
}