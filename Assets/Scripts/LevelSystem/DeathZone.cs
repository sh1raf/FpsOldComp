using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<HealthLogic>())
        {
            other.GetComponent<HealthLogic>().TakeDamage(100000);
            Debug.Log("HYAK");
        }
    }
}
