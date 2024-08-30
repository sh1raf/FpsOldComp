using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Image panel;
    
    private void Awake()
    {
        FindObjectOfType<Finish>().MazeComplete += Activate;
    }

    private void Activate()
    {
        panel.gameObject.SetActive(true);
    }

    public void Disactivate()
    {
        panel.gameObject.SetActive(false);
    }
}
