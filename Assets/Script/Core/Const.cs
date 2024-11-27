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
    public static class Const
    {
        public const int c_alertStoneNumber = 2;

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
            PlayerStone,
            EnemyStone
        }

        public enum Side
        {
            Player,
            Enemy
        }
    }
}