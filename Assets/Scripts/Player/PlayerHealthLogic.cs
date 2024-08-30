using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHealthLogic : HealthLogic
{
    [Inject] private Player _player;
    private void Awake() 
    {
        OperationsInAwake();
    }

    protected override void Die()
    {
        Time.timeScale = 0f;
        _player.Die();
    }
}
