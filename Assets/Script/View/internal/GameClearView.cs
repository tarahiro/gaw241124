using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace gaw241124.View
{
    public class GameClearView :MonoBehaviour, IGameClearView
    {
        [SerializeField] GameObject _root;
        [SerializeField] Button  _button;

        Subject<Unit> _clicked = new Subject<Unit>();
        public IObservable<Unit> Clicked => _clicked;

        public void InitializeView()
        {
            _button.onClick.AddListener(() =>
            {
                _root.SetActive(false);
                _clicked.OnNext(Unit.Default);
            });
            _root.SetActive(false);

        }

        public void EnterView()
        {
            _button.interactable = true;
            _root.SetActive(true);
        }
    }
}