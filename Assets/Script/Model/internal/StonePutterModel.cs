
using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using Tarahiro.TGrid;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Model
{
    public class StonePutterModel : IStonePutterModel
    {

        Subject<StonePositionArgs> _stonePutted = new Subject<StonePositionArgs>();

        public IObservable<StonePositionArgs> StonePutted => _stonePutted;

        public void PutStone(StonePositionArgs positionArgs)
        {
            Log.DebugLog("Puttermodel : PutStone");
            _stonePutted.OnNext(positionArgs);

        }
    }
}