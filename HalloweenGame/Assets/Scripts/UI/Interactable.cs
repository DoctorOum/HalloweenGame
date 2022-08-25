using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Interactable : MonoBehaviour
{
    public static Interactable Instance { get; private set; }

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
    private void OnDestroy()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
    }

    private void Awake()
    {   
        if(Instance = null)
        {
            Instance = this;
        }
        Instance.interactableCanvas.SetActive(false);
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");
        playerInputs = new PlayerInputs();
    }
    public void DialoguedFinsihed()
    {
        Instance.finishedTalking = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (lineViewer.alpha <= 0 && !Instance.finishedTalking && !dialogueRunner.IsDialogueRunning)
            {
                Instance.interactableCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Instance.interactableCanvas != null)
            {
                Instance.interactableCanvas.transform.LookAt(MainCam.transform);
            }

            if (playerInputs.UI.Interact.IsPressed())
            {
                
                if (lineViewer.alpha <= 0 && !finishedTalking && !dialogueRunner.IsDialogueRunning)
                {   //Floating ui turned off
                    Instance.interactableCanvas.SetActive(false);

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
            Instance.interactableCanvas.SetActive(false);
            //dialogueRunner.OnViewRequestedInterrupt();
            if (dialogueRunner.IsDialogueRunning && !finishedTalking)
            {
                dialogueRunner.Stop();
            }
        }
    }
}
