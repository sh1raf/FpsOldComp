using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalsCollector : MonoBehaviour
{
    private Wallet _wallet;
    private void Awake() 
    {
        _wallet = GetComponent<Wallet>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.TryGetComponent(out PowerUp up))
        {
            up.Pickup();
            _wallet.AddCoin();
        }
    }
}
