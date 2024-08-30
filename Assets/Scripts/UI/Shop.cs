using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using Plugins.Audio.Core;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Button boughtButton;

    [SerializeField] private new SourceAudio audio;
    [SerializeField] private string coinsClip;

    [Inject] private Description _description;

    private List<Slot> _shopSlots = new();
    public event Action OnWaultChange;

    private Slot _currentSlot;

    private void Awake() 
    {
        _shopSlots.AddRange(GetComponentsInChildren<Slot>());

        DeselectAll(_shopSlots[0]);
    }

    public void DeselectAll()
    {
        foreach(var slot in _shopSlots)
            slot.Deselect();
    }

    public void DeselectAll(Slot slot)
    {
        DeselectAll();
            
        _currentSlot = slot;
        _currentSlot.Select();

        _description.SetDescription(_currentSlot.Description.text, _currentSlot.Cost, _currentSlot.Image);

        if(_currentSlot.IsBought)
        {
            buyButton.gameObject.SetActive(false);
            boughtButton.gameObject.SetActive(true);
        }        
        else
        {
            buyButton.gameObject.SetActive(true);
            boughtButton.gameObject.SetActive(false);
        }
    }

    public void WaultChange()
    {
        OnWaultChange?.Invoke();
    }

    private void AddWeapon(Weapon weapon)
    {

    }

    public void Buy()
    {
        if(_currentSlot.TryBuy())
        {
            audio.PlayOneShot(coinsClip);
        }
    }
}
