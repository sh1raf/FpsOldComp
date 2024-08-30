using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpDescription;
    [SerializeField] private TMP_Text tmpCost;
    private RawImage _previousImage;

    private void Awake()
    {
        tmpDescription.text = "";
        tmpCost.text = "";
    }


    public void SetDescription(string textDescription, int cost, RawImage image)
    {
        if(_previousImage != null)
            _previousImage.gameObject.SetActive(false);

        tmpDescription.text = textDescription;
        tmpCost.text = cost.ToString();
        image.gameObject.SetActive(true);
        _previousImage = image;
    }
}
