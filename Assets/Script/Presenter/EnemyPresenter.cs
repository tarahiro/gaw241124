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
    public class EnemyPresenter : IInitializable,IStartable
    {
        [Inject] IEnemyWholeGroupHundler _enemyStoneHundler;
        [Inject] IEnemyInitialStoneView _initialStoneView;
        [Inject] IEnemyBrain _brain;
        [Inject] IEnemyTurn _turn;
        [Inject] Func<IEnemyInitialStoneItemView, EnemyInitialStoneArgs> _factory;
        CompositeDisposable _disposable = new CompositeDisposable();

        public void Initialize()
        {
            Log.DebugLogComment("EnemyPresenter‰Šú‰»");


            _initialStoneView.ItemFinded.Subscribe(x => _enemyStoneHundler.RegisterEnemyInitialStone(_factory.Invoke(x))).AddTo(_disposable);
            _turn.TurnEntered.Subscribe(_ => _brain.Enter().Forget()).AddTo(_disposable);
            _brain.BrainEnded.Subscribe(_ => _turn.Exit()).AddTo(_disposable);

            _enemyStoneHundler.InitializeModel();
        }

        public void Start()
        {
            _initialStoneView.InitializeView();
        }


    }
}