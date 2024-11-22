using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tarahiro.TGrid
{
    public interface IGridModel
    {
        bool IsPositionable(Vector2Int position, int positionableIndex);

    }
}
