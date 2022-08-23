using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Interactable : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public CanvasGroup lineViewer;

    public GameObject MainCam;
    public GameObject interactableCanvas;

    private PlayerInputs playerInputs;

    private bool finishedTalking = false;

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
    public void DialoguedFinsihed()
    {
        finishedTalking = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (lineViewer.alpha <= 0 && !finishedTalking && !dialogueRunner.IsDialogueRunning)
            {
                interactableCanvas.SetActive(true);
            }
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
                
                if (lineViewer.alpha <= 0 && !finishedTalking && !dialogueRunner.IsDialogueRunning)
                {   //Floating ui turned off
                    interactableCanvas.SetActive(false);

                    //Starts running dialouge if dialouge ui is off and not finished talking
                    dialogueRunner.StartDialogue(dialogueRunner.startNode);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableCanvas.SetActive(false);
            //dialogueRunner.OnViewRequestedInterrupt();
            if (dialogueRunner.IsDialogueRunning && !finishedTalking)
            {
                dialogueRunner.Stop();
            }
        }
    }
}
