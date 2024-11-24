using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tarahiro.TGrid
{
    public interface IGridProvider
    {
        bool IsPositionable(Vector2Int position, int positionableIndex);

        /// <summary>
        /// タイルマップを取得します。Order in Layerが若い順に並んでいます。
        /// </summary>
        /// <param name="tileMapId"></param>
        /// <returns></returns>
        Tilemap GetTilemap(int tileMapId);

    }
}
