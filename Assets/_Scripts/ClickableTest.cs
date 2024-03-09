using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableTest : MonoBehaviour, IPointerDownHandler
{

    public string identifier;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"clicked on me! name: {identifier}");
    }
}
