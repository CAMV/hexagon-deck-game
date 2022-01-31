using System;
using System.Collections.Generic;
using UnityEngine;

public class HexTileManager : MonoBehaviour
{
    private static Vector3 RAYCAST_ELEVATION = new Vector3(0, 4, 0);
    private static int RAYCAST_DISTANCE = 10;
    public int mapHeight;
    public int mapWidth;
    //Tile size from the center of the hex to a corner
    public int tileSize;
    private Dictionary<Vector2, HexTileNode> tileSet = new Dictionary<Vector2, HexTileNode>();
    private HexTileGraph tileGraph;

    void Awake()
    {
        Locator.ProvideHexTileManager(this);
        CreateTileSet();
        tileGraph = new HexTileGraph(tileSet);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public HexTileNode GetNode(Vector2 pos)
    {
        return tileSet[pos];
    }

    private void CreateTileSet()
    {
        HexTile baseTile = new HexTile(new Vector2(0, 0), tileSize);
        baseTile.CreatePointyToppedHex();

        float tileHeight = baseTile.height;
        float tileWidth = baseTile.width;

        int maxRows = (int)((mapHeight * 4 / 3) / tileHeight);
        int maxCols = (int)(mapWidth / tileWidth);

        for (int i = 0; i < maxCols - 1; i++)
        {
            for (int j = 0; j < maxRows - 1; j++)
            {
                AddNewTile(tileWidth, i, tileHeight, j);
            }
        }

        foreach (HexTileNode node in tileSet.Values)
        {
            UpdateAdjacents(node);
        }

        foreach (HexTileNode node in tileSet.Values)
        {
            CheckWalls(node);
        }

    }


    public void CheckWalls(HexTileNode node) 
    {
        Vector3 nodePos = node.transform.position + RAYCAST_ELEVATION;;
        //Vector3 nodePos = new Vector3(nodePos.x, 0, nodePos.z) 
        //Debug.DrawLine(nodePos, nodePos - RAYCAST_ELEVATION * 2, Color.magenta, 10);

        LayerMask wallLayer = LayerMask.GetMask("Walls");
        RaycastHit rayHit;
        if (Physics.Raycast(nodePos, Vector3.down, out rayHit, RAYCAST_DISTANCE, wallLayer))
        {
            node.isTraversable = false;
            UpdateAdjacents(node);
        } else
        {
            float d = 0.15f;
            AttemptMovedWallHit(node, nodePos, d, 1, 1);
            AttemptMovedWallHit(node, nodePos, d, -1, 1);
            AttemptMovedWallHit(node, nodePos, d, 1, -1);
            AttemptMovedWallHit(node, nodePos, d, -1, -1);
            AttemptMovedWallHit(node, nodePos, d, 2, 0);
            AttemptMovedWallHit(node, nodePos, d, -2, 0);
        }

    }

    private void AttemptMovedWallHit(HexTileNode node, Vector3 nodePos, float d, int v1, int v2)
    {
        LayerMask wallLayer = LayerMask.GetMask("Walls");
        RaycastHit rayHit;
        Vector3 deltaNodePos = new Vector3(nodePos.x + v1*d, nodePos.y, nodePos.z + v2*d);
        //Debug.DrawLine(deltaNodePos, deltaNodePos - RAYCAST_ELEVATION * 2, Color.red, 10);
        if (Physics.Raycast(deltaNodePos, Vector3.down, out rayHit, RAYCAST_DISTANCE, wallLayer))
        {
            Debug.Log("hit a wall with offset " + v1 + ", " + v2);
            UnpassableNeighbor(node, new Vector2(v1, v2));
        }
    }

    private void UnpassableNeighbor(HexTileNode node, Vector2 offset)
    {
        Vector2 neighborPos = node.tile.pos + offset;
        HexTileNode neighbor;
        if (tileSet.TryGetValue(neighborPos, out neighbor))
        {
            neighbor.RemoveAdjacent(node);
        }
    }

    public void ToggleUnpassableNode(HexTileNode node)
    {
        node.isTraversable = !node.isTraversable;

        foreach(HexTileNode adj in GetAdjacentTiles(node.tile.pos))
        {
            UpdateAdjacents(adj);
        }
    }

    public void UpdateAdjacents(HexTileNode node)
    {
        List<HexTileNode> adjacents = GetAdjacentTiles(node.tile.pos);
        node.FillAdjacentHexes(adjacents);
    }

    private void AddNewTile(float tileWidth, int i, float tileHeight, int j)
    {
        float offsetX = tileWidth / 2 * ((j + 1) % 2);
        float hexPosX = (i + 1) * tileWidth - offsetX;
        float hexPosY = tileSize + j * (tileHeight * 3 / 4);
        Vector2 center = new Vector2(hexPosX, hexPosY);

        HexTile tile = new HexTile(center, tileSize);
        Vector2 pos = new Vector2((i * 2) + j % 2, j);
        tile.pos = pos;

        SetTileObject(hexPosX, hexPosY, tile);
    }

    private void SetTileObject(float x, float z, HexTile tile)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.parent = transform;
        gameObject.transform.localPosition = new Vector3((x - mapHeight / 2), 0, (z - mapWidth / 2));
        //gameObject.AddComponent<HexTileNode>();
        //gameObject.GetComponent<HexTileNode>().Initialize(tile);


        Vector3 hexGamePosition = gameObject.transform.position;
        LayerMask layerMask = LayerMask.GetMask("Terrain");
        RaycastHit cubeHit;
        //Debug.DrawLine(hexGamePosition + new Vector3(0, 4, 0), hexGamePosition + new Vector3(0,-4,0), Color.red, 1000);
        if (Physics.Raycast(hexGamePosition + RAYCAST_ELEVATION, Vector3.down, out cubeHit, RAYCAST_DISTANCE, layerMask))
        {
            cubeHit.collider.transform.gameObject.AddComponent<HexTileNode>();
            HexTileNode hexTileNode = cubeHit.collider.transform.gameObject.GetComponent<HexTileNode>();
            hexTileNode.Initialize(tile);


            tileSet.Add(tile.pos, hexTileNode);
        }

        Destroy(gameObject);
    }


    public List<HexTileNode> GetAdjacentTiles(Vector2 pos)
    {
        List<HexTileNode> adjacentTiles = new List<HexTileNode>();

        List<Vector2> hexAdjacents = GetHexAdjacents(pos);

        foreach (Vector2 adj in hexAdjacents)
        {
            HexTileNode tile;
            if (tileSet.TryGetValue(adj, out tile))
            {
                if (tile.isTraversable)
                {
                    adjacentTiles.Add(tile);
                }
            }
        }

        return adjacentTiles;
    }

    public List<Vector2> GetHexAdjacents(Vector2 pos)
    {
        List<Vector2> adjacents = new List<Vector2>();

        adjacents.Add(new Vector2(pos.x + 1, pos.y + 1));
        adjacents.Add(new Vector2(pos.x - 1, pos.y + 1));
        adjacents.Add(new Vector2(pos.x + 1, pos.y - 1));
        adjacents.Add(new Vector2(pos.x - 1, pos.y - 1));
        adjacents.Add(new Vector2(pos.x + 2, pos.y));
        adjacents.Add(new Vector2(pos.x - 2, pos.y));

        return adjacents;
    }

    public void TriggerNodeClick(Vector2 pos)
    {
        PlayerController player = Locator.GetPlayerController();

        if (player.IsSelected)
        {
            List<Vector2> walkPath = tileGraph.GetShortestPath(player.StandingNode, tileSet[pos]);
            if (walkPath.Count > 1) {
                player.Move(walkPath);
            }
        }
    }
}
