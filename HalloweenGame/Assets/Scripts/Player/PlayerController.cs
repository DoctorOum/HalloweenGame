using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private PlayerInputs playerInput;

    public float playerSpeed;

    public GameObject PixelRenderCam;

    public Animator animator;

    private void Awake()
    {
        playerInput = new PlayerInputs();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput.UI.SwapPixelCamera.performed += _ => SwapCameras();
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
        //Gets input from player and converts into a vector
        Vector2 direction = playerInput.PlayerMovement.Move.ReadValue<Vector2>();
        rb.velocity = new Vector3(direction.x, 0, direction.y) * playerSpeed;
        direction = direction.normalized;

        //if moving rotate character
        if (rb.velocity.magnitude >= 0.1)
        {
            animator.SetBool("isMoving", true);
            //Handles directionals
            if (direction.x != 0 && direction.y == 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, direction.x * -90, 0));
            }
            else if(direction.y != 0 && direction.x == 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, (direction.y + 1) * 90, 0));
            }
            //Handles diagonals
            else if(direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, -90 + direction.y * -45 , 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 90 + direction.y * 45, 0));
            }
            //transform.rotation = Quaternion.Euler(new Vector3(0, (direction.y > 0 ? direction.y * 180 : 0) + (direction.x < 0 ? 90 : direction.x > 0 ? -90 : 0 ), 0));
            //transform.rotation = Quaternion.Euler(new Vector3(0, Vector3.Dot(new Vector3(direction.x,0,0),new Vector3(0, direction.y, 0)))) ;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void SwapCameras()
    {
        if (!PixelRenderCam.activeSelf)
        {
            PixelRenderCam.SetActive(true);
        }
        else if(PixelRenderCam.activeSelf)
        {
            PixelRenderCam.SetActive(false);
        }
    }
}
