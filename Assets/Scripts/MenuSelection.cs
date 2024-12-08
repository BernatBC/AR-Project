using UnityEngine;
using UnityEngine.InputSystem;

public class MenuSelection : MonoBehaviour
{
    public Camera arCamera; // La cámara AR que usará el raycast
    public string platoSeleccionado; // Nombre del plato seleccionado
    public GameObject menuCanvas;      // Canvas del menú principal
    public GameObject detailCanvas;    // Canvas de la vista detallad

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

        if (detailCanvas != null)
        {
            detailCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si el dispositivo tiene pantalla táctil
        if (Touchscreen.current != null)
        {
            // Obtener el toque principal
            var touch = Touchscreen.current.primaryTouch;

            // Debug: Ver si hay toques detectados
            if (touch.press.isPressed)
            {
                Debug.Log("Toque detectado. Posición: " + touch.position.ReadValue());
            }

            // Detectar si el toque acaba de empezar
            if (touch.press.wasPressedThisFrame)
            {
                Debug.Log("Toque inicial detectado.");

                // Leer la posición del toque
                Vector2 touchPosition = touch.position.ReadValue();
                Debug.Log("Posición del toque: " + touchPosition);

                // Realizar un raycast desde la posición del toque
                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                RaycastHit hit;

                // Comprobar si el raycast golpea un objeto con collider
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Raycast impactó en: " + hit.collider.gameObject.name);

                    // Verificar si el objeto impactado es este
                    if (hit.transform == transform)
                    {
                        Debug.Log("Plato seleccionado: " + platoSeleccionado);
                        // Aquí puedes añadir lógica adicional para mostrar detalles del plato
                        ShowDetailView(platoSeleccionado);
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
    // Cambiar a la vista detallada
    public void ShowDetailView(string platoInfo)
    {        
        // Ocultar el canvas del menú y mostrar el canvas detallado
        menuCanvas.SetActive(false);
        detailCanvas.SetActive(true);
    }

    // Volver al menú principal
    public void ShowMenuView()
    {
        // Ocultar el canvas detallado y mostrar el canvas del menú
        detailCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        ShowMenuView();
    }


}
