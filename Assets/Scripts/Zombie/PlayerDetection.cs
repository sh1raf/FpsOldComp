using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerDetection : MonoBehaviour
{
    private ZombieAI _zombie;
    private void Awake() 
    {
        _zombie = GetComponentInParent<ZombieAI>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<Player>())
            _zombie.PlayerInRange();
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
            _zombie.PlayerOutOfRange();
    }
}
