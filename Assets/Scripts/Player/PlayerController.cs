using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float aceleration = 0;
    private Rigidbody2D rb;
    Vector2 moveDirection;
    PlayerInput controlPlayer;
    [SerializeField] public bool isPlayer1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controlPlayer = new PlayerInput();
    }
    private void OnEnable()
    {
        controlPlayer.Enable();
        if (isPlayer1)
        {
            controlPlayer.Player1.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
            controlPlayer.Player1.Move.canceled += ctx => moveDirection = Vector2.zero;
        }
        else
        {
            controlPlayer.Player2.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
            controlPlayer.Player2.Move.canceled += ctx => moveDirection = Vector2.zero;
        }
    }
    private void OnDisable()
    {
        controlPlayer.Disable();
        if (isPlayer1)
        {
            controlPlayer.Player1.Move.performed -= ctx => moveDirection = ctx.ReadValue<Vector2>();
            controlPlayer.Player1.Move.canceled -= ctx => moveDirection = Vector2.zero;
        }
        else
        {
            controlPlayer.Player2.Move.performed -= ctx => moveDirection = ctx.ReadValue<Vector2>();
            controlPlayer.Player2.Move.canceled -= ctx => moveDirection = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {

        if (isPlayer1) 
        {
            if (Gamepad.all.Count > 0 ) {
                moveDirection = Gamepad.all[0].leftStick.value;
                    }
        }
        else
        {
            if (Gamepad.all.Count > 1)
            {
                moveDirection = Gamepad.all[1].leftStick.value;
            }
        }

        rb.linearVelocity = moveDirection * aceleration;

    }
}
