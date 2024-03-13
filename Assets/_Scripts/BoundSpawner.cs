using System.Collections.Generic;
using UnityEngine;

public class BoundSpawner : MonoBehaviour
{
    [SerializeField] BoundArea _area;

    public List<T> Spawn<T>(T item, int count) where T : Object
    {
        List<T> list = new List<T>();
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(_area.Bound.min.x, _area.Bound.max.x);
            float y = Random.Range(_area.Bound.min.y, _area.Bound.max.y);
            Vector3 position = new(x, y, 0);
            T instantiated = Instantiate(item, position, Quaternion.identity, transform);
            list.Add(instantiated);
        }
        return list;
    }
}
