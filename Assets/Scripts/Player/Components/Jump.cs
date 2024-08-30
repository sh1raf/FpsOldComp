using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;

    [Inject] private PlayerInput _input;


    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody>();

        _input.Player.Jump.performed += context => Jumping();
    }

    private void Jumping()
    {
        // Jump when the Jump button is pressed and we are on the ground.
        if(!groundCheck || groundCheck.isGrounded)
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}
