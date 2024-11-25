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

namespace gaw241124.Model
{
    public class EnemyModel : IEnemyModel
    {
        [Inject] IGridProvider _gridProvider;
        [Inject] StoneProvider _stoneProvider;
        [Inject] Func<Vector2Int, IEnemyStoneChain> _factory;

        List<IEnemyStoneChain> _stoneChain;

        public void InitializeModel()
        {
            _stoneChain = new List<IEnemyStoneChain>();

            //Ç∆ÇËÇ†Ç¶Ç∏íPàÍÇÃêŒÇÃÇ›
            var map = _gridProvider.GetTilemap((int)Const.TilemapLayer.Stone);

            for (int i = map.origin.x; i < map.origin.x + map.size.x; i++)
            {
                for (int j = map.origin.y; j < map.origin.y + map.size.y; j++)
                {
                    var v = new Vector2Int(i, j);
                    if (map.GetTile((Vector3Int)v) == _stoneProvider.GetTilebase(Const.Side.Enemy))
                    {
                        var chain = _factory.Invoke(v);
                        chain.Initialize();
                    }
                }
            }
        }

        public void TryNoticePlayerStone(Vector2Int position)
        {
            foreach (var stone in _stoneChain)
            {
                if (stone.EmptyAroundList.Contains(position))
                {
                    stone.GetNoticeStoneOnAround(position);
                }
            }
        }

        public bool IsKillAnyStoneChainIfPutted(Vector2Int position)
        {
            foreach (var stone in _stoneChain)
            {
                if (stone.IsKilledIfStonePutted(position))
                {
                    return true;
                }
            }
            return false;
        }
    }
}