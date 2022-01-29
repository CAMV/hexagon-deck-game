using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexComponent : MonoBehaviour
{
    private HexTileNode _hexTileNode;

    public HexTileNode HexTileNode
    {
        get
        {
            return _hexTileNode;
        }
        set
        {
            _hexTileNode = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
