using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

public class WeaponHolder : MonoBehaviour
{
    [Inject] private PlayerInput _input;

    [SerializeField] private int _currentWeaponIndex;
    private List<Weapon> _weapons = new();
    private Weapon _currentWeapon;

    public UnityAction<Weapon> OnWeaponChange;

    private bool _readyChange = false;
    private bool _shooting = false;

    public bool IsPaused = false;

    private void Start() 
    {
        Weapon[] weapons = GetComponentsInChildren<Weapon>();

        foreach(var weapon in weapons)
        {
            if(PlayerPrefs.GetInt(weapon.ID.ToString()) == 1 || weapon is Hurl)
            {
                weapon.gameObject.SetActive(true);
                Debug.Log(weapon.name + "ACTIVE");
            }
            else
            {
                weapon.gameObject.SetActive(false);
                Debug.Log(weapon.name + "INACTIVE");
            }
        }

        Weapon[] weaponChilds = GetComponentsInChildren<Weapon>();
        
        _currentWeapon = weaponChilds[Mathf.Clamp(_currentWeaponIndex, 0, weaponChilds.Length)];

        OnWeaponChange?.Invoke(_currentWeapon);
        _currentWeapon.OnWeaponEndHidding += OnCurrentWeaponEndHidding;

        for(int i = 0; i < weaponChilds.Length; i++)
        {
            if(weaponChilds[i] != null)
                _weapons.Add(weaponChilds[i]);

            if(_weapons[i] != _currentWeapon)
                _weapons[i].gameObject.SetActive(false);
        }



        Debug.Log(_weapons);
    }

    private void OnEnable()
    {
        _input.Player.Shoot.started += context => StartShooting();
        _input.Player.Shoot.canceled += context => EndShooting();
        _input.Player.Reload.performed += context => WeaponReload();
        _input.Player.Change.performed += context => StartCoroutine(ChangeWeapon());

        OnWeaponChange?.Invoke(_currentWeapon);
    }

    private void OnDestroy()
    {
        _input.Player.Shoot.started -= context => StartShooting();
        _input.Player.Shoot.canceled -= context => EndShooting();
        _input.Player.Reload.performed -= context => WeaponReload();
        _input.Player.Change.performed -= context => StartCoroutine(ChangeWeapon());

        _input.Disable();
        
    }

    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }


    private IEnumerator ChangeWeapon()
    {
        if(_weapons.Count > 1)
        {
            Debug.Log("ChangeStart");
            int index = 0;

            StartCoroutine(_currentWeapon.Hide());
            if(_currentWeaponIndex + 1 < _weapons.Count)
            {
                yield return new WaitUntil(() => _readyChange);

                index = _currentWeaponIndex + 1;
            }
            else
            {
                yield return new WaitUntil(() => _readyChange);
            }
            _currentWeapon.gameObject.SetActive(false);

            ChangeCurrentWeapon(index);

            _readyChange = false;

            Debug.Log("ChangeEnd");
        }
    }

    private void ChangeCurrentWeapon(int index)
    {
        if(_currentWeapon != null)
            _currentWeapon.OnWeaponEndHidding -= OnCurrentWeaponEndHidding;

        _currentWeapon = _weapons[index];
        _currentWeaponIndex = index;

        _currentWeapon.gameObject.SetActive(true);

        OnWeaponChange?.Invoke(_currentWeapon);
        _currentWeapon.OnWeaponEndHidding += OnCurrentWeaponEndHidding;
    }

    private void OnCurrentWeaponEndHidding()
    {
        _readyChange = true;
    }

    private void StartShooting()
    {
        Debug.Log("HolderTry");

        _shooting = true;
        if(!IsPaused)
            StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while(_shooting)
        {
            _currentWeapon.TryAction();

            yield return null;
        }
    }

    private void EndShooting()
    {
        _shooting = false;
    }



    private void WeaponReload()
    {
        if(_currentWeapon as Firearm)
        {
            var weapon = _currentWeapon as Firearm;
            weapon.TryReload();
        }
        else if(_currentWeapon as GrenadeGun)
        {
            var weapon = _currentWeapon as GrenadeGun;
            weapon.TryReload();
        }

    }
}
