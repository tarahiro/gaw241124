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
    public class EnemyStatus
    {
        public bool IsPercievePlayer { get; set; } = false;

        public List<Vector2Int> PercievedPlayerStone { get; set; } = new List<Vector2Int>();
    }
}