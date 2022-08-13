using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject MainCam;
    public GameObject interactableCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interactableCanvas != null)
        {
            interactableCanvas.transform.LookAt(MainCam.transform);
        }
    }
}
