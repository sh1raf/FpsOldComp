using System;
using UnityEngine;
using System.Collections;
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] protected float force;
    [SerializeField] protected int damage;

    [SerializeField] protected float actionTime;
    [SerializeField] protected float getOutTime;
    [SerializeField] protected float hideTime;

    [SerializeField] protected LayerMask layer;

    [field: SerializeField] public int ID {get; private set;}

    public event Action OnWeaponEndHidding;
    public event Action<int> OnCurrentAmmoUpdate;
    public event Action<int> OnFreeAmmoUpdate;

    protected Animator _animator;
    protected Muzzle _muzzle;

    protected bool _canAction = false;

    protected IEnumerator WaitBeforeCanUse()
    {
        yield return new WaitForSeconds(actionTime);

        _canAction = true;
    }

    protected void WeaponEndHidding()
    {
        OnWeaponEndHidding?.Invoke();
    }

    protected void CurrentAmmoUpdate(int value)
    {
        OnCurrentAmmoUpdate?.Invoke(value);
    }

    protected void FreeAmmoUpdate(int value)
    {
        OnFreeAmmoUpdate?.Invoke(value);
    }

    public virtual IEnumerator Hide()
    {
        yield return null;
    }

    
    protected virtual IEnumerator GetOut()
    {
        yield return null;
    }

    public virtual bool TryAction()
    {
        return false;
    }
}