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
    public class EnemyTurn : IEnemyTurn
    {

        [Inject] ITurn _turn;

        Subject<Unit> _TurnEntered = new Subject<Unit>();
        Subject<Unit> _TurnExited = new Subject<Unit>();
        public IObservable<Unit> TurnEntered => _TurnEntered;
        public IObservable<Unit> TurnExited => _TurnExited;
        public void Enter()
        {
            Log.DebugLog("EnemyTurnEnter");
            _turn.Enter();
            _TurnEntered.OnNext(Unit.Default);
        }

        public void Exit()
        {
            Log.DebugLog("EnemyTurnExit");
            _turn.Exit();
            _TurnExited.OnNext(Unit.Default);
        }
    }
}