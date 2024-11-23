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
    public class StoneModel : IStoneModel
    {
        Subject<Vector2Int> _stoneCreated = new Subject<Vector2Int>();

        public IObservable<Vector2Int> StoneCreated => _stoneCreated;

        public void CreateStone(Vector2Int position)
        {
            Log.DebugLog(position);
            _stoneCreated.OnNext(position);
        }

    }
}