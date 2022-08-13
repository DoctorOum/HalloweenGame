using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private PlayerInputs playerInput;

    public float playerSpeed;

    private void Awake()
    {
        playerInput = new PlayerInputs();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput?.Disable();
    }

    void FixedUpdate()
    {
        Vector2 direction = playerInput.PlayerMovement.Move.ReadValue<Vector2>();
        rb.velocity = new Vector3(direction.x, 0, direction.y) * playerSpeed;
    }
}
