using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tarahiro.TGrid
{
    [CreateAssetMenu(menuName = "SpriteInformationContainer")]
    public class SpriteInformationContainer : ScriptableObject
    {
        [SerializeField]
        List<SerializableSpriteList> m_UnPositionableTileList;

        public List<Sprite> GetPositionableList(int positionableIndex)
        {
            return m_UnPositionableTileList[positionableIndex].spriteList;
        }

        public int Count()
        {
            return m_UnPositionableTileList.Count;
        }

    }
}