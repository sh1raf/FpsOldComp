using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthLogic : HealthLogic
{
    [SerializeField] private List<GameObject> destroyOnDie = new();
    private void Awake() 
    {
        OperationsInAwake();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        GetComponent<ZombieAI>().NeedHunting();
    }

    protected override void Die()
    {
        Debug.Log("ZombieDie");
        GetComponent<ZombieAI>().RagdollOn();

        foreach(var go in destroyOnDie)
            Destroy(go);
    }
}
