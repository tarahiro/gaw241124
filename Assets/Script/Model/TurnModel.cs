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

        public void Enter()
        {
            _currentTurn = _playerTurn;
            _currentTurn.Enter();
        }

        public void GoNextSideTurn()
        {
            if(_currentTurn == _playerTurn)
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