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
    public class TitleModel : ITitleModel
    {
        Subject<Unit> _entered = new Subject<Unit>();
        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;
        public IObservable<Unit> Exited => _exited;
       public void Enter()
        {

        }
       public void Exit()
        {
            _exited.OnNext(Unit.Default);
        }


    }
}