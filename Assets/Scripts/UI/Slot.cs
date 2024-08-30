using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class Slot : MonoBehaviour
{
    [Inject] private Shop _shop;

    [SerializeField] private Image bounds;

    [field: SerializeField] public int Cost {get; private set;}
    [field: SerializeField] public TMP_Text Description {get; private set;}
    [field: SerializeField] public RawImage Image {get; private set;}
    [field: SerializeField] public Weapon WeaponPrefab;
    [field: SerializeField] public bool IsBought {get; private set;} = false;
    public int Id {get {return WeaponPrefab.ID;} private set{}}

    private void Awake()
    {
        if (IsBought)
            PlayerPrefs.SetInt(Id.ToString(), 1);

        if(PlayerPrefs.HasKey(Id.ToString()))
        {
            if(PlayerPrefs.GetInt(Id.ToString()) == 1)
            {
                IsBought = true;
            }
        }
    }

    public void OnClick()
    {
        _shop.DeselectAll(this);
    }

    public bool TryBuy()
    {
        if(Cost <= PlayerPrefs.GetInt("Goals") && !IsBought)
        {
            PlayerPrefs.SetInt("Goals",PlayerPrefs.GetInt("Goals") - Cost);
            PlayerPrefs.SetInt(Id.ToString(), 1);
            IsBought = true;
            _shop.WaultChange();

            return true;
        }

        return false;
    }

    public void Select()
    {
        bounds.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        bounds.gameObject.SetActive(false);
    }
}
