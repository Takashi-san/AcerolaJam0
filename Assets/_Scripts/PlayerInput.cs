using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action OnPressEsc;
    public Action<Vector3> MousePosition;

    [SerializeField] Camera _camera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPressEsc();
        }

        MousePosition(_camera.ScreenToWorldPoint(Input.mousePosition));
    }
}
