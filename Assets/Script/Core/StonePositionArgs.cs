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
    public class StonePositionArgs
    {
        public Const.Side Side { get; set; }
        public Vector2Int Position { get; set; }

        public StonePositionArgs(Const.Side side, Vector2Int position)
        {
            Side = side;
            Position = position;
        }
    }
}