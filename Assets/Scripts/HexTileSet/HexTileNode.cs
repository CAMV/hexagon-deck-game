using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileNode : MonoBehaviour
{
    public HexTile tile { get; private set; }
    public List<HexTileNode> adjacentTiles { get; set; }
    public bool isTraversable = true;   //Used when the node will never be passable(walls, trees, static blocking props, etc)
    private bool _isOccupied = true;    //Used when occupied by unpassable transient entities (units, AoEs, etc)
    bool isVisible = true;
    private int _traverseCost;
    [SerializeField]
    Vector2 displayNode;

    public bool IsOccupied
    {
        get => _isOccupied;
        set => _isOccupied = value;
    }

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

    public void ToggleOccupied()
    {
        _isOccupied = !IsOccupied;
    }

    public void TriggerNodeClick()
    {
        Debug.Log(this.tile);
        if (this.tile != null)
        {
            Locator.GetHexTileManager().TriggerNodeClick(this.tile.pos);
        }
    }

    internal void RemoveAdjacent(HexTileNode node)
    {
        List<HexTileNode> neighbors = new List<HexTileNode>();

        foreach (var neighbor in adjacentTiles)
        {
            if (neighbor != node)
            {
                neighbors.Add(neighbor);
            }
        }

        adjacentTiles = neighbors;
    }
}