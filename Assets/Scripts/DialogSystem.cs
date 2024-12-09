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
    public Animator animator;

    private bool freezeInteraction = false;


    private void Start()
    {
        _raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        if (!freezeInteraction && Input.touchCount == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.name == gameObject.name) StartDialog();
            }
           
        }
    }

    private void StartDialog() {
        canvas.SetActive(true);
    }

    public void Option1Selected() {
        Debug.Log("Option 1 (sushi) selected");
        StartCoroutine(CookCoroutine());
        // Show sushi model
    }

    public void Option2Selected()
    {
        Debug.Log("Option 2 (ramen) selected");
        StartCoroutine(CookCoroutine());
        // Show ramen model
    }

    public void Option3Selected()
    {
        Debug.Log("Option 3 (pizza) selected");
        StartCoroutine(CookCoroutine());
        // Show pizza model
    }

    IEnumerator CookCoroutine()
    {
        canvas.SetActive(false);
        freezeInteraction = true;
        animator.SetBool("IsCooking", true);
        yield return new WaitForSeconds(10);
        animator.SetBool("IsCooking", false);
        freezeInteraction = false;
    }
}
