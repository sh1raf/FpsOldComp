using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Camera fpsCam;
    [SerializeField] private LayerMask layer;

    private Image _image;

    private void Awake() 
    {
        _image = GetComponent<Image>();
    }

    private void Update() 
    {
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hitInfo, 150f, layer))
        {
            if(hitInfo.collider.GetComponentInParent<HealthLogic>())
                _image.color = Color.red;
            else
                _image.color = Color.white;
        }
        else
            _image.color = Color.white;
    }

    private void OnDrawGizmos() 
    {
        Ray ray = new(fpsCam.transform.position, fpsCam.transform.forward);
    }
}
