using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundArea : MonoBehaviour
{
    public Bounds Bound;

    [Header("Gizmos")]
    [SerializeField] Color _gizmosColor = Color.green;

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawWireCube(Bound.center + transform.position, Bound.size);
    }
}
