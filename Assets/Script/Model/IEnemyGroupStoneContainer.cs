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
    public interface IEnemyGroupStoneContainer
    {
        void InitializeModel(CompositeDisposable disposables);
        void TryNoticePlayerStone(Vector2Int position);
        void NoticeEnemyStone(Vector2Int position, List<Vector2Int> eyesightDirection);
        bool IsKillAnyStoneChainIfPutted(Vector2Int position);
        bool TryGetAtariStone(out IEnemyGroupStoneChain enemyStoneChain);
        IObservable<List<Vector2Int>> Arounded { get; }
        IObservable<Vector2Int> EyesightStarted { get; }

        Vector2 Centroid();
    }
}