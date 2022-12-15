using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using  UnityEngine.InputSystem.EnhancedTouch;

public class multiTouch : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brushManagerPrefab;

    protected void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    protected void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.began)
            {
            }
            else if (touch.ended) ;

            {
            }
        }
    }
}