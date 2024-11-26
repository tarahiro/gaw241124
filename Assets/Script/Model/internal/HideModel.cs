using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.TGrid;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;
using static gaw241124.Const;

namespace gaw241124.Model
{
    public class HideModel: MonoBehaviour, IHideModel
    {
        [Inject] IGridProvider _gridProvider;
        [Inject] StoneProvider _stoneProvider;

        [SerializeField] Sprite _showGroundStone;
         
        Subject<Vector2Int> _hided = new Subject<Vector2Int>();
        Subject<Vector2Int> _groundShowed = new Subject<Vector2Int>();

        public IObservable<Vector2Int> Hided => _hided;
        public IObservable<Vector2Int> GroundShowed => _groundShowed;

        const int c_range = 3;

        public void InitializeModel()
        {
            var ground = _gridProvider.GetTilemap((int)Const.TilemapLayer.Ground);
            var stoneTile = _gridProvider.GetTilemap((int)Const.TilemapLayer.Stone);

            for (int x = ground.origin.x; x < ground.size.x + ground.origin.x; x++)
            {
                for (int y = ground.origin.y; y < ground.size.y + ground.origin.x; y++)
                {
                    var v = new Vector2Int(x, y);

                    if (_gridProvider.IsPositionable(v, (int)Const.Positionable.Groundable))
                    {
                        for (int i = -c_range; i <= c_range; i++)
                        {
                            for (int j = -(c_range - Mathf.Abs(i)); j <= (c_range - Mathf.Abs(i)); j++)
                            {
                                if (stoneTile.GetTile(new Vector3Int(x + i, y + j, 0)) == _stoneProvider.GetTilebase(Const.Side.Player))
                                {
                                    goto EndRangeLoop;
                                }
                            }
                        }

                        _hided.OnNext(new Vector2Int(x, y));

                    EndRangeLoop:;
                    }
                }
            }
        }
        
        public void ClearHide(Vector2Int position)
        {
            for(int i = -c_range; i <= c_range; i++)
            {
                for (int j = -(c_range - Mathf.Abs(i)); j <= (c_range - Mathf.Abs(i)); j++)
                {
                    _groundShowed.OnNext(new Vector2Int(position.x + i, position.y + j));
                }
            }
        }
    }
}