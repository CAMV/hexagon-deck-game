using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileGraph
{
    private Dictionary<Vector2, HexTileNode> tileSet = new Dictionary<Vector2, HexTileNode>();

    public HexTileGraph(Dictionary<Vector2, HexTileNode> tileSet)
    {
        this.tileSet = tileSet;
    }

    public bool HasVision(List<HexTileNode> hexes)
    {
        foreach (HexTileNode node in hexes)
        {
            if (!node.isTraversable) return false;
        }

        return true;
    }

    public List<HexTileNode> DrawLine(HexTileNode startNode, HexTileNode endNode)
    {
        List<HexTileNode> hexes = new List<HexTileNode>();

        int distance = GetDistance(startNode, endNode);




        return hexes;
    }

    public int GetDistance(HexTileNode startNode, HexTileNode endNode)
    {
        Vector2 startPos = startNode.tile.pos;
        Vector2 endPos = endNode.tile.pos;
        int dcol = (int) Mathf.Abs(startPos.x - endPos.x);
        int drow = (int) Mathf.Abs(startPos.y - endPos.y);

        return drow + Mathf.Max(0, (dcol-drow)/2);
    }

    private List<Vector2> InterpolateLine(HexTileNode startNode, HexTileNode endNode, int distance) {
        List<Vector2> line = new List<Vector2>();
        Vector2 startPoint = startNode.tile.pos;
        float startX = startPoint.x;
        float startY = startPoint.y;

        Vector2 endPoint = endNode.tile.pos;
        float endX = endPoint.x;
        float endY = endPoint.y;

        for (int i = 0; i <= distance; i++) {
            int interp = i / distance;
            float x = startX + (endX - startX) * interp;
            float y = startY + (endY - startY) * interp;
            Vector2 point = new Vector2(x, y);

            line.Add(CubeToDoubleWidth(CubeRound(DoublewidthToCube(point))));
        }

        return line;
    }


    private Vector3 DoublewidthToCube(Vector2 point) {
        float q = (point.x - point.y) / 2;
        float r = point.y;

        return new Vector3(q, r, q-r);
    }

    private Vector2 CubeToDoubleWidth(Vector3 cube) {
        var col = 2 * cube.x + cube.y;
        var row = cube.y;

        return new Vector2(col, row);
    }

    private Vector3 CubeRound(Vector3 frac) {

        var x = Mathf.Round(frac.x);
        var y = Mathf.Round(frac.y);
        var z = Mathf.Round(frac.z);

        var x_diff = Mathf.Abs(x - frac.x);
        var y_diff = Mathf.Abs(y - frac.y);
        var z_diff = Mathf.Abs(z - frac.z);

        if (x_diff > y_diff && x_diff > z_diff) 
        {
            x = -y-z;
        }
        else if (y_diff > z_diff)
        {
            y = -x-z;
        }
        else {
            z = -x-y;
        }

        return new Vector3(x, y, z);
    }

}