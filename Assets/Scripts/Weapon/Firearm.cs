using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.ProBuilder.Shapes;

public class Firearm : Weapon
{
    [SerializeField] private int headMulti;
    [SerializeField] private int bodyMulti;

    [SerializeField] private int maxAmmoInMagazine;
    [SerializeField] private int maxAmmo;

    [SerializeField] private float reloadTime;

    [SerializeField] private GameObject shootSolidImpact;
    [SerializeField] private GameObject shootBloodImpact;

    private int _currentAmmo;
    private int _freeAmmo;

    private bool _isReloading = false;
    private bool _canReloading = true;
    private bool _isChanging = false;

    private void Awake()
    {
        _freeAmmo = maxAmmo;
        _currentAmmo = maxAmmoInMagazine;

        _muzzle = GetComponent<Muzzle>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(GetOut());
    }

    protected override IEnumerator GetOut()
    {
        _animator.SetTrigger("GetOut");
        _muzzle.GetOutEffect();

        yield return new WaitForSeconds(getOutTime);

        _canAction = true;
        _canReloading = true;
        _isChanging = false;

        StartCoroutine(UpdateAmmo());

        CurrentAmmoUpdate(_currentAmmo);
        FreeAmmoUpdate(_freeAmmo);

    }

    protected IEnumerator UpdateAmmo()
    {
        yield return new WaitForSeconds(1f);
        CurrentAmmoUpdate(_currentAmmo);
        FreeAmmoUpdate(_freeAmmo);
    }

    public bool TryReload()
    {
        if(_freeAmmo > 0 && _currentAmmo < maxAmmoInMagazine && !_isReloading && _canReloading)
        {
            StartCoroutine(Reload());
            return true;
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
        else if(_canAction && !_isReloading && !_isChanging)
        {
            StartCoroutine(Shoot());
            return true;
        }
        return false;
    }
    public IEnumerator Reload()
    {
        _isReloading = true;

        _animator.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);

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

    private IEnumerator Shoot()
    {
        _canAction = false;

        _muzzle.ShootEffect();
        _currentAmmo--;
        CurrentAmmoUpdate(_currentAmmo);

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, range, layer))
        {

            if(hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Zombie"))
            {
                if(hitInfo.collider.GetComponentInParent<HealthLogic>().TryGetComponent(out HealthLogic health))
                {
                    if(hitInfo.collider.TryGetComponent(out Rigidbody rb))
                        rb.AddForce(-hitInfo.normal * force);

                    int currentDamage = damage;

                    switch(hitInfo.collider.tag)
                    {
                        case("Head"):
                        {
                            currentDamage *= headMulti;
                            break;
                        }
                        case("Body"):
                        {
                            currentDamage *= bodyMulti;
                            break;
                        }
                    }
                    health.TakeDamage(currentDamage);
                    Debug.Log(damage);
                }

                var impact = Instantiate(shootBloodImpact, hitInfo.point, Quaternion.identity);
                Destroy(impact, 2f);
            }
            else if(hitInfo.collider.tag == "Solid" || hitInfo.collider.tag == "Ground")
            {
                var impact = Instantiate(shootSolidImpact, hitInfo.point, Quaternion.identity);
                Destroy(impact, 2f);
            }
        }
        Debug.Log("Shoot");
        
        yield return new WaitForSeconds(actionTime);

        _canAction = true;
    }

    private void OnDrawGizmos() 
    {
        Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);
        Gizmos.DrawRay(ray);
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
