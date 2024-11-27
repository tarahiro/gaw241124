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
    public interface IEnemyGroupStonePutter
    {
        void PutStone(Vector2Int position, List<Vector2Int> eyesightDirection);
        IObservable<Vector2Int> StoneArounded { get; }
    }
}