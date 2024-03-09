using System.Collections.Generic;
using UnityEngine;

public class BoundSpawner : MonoBehaviour
{
    public Bounds bound;

    public List<T> Spawn<T>(T item, int count) where T : Object
    {
        List<T> list = new List<T>();
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(bound.min.x, bound.max.x);
            float y = Random.Range(bound.min.y, bound.max.y);
            Vector3 position = new(x, y, 0);
            T instantiated = Instantiate(item, position, Quaternion.identity, transform);
            list.Add(instantiated);
        }
        return list;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(bound.center + transform.position, bound.size);
    }
}
