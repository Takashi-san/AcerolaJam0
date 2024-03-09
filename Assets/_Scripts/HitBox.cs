using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class HitBox : MonoBehaviour, IPointerDownHandler
{
    public Action OnPointerDownAction;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownAction();
    }
}
