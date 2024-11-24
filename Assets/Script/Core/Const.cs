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
            Grid,
            Stone
        }
        public enum Positionable
        {
            Grid,
            Stone
        }
    }
}