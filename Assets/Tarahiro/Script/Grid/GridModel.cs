using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Tilemaps;
using VContainer;
using VContainer.Unity;

namespace Tarahiro.TGrid
{
    public class GridModel : IGridModel, IInitializable
    {
        [Inject] IGridReader _gridReader;
        [Inject] SpriteInformationContainer _spriteInformationContainer;


        List<Tilemap> m_TilemapList;
        int m_GroundLayer;

        List<List<Sprite>> UnPositionableTileList;


        public void Initialize()
        {
            m_TilemapList = _gridReader.GetTilemaps();

            UnPositionableTileList = new List<List<Sprite>>();

            for (int i = 0; i < _spriteInformationContainer.Count(); i++)
            {

                UnPositionableTileList.Add(_spriteInformationContainer.GetPositionableList(i));
            }

        }

        public bool IsPositionable(Vector2Int position, int positionableIndex)
        {
            if (!isInTileMap(position, m_TilemapList))
            {
                return false;
            }

            for (int i = 0; i < m_TilemapList.Count; i++)
            {
                if (m_TilemapList[i].GetTile((Vector3Int)position) != null)
                {
                    if (UnPositionableTileList[positionableIndex].Exists(x => x.name == m_TilemapList[i].GetTile((Vector3Int)position).name))
                    {
                        return false;
                    }
                }
            }
            return true;

        }

        bool isInTileMap(Vector2Int position, List<Tilemap> tilemapList)
        {

                if(position.x < m_TilemapList[m_GroundLayer].origin.x 
                || position.x > m_TilemapList[m_GroundLayer].origin.x + m_TilemapList[m_GroundLayer].size.x)
                {
                    return false;
                }
                if(position.y < m_TilemapList[m_GroundLayer].origin.y 
                || position.y > m_TilemapList[m_GroundLayer].origin.y + m_TilemapList[m_GroundLayer].size.y)
                {
                    return false;
                }
            return true;

        }
    }
}