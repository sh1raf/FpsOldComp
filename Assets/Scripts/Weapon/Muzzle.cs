using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Plugins.Audio.Core;

public class Muzzle : MonoBehaviour 
{
    [SerializeField] private float delayTime;
    [SerializeField] private ParticleSystem shootEffect;
    [SerializeField] private SourceAudio sound;
    [SerializeField] private string shootClip;
    [SerializeField] private string getOutClip;

    private Animator _animator;

    private void Awake() 
    {
        if(GetComponent<Animator>() != null)
            _animator = GetComponent<Animator>();
        if(shootEffect!=null)
            shootEffect.gameObject.SetActive(false);
    }

    [Button]
    public void ShootEffect()
    {
        if(shootEffect == null || sound == null || _animator == null)
            return;
        shootEffect.gameObject.SetActive(true);
        StartCoroutine(DisableOnTimer());
        _animator.SetTrigger("Shoot");

        sound.Pitch = Random.Range(0.92f, 1.08f);
        sound.PlayOneShot(shootClip);

        Debug.Log(sound);
    }

    public void ShootEffectAwake()
    {
        if (sound == null || _animator == null)
            return;

        _animator.SetTrigger("Shoot");

        sound.Pitch = Random.Range(0.92f, 1.08f);
        sound.PlayOneShot(shootClip);

        Debug.Log(sound);
    }

    public void GetOutEffect()
    {
        if (sound == null)
            return;

        sound.Pitch = 1f;
        sound.PlayOneShot(getOutClip);

        Debug.Log(sound);
    }

    private IEnumerator DisableOnTimer()
    {
        yield return new WaitForSeconds(delayTime);

        shootEffect.gameObject.SetActive(false);
    }
}