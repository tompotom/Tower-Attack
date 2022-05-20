using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent(typeof(BoxCollider2D));
    }
}
