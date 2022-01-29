using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _isSelected = false;
    private HexTileNode _standingNode;

    public HexTileNode StandingNode 
    {
        get => _standingNode;
    }

    public bool IsSelected 
    {
        get => _isSelected;
    }


    // Start is called before the first frame update
    void Awake()
    {
        Locator.ProvidePlayerController(this);
    }

    public void Move(List<Vector2> path) {
        // ??
        HexTileManager tileManager = Locator.GetHexTileManager();
        _standingNode = tileManager.GetNode(path[path.Count - 1]);
    }
    
}
