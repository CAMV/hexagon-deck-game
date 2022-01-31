using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitController
{

    private bool _isSelected = false;
    private Player _player;

    public Player Player
    {
        get => _player;
        set => Player = value;
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => _isSelected = value;
    }


    void Awake()
    {
        base.Awake();
        Locator.ProvidePlayerController(this);
    }

}
