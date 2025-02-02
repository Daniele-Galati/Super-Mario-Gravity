using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    [NonSerialized] public Vector2 move;
    private CharacterMovement movement;

    private void Start()
    {
        movement = GetComponent<CharacterMovement>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            movement.JumpRequested();
    }
}
