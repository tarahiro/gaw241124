using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tarahiro.TGrid
{
    public static class GridUtil
    {
        public static Vector2Int ConvertPosition(Vector2 vec)
        {
            return new Vector2Int((int)Mathf.Round(vec.x - .5f), (int)Mathf.Round(vec.y - .5f));
        }
    }
}