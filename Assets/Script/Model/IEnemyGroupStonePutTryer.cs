using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124
{
    public interface IEnemyGroupStonePutTryer
    {
        bool TryPutStone(Vector2Int position, List<Vector2Int> eyesightDirection);

    }
}