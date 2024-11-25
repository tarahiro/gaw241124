using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tarahiro.TGrid
{
    public static class GridUtil
    {
        static readonly List<Vector2Int> _direction = new List<Vector2Int>() { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        public static Vector2Int ConvertPosition(Vector2 vec)
        {
            return new Vector2Int((int)Mathf.Round(vec.x - .5f), (int)Mathf.Round(vec.y - .5f));
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static List<Vector2Int> GetDirectionList()
        {
            return _direction;
        }
    }
}