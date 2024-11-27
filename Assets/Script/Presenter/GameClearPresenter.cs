using Cysharp.Threading.Tasks;
using gaw241124.Model;
using gaw241124.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;


namespace gaw241124.Presenter
{
    public class GameClearPresenter : IPostStartable
    {
        [Inject] IGameClearModel _model;
        [Inject] IGameClearView _view;


        public void PostStart()
        {
            Log.DebugLogComment("GameClearPresenter‰Šú‰»");


            _model.Entered.Subscribe(_ => _view.EnterView() );
            _view.Clicked.Subscribe(_ => _model.Exit());

            _view.InitializeView();
        }

    }
}