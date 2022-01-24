using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileNode
{
    public HexTile tile { get; private set; }
    public List<HexTileNode> adjacentTiles { get; set; }
    bool isTraversable = true;

    public HexTileNode(HexTile tile)
    {
        this.tile = tile;
    }


    public void FillAdjacentHexes(List<HexTileNode> tileNodes)
    {
        List<HexTileNode> adjacentTiles = new List<HexTileNode>();
        foreach (HexTileNode tileNode in tileNodes)
        {
            if (tileNode.isTraversable) adjacentTiles.Add(tileNode);
        }

        this.adjacentTiles = adjacentTiles;
    }
}