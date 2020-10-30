using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
	bool isDragging, isMobilePlatform;
    Vector2 tapPoint, TouchDelta;
    public float minTouchDelta = 0;

    public delegate void OnTouchInput(TouchType type);
    public static event OnTouchInput TouchEvent;

    public enum TouchType
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

     private void Awake()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
            isMobilePlatform = false;
        #else
            isMobilePlatform = true;
        #endif
    }

    private void Update()
    {
        if (!isMobilePlatform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                tapPoint = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
                ResetTouch();
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    isDragging = true;
                    tapPoint = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Canceled ||
                         Input.touches[0].phase == TouchPhase.Ended)
                    ResetTouch();
            }
        }
        CalculateTouch();
    }

    void CalculateTouch()
    {
        TouchDelta = Vector2.zero;

        if (isDragging)
        {
            if (!isMobilePlatform && Input.GetMouseButton(0))
                TouchDelta = (Vector2)Input.mousePosition - tapPoint;
            else if (Input.touchCount > 0)
                TouchDelta = Input.touches[0].position - tapPoint;
        }

        if (TouchDelta.magnitude > minTouchDelta)
        {
            if (TouchEvent != null)
            {
                if (Mathf.Abs(TouchDelta.x) > Mathf.Abs(TouchDelta.y))
                    TouchEvent(TouchDelta.x < 0 ? TouchType.LEFT : TouchType.RIGHT);
                else
                    TouchEvent(TouchDelta.y > 0 ? TouchType.UP : TouchType.DOWN);
            }
            //ResetTouch();
        }
    }

    void ResetTouch()
    {
        isDragging = false;
        tapPoint = TouchDelta = Vector2.zero;
    }

}
