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

        [Inject] IEnemyWholeGroupHundler _wholeGroupHundler;

        Subject<Unit> _brainEnded = new Subject<Unit>();
        public  IObservable<Unit> BrainEnded => _brainEnded;

        public async UniTask Enter()
        {
            List<IEnemyGroupBrain> _brainList = _wholeGroupHundler.GetInstanceOfAllGroup<IEnemyGroupBrain>();

            foreach (var brain in _brainList)
            {
                await brain.Enter();
            }

            _brainEnded.OnNext(Unit.Default);
        }
    }
}