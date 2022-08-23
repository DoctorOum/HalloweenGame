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
    public float turnSpeed = 0.5f;
    float timeCount = 0;

    public Yarn.Unity.DialogueRunner dialogueRunner;

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
        if (!dialogueRunner.IsDialogueRunning)
            rb.velocity = new Vector3(direction.x, 0, direction.y) * playerSpeed;


        //if moving && not in any dialogue rotate character
        if (rb.velocity.magnitude >= 0.1)
        {
            animator.SetBool("isMoving", true);
            //Handles directionals

            transform.rotation = Quaternion.Lerp(transform.rotation, NewRotation(direction), timeCount * turnSpeed);
            timeCount += Time.fixedDeltaTime;
            if (timeCount >= 1)
            {
                timeCount = 0;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    /// <summary>
    /// Gives back rotation based on current input direction
    /// </summary>
    /// <param name="direction">Vector2 of input from the player</param>
    /// <returns></returns>
    Quaternion NewRotation(Vector2 direction)
    {
        //Handles going right or left
        if (direction.x != 0 && direction.y == 0)
        {
            Quaternion newRotation = Quaternion.Euler(new Vector3(0, direction.x * -90, 0));
            return newRotation;
        }
        //handles going forward/up
        else if (direction.y != 0 && direction.x == 0)
        {
            Quaternion newRotation = Quaternion.Euler(new Vector3(0, (direction.y + 1) * 90, 0));
            return newRotation;
        }
        //Handles diagonal right, top or bottom
        else if (direction.x > 0)
        {
            Quaternion newRotation = Quaternion.Euler(new Vector3(0, -90 + direction.y * -45, 0));
            return newRotation;
        }
        //handles going diagonal left, top or bottom
        else
        {
            Quaternion newRotation = Quaternion.Euler(new Vector3(0, 90 + direction.y * 45, 0));
            return newRotation;
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
