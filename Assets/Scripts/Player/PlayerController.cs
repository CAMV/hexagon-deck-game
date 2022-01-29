using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static Vector3 ELEVATION = new Vector3(0, 0.35f, 0);

    private bool _isSelected = true;
    private HexTileNode _standingNode;

    public HexTileNode StandingNode 
    {
        get => _standingNode;
        // set => _standingNode = value;
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

    void Start() 
    {
        SetCurrentNode();
    }

    public void Move(List<Vector2> path) {
        // ??
        HexTileManager tileManager = Locator.GetHexTileManager();
        Vector2 newNodePos = path[path.Count - 1];
        HexTileNode newNode = tileManager.GetNode(path[path.Count - 1]);
        Debug.Log("moving player to " + newNode.tile.center);
        transform.position = newNode.tile.center;
    }


    private void SetCurrentNode() 
    {
        HexTileNode node = this.transform.parent.GetComponent<HexTileNode>();
        _standingNode = node;

        transform.position = node.transform.parent.position;

    }
}
