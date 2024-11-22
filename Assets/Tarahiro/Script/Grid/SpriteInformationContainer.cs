using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tarahiro.TGrid
{
    [CreateAssetMenu(menuName = "SpriteInformationContainer")]
    public class SpriteInformationContainer : ScriptableObject
    {
        /*
        [SerializeField]
        List<Sprite> m_UnWalkableTileList;
        [SerializeField]
        List<Sprite> m_UnShipableTileList;
        */

        [SerializeField]
        List<SpriteList> m_UnPositionableTileList;

        /*
        public List<Sprite> GetUnWalkableTileList()
        {
            return m_UnWalkableTileList;
        }
        public List<Sprite> GetUnShipableTileList()
        {
            return m_UnShipableTileList;
        }
        */

        public List<Sprite> GetPositionableList(int positionableIndex)
        {
            return m_UnPositionableTileList[positionableIndex].spriteList;
        }

        public int Count()
        {
            return m_UnPositionableTileList.Count;
        }

        [System.Serializable]
        private class SpriteList
        {
            [SerializeField]
            public List<Sprite> spriteList;
        }
    }
}