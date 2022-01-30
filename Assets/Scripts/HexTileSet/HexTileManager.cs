using System.Collections.Generic;
using UnityEngine;

public class HexTileManager : MonoBehaviour
{
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
            node.FillAdjacentHexes(GetAdjacentTiles(node.tile.pos));
        }
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

        Vector3 hexGamePosition = gameObject.transform.position;
        LayerMask layerMask = LayerMask.GetMask("Terrain");
        RaycastHit cubeHit;
        if (Physics.Raycast(hexGamePosition + new Vector3(0, -4, 0), Vector3.up, out cubeHit, 5, layerMask))
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
                adjacentTiles.Add(tile);
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
