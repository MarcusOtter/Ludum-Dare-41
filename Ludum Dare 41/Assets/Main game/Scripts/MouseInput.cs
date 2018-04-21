using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MouseInput : MonoBehaviour
{
    internal delegate void ClickAction();
    internal static event ClickAction LeftClick;
    internal static event ClickAction RightClick;
    internal static event ClickAction RightMouseHeld;


    internal Vector2 MouseWorldPosition;

    [SerializeField] private float _rightMouseButtonHoldDelay;

    private Vector2 _mousePosition;
    private Camera _camera;

    private float _lastRightClickTime;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _lastRightClickTime = Mathf.Infinity;
    }

    private void Update()
    {
        // Calculate mouse world pos
        _mousePosition = Input.mousePosition;
        MouseWorldPosition = _camera.ScreenToWorldPoint(_mousePosition);
        //print(MouseWorldPosition);

        CheckMouseClicks();
        CheckRightMouseButtonHeld();
    }

    private void CheckRightMouseButtonHeld()
    {
        if (RightMouseHeld == null)
        {
            return;
        }

        if (Time.time > _lastRightClickTime + _rightMouseButtonHoldDelay)
        {
            RightMouseHeld();
        }
    }

    // Fire off events if click mouse button
    private void CheckMouseClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (LeftClick == null) return;
            LeftClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            _lastRightClickTime = Time.time;
            if (RightClick == null) return;
            RightClick();

        }

        if (Input.GetMouseButtonUp(1))
        {
            _lastRightClickTime = Mathf.Infinity;
        }

    }
}
