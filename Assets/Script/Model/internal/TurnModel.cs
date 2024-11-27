using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Model
{
    public class TurnModel : ITurnModel
    {
        [Inject] IPlayerTurn _playerTurn;
        [Inject] IEnemyTurn _enemyTurn;

        ITurn _currentTurn;
        bool _isTurnEntered = false;

        Subject<Unit> _entered = new Subject<Unit>();
        Subject<bool> _exited = new Subject<bool>();
        public IObservable<Unit> Entered => _entered;
        public IObservable<bool> Exited => _exited;

        public void Enter()
        {
            _isTurnEntered = true;
            _entered.OnNext(Unit.Default);
            _currentTurn = _playerTurn;
            _currentTurn.Enter();
        }

        public void Exit(bool b)
        {

            Log.DebugLog("TurnModelèIóπ");

            _isTurnEntered = false;
            _exited.OnNext(b);
        }

        public void TryGoNextSideTurn()
        {
            if (_isTurnEntered)
            {

                if (_currentTurn == _playerTurn)
                {
                    _currentTurn = _enemyTurn;
                }
                else
                {
                    _currentTurn = _playerTurn;
                }

                _currentTurn.Enter();
            }
        }
    }
}