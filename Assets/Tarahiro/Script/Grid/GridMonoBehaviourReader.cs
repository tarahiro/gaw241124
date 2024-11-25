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

        List<Tilemap> _tileMapList;


        void ReadTilemap()
        {
            var m_TilemapArray = m_Grid.GetComponentsInChildren<Tilemap>();

           　var query =  m_TilemapArray.OrderBy(t => t.GetComponent<TilemapRenderer>().sortingOrder);
            //Order順に並べ替える
            _tileMapList = new List<Tilemap>();
            foreach(var tilemap in query)
            {
                _tileMapList.Add(tilemap);
            }
        }

        public List<Tilemap> GetTilemaps()
        {
            if (_tileMapList == null)
            {
                ReadTilemap();
            }
            return _tileMapList;
        }


    }
}