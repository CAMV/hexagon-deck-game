using System.Collections;
using System.Collections.Generic;
using System;
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

        List<Vector2> interpolatedNodes = InterpolateLine(startNode, endNode, distance);

        foreach (Vector2 node in interpolatedNodes)
        {
            hexes.Add(tileSet[node]);
        }

        return hexes;
    }

    public int GetDistance(HexTileNode startNode, HexTileNode endNode)
    {
        Vector2 startPos = startNode.tile.pos;
        Vector2 endPos = endNode.tile.pos;
        int dcol = (int)Mathf.Abs(startPos.x - endPos.x);
        int drow = (int)Mathf.Abs(startPos.y - endPos.y);

        return drow + Mathf.Max(0, (dcol - drow) / 2);
    }

    private List<Vector2> InterpolateLine(HexTileNode startNode, HexTileNode endNode, int distance)
    {
        List<Vector2> line = new List<Vector2>();
        Vector2 startPoint = startNode.tile.pos;
        float startX = startPoint.x;
        float startY = startPoint.y;

        Vector2 endPoint = endNode.tile.pos;
        float endX = endPoint.x;
        float endY = endPoint.y;

        for (int i = 0; i <= distance; i++)
        {
            int interp = i / distance;
            float x = startX + (endX - startX) * interp;
            float y = startY + (endY - startY) * interp;
            Vector2 point = new Vector2(x, y);

            line.Add(CubeToDoubleWidth(CubeRound(DoublewidthToCube(point))));
        }

        return line;
    }


    private Vector3 DoublewidthToCube(Vector2 point)
    {
        float q = (point.x - point.y) / 2;
        float r = point.y;

        return new Vector3(q, r, q - r);
    }

    private Vector2 CubeToDoubleWidth(Vector3 cube)
    {
        var col = 2 * cube.x + cube.y;
        var row = cube.y;

        return new Vector2(col, row);
    }

    private Vector3 CubeRound(Vector3 frac)
    {

        var x = Mathf.Round(frac.x);
        var y = Mathf.Round(frac.y);
        var z = Mathf.Round(frac.z);

        var x_diff = Mathf.Abs(x - frac.x);
        var y_diff = Mathf.Abs(y - frac.y);
        var z_diff = Mathf.Abs(z - frac.z);

        if (x_diff > y_diff && x_diff > z_diff)
        {
            x = -y - z;
        }
        else if (y_diff > z_diff)
        {
            y = -x - z;
        }
        else
        {
            z = -x - y;
        }

        return new Vector3(x, y, z);
    }


    public List<Vector2> GetShortestPath(HexTileNode start, HexTileNode end)
    {
        return DijkstraShortestPath(start, end);
    }


    //WARNING: this algorithm can return a suboptimal if the costs are associated to the vertes instead of the node 
    private List<Vector2> DijkstraShortestPath(HexTileNode start, HexTileNode end)
    {
        List<Vector2> shortestPath = new List<Vector2>();
        PriorityQueue<HexTileNode, int> frontier = new PriorityQueue<HexTileNode, int>(0);
        frontier.Insert(start, 0);
        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();
        Dictionary<Vector2, int> costSoFar = new Dictionary<Vector2, int>();

        Vector2 startPos = start.tile.pos;
        Vector2 endPos = end.tile.pos;
        cameFrom.Add(startPos, startPos);
        costSoFar.Add(startPos, 0);


        while (!frontier.isEmpty() && !cameFrom.ContainsKey(endPos))
        {
            HexTileNode current = frontier.Pop();
            Vector2 currentPos = current.tile.pos;

            foreach (HexTileNode neighbor in current.adjacentTiles)
            {
                Vector2 neighborPos = neighbor.tile.pos;
                int newCost = costSoFar[currentPos] + neighbor.traverseCost;
                if (!costSoFar.ContainsKey(neighborPos))
                {
                    costSoFar.Add(neighborPos, newCost);
                    frontier.Insert(neighbor, newCost);
                    cameFrom.Add(neighborPos, currentPos);
                }
                else if (costSoFar[neighborPos] > newCost)
                {
                    costSoFar[neighborPos] = newCost;
                    frontier.Insert(neighbor, newCost);
                    cameFrom[neighborPos] = currentPos;
                }
            }
        }

        if (cameFrom.ContainsKey(endPos))
        {
            shortestPath = CreatePath(cameFrom, endPos);
        }

        return shortestPath;
    }

    private List<Vector2> CreatePath(Dictionary<Vector2, Vector2> cameFrom, Vector2 endPos)
    {
        List<Vector2> path = new List<Vector2>();
        Vector2 currentPos = endPos;
        path.Add(endPos);

        while (cameFrom[currentPos] != currentPos)
        {
            path.Add(currentPos);
            currentPos = cameFrom[currentPos];
        }

        path.Reverse();
        return path;
    }
}