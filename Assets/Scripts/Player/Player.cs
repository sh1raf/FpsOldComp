using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform centerOfBody;
    public Transform CenterOfBody {get {return centerOfBody;} private set{}}

    private WeaponHolder _holder;
    private FirstPersonLook _fpLook;

    private RestartPanel _restart;

    private void Awake()
    {
        _holder = GetComponent<WeaponHolder>();
        _fpLook = GetComponentInChildren<FirstPersonLook>();
    }

    public void Pause()
    {
        _fpLook.enabled = false;
        _holder.IsPaused = true;
    }

    public void Unpause()
    {
        _fpLook.enabled = true;
        _holder.IsPaused = false;
    }

    public void Die()
    {
        _fpLook.enabled = false;
        _holder.IsPaused = true;
        _holder.EnableCursor();

        _restart = FindObjectOfType<RestartPanel>();
        _restart.Enable();
    }
}
