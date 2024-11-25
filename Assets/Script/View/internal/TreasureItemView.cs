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
    public class TreasureItemView : MonoBehaviour, ITreasureItemView
    {
        [SerializeField] string _id;
        [SerializeField] int _arg;

        public int Index{get;set;}
        public string Id => _id;
        public int Arg => _arg;

        public Vector2Int GridPosition => GridUtil.ConvertPosition(transform.position);

        public void Destroy()
        {
            Destroy(gameObject);
        }

    }
}