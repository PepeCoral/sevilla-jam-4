using System.Diagnostics.Contracts;
using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    Vector2 moveDirection;
    PlayerInput playerInput;

    [SerializeField] float maxInclination;
    [SerializeField] float inclinationOutRate;
    [SerializeField] float inclinationInRate;

    [SerializeField] Transform visualCharacter; 
    private float currentInclination;

    [SerializeField] float stepMagnitude;
    [SerializeField] float stepRate;



    PlayerController controller;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        playerInput.Enable();
        if (controller.isPlayer1)
        {
            playerInput.Player1.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
            playerInput.Player1.Move.canceled += ctx => moveDirection = Vector2.zero;
        }
        else
        {
            playerInput.Player2.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
            playerInput.Player2.Move.canceled += ctx => moveDirection = Vector2.zero;
        }
    }
    private void OnDisable()
    {
        playerInput.Disable();
        if (controller.isPlayer1)
        {
            playerInput.Player1.Move.performed -= ctx => moveDirection = ctx.ReadValue<Vector2>();
            playerInput.Player1.Move.canceled -= ctx => moveDirection = Vector2.zero;
        }
        else
        {
            playerInput.Player2.Move.performed -= ctx => moveDirection = ctx.ReadValue<Vector2>();
            playerInput.Player2.Move.canceled -= ctx => moveDirection = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        float moveSign = Mathf.Sign(moveDirection.x);

        if (moveDirection.x == 0) moveSign = 0;

        if (moveSign == 0 && currentInclination != 0)
        {

            currentInclination += -Mathf.Sign(currentInclination) * inclinationInRate;
        }

        else
        {

            currentInclination += moveSign * inclinationOutRate;
        }

        currentInclination = Mathf.Clamp(currentInclination, -maxInclination, maxInclination);

        transform.localRotation = Quaternion.Euler(0, 0, currentInclination);

        if(Mathf.Abs(currentInclination) < 1.5f)
        {
            currentInclination = 0;
        }


        if (moveDirection.magnitude != 0) {
        
            visualCharacter.localPosition = new Vector2(0, Mathf.Abs(Mathf.Sin(Time.time*stepRate)) * stepMagnitude);
        }
        else
        {
            visualCharacter.localPosition = Vector2.zero;
        }

    }
}
