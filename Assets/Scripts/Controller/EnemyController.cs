using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitController
{
    private bool _isSelected = true;
    private Enemy _enemy;
    
    public Enemy Enemy
    {
        get => _enemy;
        set => Enemy = value;
    }
    
    
    public bool IsSelected 
    {
    get => _isSelected;
    }
    
    
    void Awake()
    {
        Locator.GetEnemyHandler().AddEnemy(this);
    }
    
}
