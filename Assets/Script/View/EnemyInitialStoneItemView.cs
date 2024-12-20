using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.TGrid;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.View
{
    public class EnemyInitialStoneItemView : MonoBehaviour, IEnemyInitialStoneItemView
    {
        [SerializeField] string _id;
        [SerializeField] List<Vector2Int> _eyesightDirection;

        public string Id => _id;

        public Vector2Int GridPosition => GridUtil.ConvertPosition(transform.position);

        public List<Vector2Int> EyesightDirection => _eyesightDirection;

    }
}