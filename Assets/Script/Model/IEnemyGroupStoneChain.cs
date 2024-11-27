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
    public interface IEnemyGroupStoneChain
    {
        void Initialize();
        List<Vector2Int> StonePositionList { get; }
        List<Vector2Int> EmptyAroundList { get; }
        List<Vector2Int> EyesightList { get; }
        IObservable<List<Vector2Int>> AroundFilled { get; }
        IObservable<Vector2Int> EyesightStarted { get; }
        bool IsKilledIfStonePutted(Vector2Int position);
        bool IsAtari();

        void GetNoticeStoneOnAround(Vector2Int position);
        void AddStone(Vector2Int position, List<Vector2Int> eyesightDirection);
        void RemovePlayerStone(Vector2Int position);
    }
}