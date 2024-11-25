using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace gaw241124
{
    [CreateAssetMenu(menuName = "StoneProvider")]
    public class StoneProvider: ScriptableObject
    {
        [SerializeField] List<TileBase> _stoneSpriteList;

        public TileBase GetTilebase(Const.Side side)
        {
            return _stoneSpriteList[(int)side];
        }

        public int Count()
        {
            return _stoneSpriteList.Count;
        }
    }
}