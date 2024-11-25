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
    public interface IEnemyModel
    {
        void InitializeModel(CompositeDisposable disposables);
        void TryNoticePlayerStone(Vector2Int position);
        bool IsKillAnyStoneChainIfPutted(Vector2Int position);
        IObservable<List<Vector2Int>> Arounded { get; }
    }
}