using UnityEngine;

public class MouseInput : MonoBehaviour
{
    internal delegate void ClickAction();
    internal static event ClickAction LeftClick;
    internal static event ClickAction RightClick;

    internal Vector2 MouseWorldPosition;

    private Vector2 _mousePosition;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        // Calculate mouse world pos
        _mousePosition = Input.mousePosition;
        MouseWorldPosition = _camera.ScreenToWorldPoint(_mousePosition);
        //print(MouseWorldPosition);

        CheckMouseClicks();
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
            if (RightClick == null) return;
            RightClick();
        }
    }
}
