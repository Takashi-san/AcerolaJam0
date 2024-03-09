using UnityEngine;

public class CrowdSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int quantity;
    public Bounds bound;

    private void Start()
    {
        for (int i = 0; i < quantity; i++)
        {
            float x = Random.Range(bound.min.x, bound.max.x);
            float y = Random.Range(bound.min.y, bound.max.y);
            Vector3 position = new(x, y, 0);
            Debug.Log($"prefab {i} position: {position}");
            Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(bound.center + transform.position, bound.size);
    }
}
