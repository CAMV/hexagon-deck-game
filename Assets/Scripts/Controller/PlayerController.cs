using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitController
{

    private bool _isSelected = true;
    private Player _player;

    public Player Player
    {
        get => _player;
        set => Player = value;
    }


    public bool IsSelected 
    {
        get => _isSelected;
    }


    void Awake()
    {
        Locator.ProvidePlayerController(this);
    }

}
