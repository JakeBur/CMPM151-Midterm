using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public float mouseSensitivity;
    public float deadZone;

    public event Action<Vector2> onMove;
    public event Action<Vector2> onRotate;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputAxis.magnitude < deadZone) inputAxis = Vector2.zero;

        onMove?.Invoke(inputAxis.normalized);
        onRotate?.Invoke(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity);
    }
}
