using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dfvsfdsq : MonoBehaviour
{
    Transform transform;

    
    public void GetTransform()
    {
        transform = GetComponent<Transform>();
    }

    private void Start()
    {
        transform.position = new Vector3(0f, 1f, 0.2f);
    }


}
