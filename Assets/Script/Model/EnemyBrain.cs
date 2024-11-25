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
    public class EnemyBrain : IEnemyBrain
    {
        [Inject] IEnemyStoneContainer _container;
        [Inject] IEnemyStonePutTryer _putTryer;

        Subject<Unit> _brainEnded = new Subject<Unit>();
        public IObservable<Unit> BrainEnded => _brainEnded;

        public void Enter()
        {
            Log.DebugLog("BrainEnter");

            //ÉAÉ^ÉäÇÃêŒÇ™Ç¢ÇΩÇÁÅAì¶Ç∞ÇÈ
            if (_container.TryGetAtariStone(out var v))
            {
                Log.DebugLog("AtariExist");
                _putTryer.TryPutStone(v.EmptyAroundList[0]);
            }

            _brainEnded.OnNext(Unit.Default);

        }

    }
}