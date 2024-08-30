using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private Transform body;

    [SerializeField]
    Transform character;
    public float mobileSensitivity = 2;
    public float sensitivity = 0.4f;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;

    [Inject] private PlayerInput _input;

    private float _xRotation;


    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(_input.Player.MouseDelta.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector2 rawFrameVelocity = Vector2.Scale(_input.Player.MouseDelta.ReadValue<Vector2>(), Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);
        
            //Rotate camera up-down and controller left-right from velocity.
           transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
           character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        }
        else if(_input.Player.Look.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector2 rawFrameVelocity = Vector2.Scale(_input.Player.Look.ReadValue<Vector2>(), Vector2.one * mobileSensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
            transform.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
            //transform.Rotate(new Vector3(sensitivity * rotation.y * 0, sensitivity * rotation.x, sensitivity * rotation.y));
        }
    }
}
