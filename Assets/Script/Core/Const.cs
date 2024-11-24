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
    public class Const
    {
        public enum TilemapLayer
        {
            Ground,
            Cross,
            Stone,
            Hide,
        }
        public enum Positionable
        {
            Groundable,
            Stone
        }

        public enum Side
        {
            Player,
            Enemy
        }
    }
}