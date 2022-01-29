using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileNode : MonoBehaviour
{
    public HexTile tile { get; private set; }
    public List<HexTileNode> adjacentTiles { get; set; }
    public bool isTraversable = true;
    bool isVisible = true;
    public int traverseCost { get; private set; }

    public HexTileNode(HexTile tile)
    {
        this.tile = tile;
        traverseCost = 1;
    }

    public void Initialize(HexTile tile) 
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

    public void TriggerNodeClick()
    {
        Debug.Log(this.tile);
        if (this.tile != null)
        {
            Locator.GetHexTileManager().TriggerNodeClick(this.tile.pos);
        }
    }
}