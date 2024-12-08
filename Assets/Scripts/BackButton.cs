using UnityEngine;
using UnityEngine.InputSystem;


public class BackButton : MonoBehaviour
{

    public Camera arCamera;

    void Awake()
    {
        if (arCamera == null)
        {
            // Busca la cámara AR en la escena
            arCamera = Camera.main; // Usa la cámara principal (MainCamera)
            if (arCamera == null)
            {
                Debug.LogWarning("No se encontró la cámara principal en la escena.");
            }
        }
    }
    void Update()
    {
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;

            if (touch.press.isPressed)
            {
                Debug.Log("Toque detectado. Posición: " + touch.position.ReadValue());
            }

            if (touch.press.wasPressedThisFrame)
            {
                Debug.Log("Toque inicial detectado.");

                Vector2 touchPosition = touch.position.ReadValue();
                Debug.Log("Posición del toque: " + touchPosition);

                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Raycast impactó en: " + hit.collider.gameObject.name);

                    if (hit.transform == transform)
                    {
                        Debug.Log("Entro");
                        FindObjectOfType<MenuSelection>().OnBackButtonClicked();
                    }
                }
                else
                {
                    Debug.LogWarning("El raycast no impactó en ningún objeto.");
                }
            }
        }
        else
        {
            Debug.LogWarning("No se detecta pantalla táctil.");
        }
    }
}
