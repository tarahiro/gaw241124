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

namespace gaw241124.View
{
    public interface IEnemyInitialStoneItemView : IGridPositionGettable
    {
        string Id { get; }

        List<Vector2Int> EyesightDirection { get; }

    }
}