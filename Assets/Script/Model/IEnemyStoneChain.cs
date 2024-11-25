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
    public interface IEnemyStoneChain
    {
        void Initialize();
        List<Vector2Int> StonePositionList { get; }
        List<Vector2Int> EmptyAroundList { get; }
        IObservable<List<Vector2Int>> AroundFilled { get; }
        bool IsKilledIfStonePutted(Vector2Int position);

        void GetNoticeStoneOnAround(Vector2Int position);
    }
}