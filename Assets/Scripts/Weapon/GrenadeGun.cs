using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeGun : Weapon
{
    [SerializeField] private int maxAmmoInMagazine;
    [SerializeField] private int maxAmmo;

    [SerializeField] private float reloadTime;

    private int _currentAmmo;
    private int _freeAmmo;

    private bool _isReloading = false;
    private bool _canReloading = true;
    private bool _isChanging = false;

    private List<Grenade> _grenades = new();

    private int _currentGrenadeIndex = 0;

    private void Awake()
    {
        _freeAmmo = maxAmmo;
        _currentAmmo = maxAmmoInMagazine;
    }

    private void OnEnable()
    {
        _muzzle = GetComponent<Muzzle>();
        _animator = GetComponent<Animator>();

        if (_currentAmmo == 0 && _freeAmmo == 0)
            return;
        

        _grenades.AddRange(GetComponentsInChildren<Grenade>());

        foreach (var gren in _grenades)
            gren.gameObject.SetActive(false);

        _grenades[_currentGrenadeIndex].gameObject.SetActive(true);



        StartCoroutine(WaitBeforeCanUse());
    }

    private void Start()
    {
        StartCoroutine(GetOut());
    }

    public bool TryReload()
    {
        if(_freeAmmo > 0 && _currentAmmo < maxAmmoInMagazine && !_isReloading && _canReloading)
        {
            StartCoroutine(Reload());
            return true;
        }
        else if(_freeAmmo == 0 && _currentAmmo == 0)
        {
            CurrentAmmoUpdate(0);
            FreeAmmoUpdate(0);
            return false;
        }
        else
            return false;
    }

    public override bool TryAction()
    {
        if(_currentAmmo <= 0)
        {
            TryReload();
            return false;
        }
        else if(_canAction && !_isReloading && !_isChanging && _grenades.Count > _currentGrenadeIndex)
        {
            Shoot();
            return true;
        }
        return false;
    }
    public IEnumerator Reload()
    {
        _isReloading = true;

        _animator.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime / 2);

        _currentGrenadeIndex++;
        _grenades[_currentGrenadeIndex].gameObject.SetActive(true);

        yield return new WaitForSeconds(reloadTime / 2);

        if(_freeAmmo < maxAmmoInMagazine)
        {
            _currentAmmo = _freeAmmo;
            _freeAmmo = 0;
        }
        else
        {
            var surplus = maxAmmoInMagazine - _currentAmmo;

            _currentAmmo = maxAmmoInMagazine;
            _freeAmmo -= surplus;
        }

        CurrentAmmoUpdate(_currentAmmo);
        FreeAmmoUpdate(_freeAmmo);

        _isReloading = false;

        Debug.Log("Reloaded");
    }

    private void Shoot()
    {
        _muzzle.ShootEffectAwake();
        _currentAmmo--;
        CurrentAmmoUpdate(_currentAmmo);

        _grenades[_currentGrenadeIndex].transform.parent = null;
        _grenades[_currentGrenadeIndex].Launch();
        
        TryReload();
    }

    protected override IEnumerator GetOut()
    {
        _animator.SetTrigger("GetOut");
        _muzzle.GetOutEffect();

        yield return new WaitForSeconds(getOutTime);
        
        CurrentAmmoUpdate(_currentAmmo);
        FreeAmmoUpdate(_freeAmmo);

        _canAction = true;
        _canReloading = true;
        _isChanging = false;
    }

    public override IEnumerator Hide()
    {
        if(_isReloading)
            _isReloading = false;

        _canReloading = false;
        _isChanging = true;

        _animator.SetTrigger("Hide");

        yield return new WaitForSeconds(hideTime);

        WeaponEndHidding();
    }
}
