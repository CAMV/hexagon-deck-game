using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private static Vector3 ELEVATION = new Vector3(0, 0.35f, 0);
    private HexTileNode _standingNode;

    public HexTileNode StandingNode 
    {
        get => _standingNode;
        // set => _standingNode = value;
    }

    protected void Awake()
    {
        TurnController.SubscribeUnit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        HexTileNode node = this.transform.parent.GetComponent<HexTileNode>();
        node.IsOccupied = true;
        this._standingNode = node;
    }

    public void Move(List<Vector2> path) {
        // ??
        HexTileManager tileManager = Locator.GetHexTileManager();
        Vector2 newNodePos = path[path.Count - 1];
        HexTileNode newNode = tileManager.GetNode(path[path.Count - 1]);
        Debug.Log("moving player to " + newNode.tile.center);
        MoveToNode(newNode);
    }


    private void MoveToNode(HexTileNode node) 
    {
        _standingNode.ToggleOccupied();

        transform.parent = node.transform;
        transform.position = node.transform.position + ELEVATION;
        _standingNode = node;

        node.ToggleOccupied();
    }

    public void EndTurn()
    {
        Locator.GetGameCycleManager().EndTurn();
    }
}
