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
    public class EnemyInitialStoneArgs
    {
        public string Id { get; set; }
        public Vector2Int Position { get; set; }

        public List<Vector2Int> EyesightDirection { get; set; }

        public EnemyInitialStoneArgs(string id, Vector2Int position, List<Vector2Int> eyesightDirection)
        {
            Id = id;
            Position = position;
            EyesightDirection = eyesightDirection;
        }
    }
}