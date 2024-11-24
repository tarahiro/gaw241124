using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tarahiro.TGrid
{
    internal interface IGridReader
    {
        List<Tilemap> GetTilemaps();
    }
}