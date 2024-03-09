using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action OnPressEsc;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPressEsc();
        }
    }
}
