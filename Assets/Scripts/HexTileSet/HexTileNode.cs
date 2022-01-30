using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileNode : MonoBehaviour
{
    public HexTile tile { get; private set; }
    public List<HexTileNode> adjacentTiles { get; set; }
    public bool isTraversable = true;
    bool isVisible = true;
    private int _traverseCost;
    [SerializeField]
    Vector2 displayNode;

    public int TraverseCost
    {
        get => _traverseCost;
    }


    public HexTileNode(HexTile tile)
    {
        this.tile = tile;
        _traverseCost = 1;
    }

    public void Initialize(HexTile tile) 
    {
        this.tile = tile;
        this.isTraversable = true;
        this.isVisible = true;
        displayNode = tile.pos;
        _traverseCost = 1;
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