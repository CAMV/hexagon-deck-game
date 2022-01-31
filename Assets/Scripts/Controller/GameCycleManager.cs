using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycleManager : MonoBehaviour
{
    private EnemyHandler _enemyHandler;
    private PlayerController _playerController;
    private UnitController _nextUnit = null;
    bool nextTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        TurnController.ShuffleOrder();
        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartTurn()
    {
        _nextUnit = TurnController.NextUnit();
        if (_nextUnit.GetType() == typeof(PlayerController))
        {
            Debug.Log("Player turn");

            PlayerController player = (PlayerController)_nextUnit;
            player.IsSelected = true;
        }
    }

    void EndTurn()
    {
        StartTurn();
    }
}
