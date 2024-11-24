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
    public interface ITreasureModel
    {
        void TryAchieveTreasure(Vector2Int position);

        void RegisterTreasure(TreasureModelItemArgs args);

        IObservable<int> TreasureAchieved { get; }
    }
}