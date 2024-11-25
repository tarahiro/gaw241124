using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tarahiro.TGrid
{
    [System.Serializable]
    public class SerializableSpriteList
    {
        [SerializeField]
        string name;

        [SerializeField]
        public List<Sprite> spriteList;
    }
}