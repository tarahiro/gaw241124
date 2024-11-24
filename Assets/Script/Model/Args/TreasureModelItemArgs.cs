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
    public class TreasureModelItemArgs
    {
        public int InstanceIndex { get; set; }
        public Vector2Int Position { get; set; }
        public string Id { get; set; }

        public int arg { get; set; }

        public TreasureModelItemArgs(int instanceIndex, Vector2Int position, string id, int arg)
        {
            InstanceIndex = instanceIndex;
            Position = position;
            Id = id;
            this.arg = arg;
        }
    }
}