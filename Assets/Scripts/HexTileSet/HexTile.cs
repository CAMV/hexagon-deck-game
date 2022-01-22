using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile
{
    public Vector2 center;
    public float size;
    public float width { get; private set; }
    public float height { get; private set; }
    Vector2[] corners = new Vector2[6];

    public HexTile(Vector2 center, float size)
    {
        this.center = center;
        this.size = size;
    }


    public void CreatePointyToppedHex()
    {
        if (center != null && size > 0)
        {
            FillCorners(30);
            height = 2 * size;
            width = Mathf.Sqrt(3) * size;
        }
    }


    private void FillCorners(int degreeStart)
    {
        for (int i = 0; i < 6; i++)
        {
            corners[i] = GetHexCorner(center, size, i, degreeStart);
        }
    }


    private Vector2 GetHexCorner(Vector2 center, float size, int i, int degreeStart)
    {
        int angleDeg = 60 * i - degreeStart;
        float angleRad = Mathf.PI * angleDeg / 180;

        return new Vector2(center.x + size * Mathf.Cos(angleRad),
                 center.y + size * Mathf.Sin(angleRad));
    }
}