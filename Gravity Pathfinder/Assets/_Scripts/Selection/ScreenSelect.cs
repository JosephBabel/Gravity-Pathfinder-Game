using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Finger = UnityEngine.InputSystem.EnhancedTouch.Finger;

public class ScreenSelect : MonoBehaviour
{
    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += FingerDown;
    }

    void OnDisable()
    {
        Touch.onFingerDown -= FingerDown;
        EnhancedTouchSupport.Disable();
    }

    void FingerDown(Finger finger)
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(finger.screenPosition), out RaycastHit raycastHit, 100f, Globals.SelectableObjectLayer);

        if (raycastHit.collider != null)
        {
            raycastHit.transform.TryGetComponent(out SelectableObject selectableObject);

            if (selectableObject != null)
            {
                EventHandler.SelectObject(selectableObject);
            }
        }
    }
}
