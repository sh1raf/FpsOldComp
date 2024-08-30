using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text currentAmmoTMP;
    [SerializeField] private TMP_Text freeAmmoTMP;

    private Weapon _currentWeapon;
    [Inject] private WeaponHolder _holder;

    private void Awake()
    {
        _holder.OnWeaponChange += OnCurrentWeaponChange;
    }

    private void OnDisable() 
    {
        _holder.OnWeaponChange -= OnCurrentWeaponChange;
    }

    private void OnCurrentWeaponChange(Weapon weapon)
    {
        if(_currentWeapon != null)
        {
            _currentWeapon.OnCurrentAmmoUpdate -= OnCurrentAmmoUpdate;
            _currentWeapon.OnFreeAmmoUpdate -= OnFreeAmmoUpdate;
        }
        Debug.Log("EVENTED");
        _currentWeapon = weapon;

        if(weapon is Hurl)
        {
            currentAmmoTMP.text = "0";
            freeAmmoTMP.text = "0";
        }
        else
        {
            weapon.OnCurrentAmmoUpdate += OnCurrentAmmoUpdate;
            weapon.OnFreeAmmoUpdate += OnFreeAmmoUpdate;
        }
    }

    private void OnFreeAmmoUpdate(int value)
    {
        freeAmmoTMP.text = value.ToString();
    }

    private void OnCurrentAmmoUpdate(int value)
    {
        currentAmmoTMP.text = value.ToString();
    }

    private IEnumerator UpdateFromTime()
    {
        yield return new WaitForSeconds(2);

        _holder.OnWeaponChange += OnCurrentWeaponChange;
    }
}

