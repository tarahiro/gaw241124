using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tarahiro.TGrid
{
    internal class GridMonoBehaviourReader : MonoBehaviour,IGridReader
    {
        [SerializeField]
        Grid m_Grid;

        List<Tilemap> _tileMapList = new List<Tilemap>();


        void ReadTilemap()
        {
            Log.DebugLogComment("TileMap読み込み");

            var m_TilemapArray = m_Grid.GetComponentsInChildren<Tilemap>();

           　var query =  m_TilemapArray.OrderBy(t => t.GetComponent<TilemapRenderer>().sortingOrder);

            Log.DebugLogComment("Order順に並べ替える");
            foreach (var tilemap in query)
            {
                Log.DebugLogComment(tilemap.name + "読み込み");
                _tileMapList.Add(tilemap);
            }
        }

        public List<Tilemap> GetTilemaps()
        {
            Log.DebugLogComment("TileMap返送");
            if (_tileMapList.Count == 0)
            {
                ReadTilemap();
            }
            return _tileMapList;
        }


    }
}