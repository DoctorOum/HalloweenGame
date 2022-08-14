using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject MainCam;
    public GameObject interactableCanvas;

    private PlayerInputs playerInputs;

    private void OnEnable()
    {
        playerInputs.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Disable();
    }

    private void Awake()
    {
        interactableCanvas.SetActive(false);
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");
        playerInputs = new PlayerInputs();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableCanvas.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (interactableCanvas != null)
            {
                interactableCanvas.transform.LookAt(MainCam.transform);
            }

            if (playerInputs.UI.Interact.IsPressed())
            {
                interactableCanvas.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableCanvas.SetActive(false);
        }
    }
}
