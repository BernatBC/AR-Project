using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectManipulation : MonoBehaviour
{
    private bool isObjectPlaced = false;
    private Vector2 touchStartPos;
    private Vector2 touchPrevPos;
    private float initialScale;
    private bool isTouching = false;
    private float initialDistance;

    private float minScale = 0.1f;
    private float maxScale = 0.25f;

    private float initialAngle = 0f;

    private void Update()
    {
        if (isObjectPlaced)
        {
            var touches = Touchscreen.current.touches;
            int activeTouchCount = 0;
            foreach (var touch in touches)
            {
                if (touch.press.isPressed)
                {
                    activeTouchCount++;
                }
            }
            Debug.Log("Toques detectados: " + activeTouchCount);
            if (activeTouchCount == 1)
            {
                var touch = Touchscreen.current.primaryTouch;
                Debug.Log("Toque detectado.");

                if (touch.press.isPressed)
                {

                    Debug.Log("Toque presionado.");

                    if (!isTouching)
                    {
                        touchStartPos = touch.position.ReadValue();
                        isTouching = true;
                    }
                    Vector2 touchDelta = touch.position.ReadValue() - touchPrevPos;

                    if (touchDelta.magnitude > 0.01f)
                    {
                        MoveObject(touchDelta);
                    }

                    touchPrevPos = touch.position.ReadValue();
                }
                else if (!touch.press.isPressed && isTouching)
                {
                    isTouching = false;
                }
            }
            
            else if (activeTouchCount == 3)
            {
                Debug.Log("Tres toques detectados.");
                var touch1 = Touchscreen.current.touches[0];
                var touch2 = Touchscreen.current.touches[1];

                if (touch1.press.isPressed && touch2.press.isPressed)
                {
                    if (initialDistance == 0)
                    {
                        initialDistance = Vector2.Distance(touch1.position.ReadValue(), touch2.position.ReadValue());
                    }
                    ScaleObject(touch1.position.ReadValue(), touch2.position.ReadValue());
                }
            }

            else if (activeTouchCount == 2)
            {
                var touch1 = Touchscreen.current.touches[0];
                var touch2 = Touchscreen.current.touches[1];

                if (touch1.press.isPressed && touch2.press.isPressed)
                {
                    RotateObject(touch1.position.ReadValue(), touch2.position.ReadValue());
                }
            }
        }
    }

    private void MoveObject(Vector2 touchDelta)
    {
        Vector3 move = new Vector3(touchDelta.x * 0.0001f, touchDelta.y * 0.0001f, 0f);
        transform.position += move;
    }

    private void ScaleObject(Vector2 position1, Vector2 position2)
    {
        Debug.Log("Escalar objeto.");
        float distanceNow = Vector2.Distance(position1, position2);
        float scaleFactor = distanceNow / initialDistance;

        scaleFactor = Mathf.Max(0.1f, Mathf.Min(scaleFactor, 1.05f));


        Vector3 newScale = new Vector3(
            transform.localScale.x * scaleFactor,
            transform.localScale.y * scaleFactor,
            transform.localScale.z * scaleFactor
        );

        newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
        newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
        newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);

        transform.localScale = newScale;


        Debug.Log($"Escala aplicada: {transform.localScale}");

    }

    private void RotateObject(Vector2 touch1, Vector2 touch2)
    {
        Vector2 touchDelta1 = touch1 - touchPrevPos;
        Vector2 touchDelta2 = touch2 - touchPrevPos;

        float angleNow = Vector2.SignedAngle(touchDelta1, touchDelta2);

        float rotationFactor = 0.01f;
        angleNow *= rotationFactor;

        transform.Rotate(Vector3.up, angleNow);

        Debug.Log($"Ángulo de rotación: {angleNow}");

        touchPrevPos = (touch1 + touch2) / 2;
    }

    public void PlaceObjectOnTracker()
    {
        isObjectPlaced = true;
        touchPrevPos = Touchscreen.current.primaryTouch.position.ReadValue();
        initialDistance = 0;
    }
}
