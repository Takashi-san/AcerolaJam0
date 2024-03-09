using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Input.mousePosition;
            Debug.Log($"Pressed left-click. position: {position}");
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 position = Input.mousePosition;
            Debug.Log($"Pressed right-click. position: {position}");
        }
    }
}
