using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tarahiro.TGrid
{
    public class GridMonoBehaviourReader : MonoBehaviour,IGridReader
    {
        [SerializeField]
        Grid m_Grid;

        Tilemap[] m_TilemapArray;


        void ReadTilemap()
        {
            m_TilemapArray = m_Grid.GetComponentsInChildren<Tilemap>();
        }

        public List<Tilemap> GetTilemaps()
        {

            if(m_TilemapArray == null)
            {
                ReadTilemap();
            }
            return m_TilemapArray.ToList();
        }


    }
}