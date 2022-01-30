using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    List<EnemyController> _enemies = new List<EnemyController>();
    private void Awake()
    {
        Locator.ProvideEnemyHandler(this);
    }

    public void AddEnemy(EnemyController enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        _enemies.Remove(enemy);
    }
}
