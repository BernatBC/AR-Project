using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DialogSystem : MonoBehaviour
{
    public GameObject canvas;

    private ARRaycastManager _raycastManager;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();


    private void Start()
    {
        _raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Debug.Log("BENE 1 touch detected");
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("BENE Tapped on: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.name == gameObject.name) StartDialog();
            }
           
        }
    }

    private void StartDialog() {
        canvas.SetActive(true);
    }

    public void Option1Selected() {
        Debug.Log("Option 1 selected");
        HideCanvas();
    }

    public void Option2Selected()
    {
        Debug.Log("Option 2 selected");
        HideCanvas();
    }

    public void Option3Selected()
    {
        Debug.Log("Option 3 selected");
        HideCanvas();
    }

    private void HideCanvas() {
        canvas.SetActive(false);
    }
}
