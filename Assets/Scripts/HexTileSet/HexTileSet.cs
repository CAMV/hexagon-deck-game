using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileSet : MonoBehaviour
{
    public int mapHeight;
    public int mapWidth;
    //Tile size from the center of the hex to a corner
    public int tileSize;
    public Vector2 tileCenter;
    private Dictionary<Vector2, HexTile> tileSet = new Dictionary<Vector2, HexTile>();



    // Start is called before the first frame update
    void Start()
    {
        CreateTileSet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateTileSet()
    {
        HexTile baseTile = new HexTile(new Vector2(0, 0), tileSize);
        baseTile.CreatePointyToppedHex();

        float tileHeight = baseTile.height;
        Debug.Log(baseTile.height);
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
    }

    private void AddNewTile(float tileWidth, int i, float tileHeight, int j)
    {
        float offsetX = tileWidth / 2 * ((j + 1) % 2);
        float hexPosX = (i + 1) * tileWidth - offsetX;
        float hexPosY = tileSize + j * (tileHeight * 3 / 4);
        Vector2 center = new Vector2(hexPosX, hexPosY);
        tileSet.Add(new Vector2(i * 2, j), new HexTile(center, tileSize));

        CreateNewTileObject(hexPosX, hexPosY);
    }

    private void CreateNewTileObject(float x, float z)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.parent = transform;
        gameObject.transform.position = new Vector3((x - mapHeight / 2), 0, (z - mapWidth / 2));
    }

}
