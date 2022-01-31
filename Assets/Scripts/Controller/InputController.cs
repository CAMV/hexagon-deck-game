using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray cubeRay = Locator.GetTextureCamera().camera.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = LayerMask.GetMask("Terrain");
            RaycastHit cubeHit;
            if (Physics.Raycast(cubeRay, out cubeHit, 10, layerMask))
            {
                TriggerHexTileClick(cubeHit.transform);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            //DebugCheckWalls();
        }
    }

    private void DebugCheckWalls()
    {
        Debug.Log("clicky");
        Ray cubeRay = Locator.GetTextureCamera().camera.ScreenPointToRay(Input.mousePosition);
        LayerMask layerMask = LayerMask.GetMask("Terrain");
        RaycastHit cubeHit;
        if (Physics.Raycast(cubeRay, out cubeHit, 10, layerMask))
        {
            Locator.GetHexTileManager().CheckWalls(cubeHit.collider.transform.GetComponent<HexTileNode>());
        }
    }

    void TriggerHexTileClick(Transform transform) {
        Debug.Log("i click");
        HexTileNode node = transform.GetComponent<HexTileNode>();

        if (node != null) {
            node.TriggerNodeClick();
            Debug.Log("node in pos " + node.tile.pos);
        }

    }
}
