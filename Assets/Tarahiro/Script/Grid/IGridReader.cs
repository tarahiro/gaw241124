using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tarahiro.TGrid
{
    public interface IGridReader
    {
        List<Tilemap> GetTilemaps();
    }
}