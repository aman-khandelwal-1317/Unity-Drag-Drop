using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{

    Vector2 playerInput;
    public float moveSpeed;

    [SerializeField] private GameObject playerParent;

    public void Move(InputAction.CallbackContext context)
    {
        playerInput=context.ReadValue<Vector2>();
    }

    private void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        Vector3 moveVector = playerInput.x * Vector3.right + playerInput.y * Vector3.forward;
        moveVector.Normalize();

        playerParent.transform.Translate(moveVector * moveSpeed * Time.deltaTime,Space.World);
    }

    public void ResetPosition()
    {
        gameObject.transform.position = Vector3.zero;
    }
}
