using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Detection _detection;
    [SerializeField] float _speed;

    void Start()
    {
        _detection = GameObject.FindObjectOfType<Detection>();
    }

    void Update()
    {
        MoveTowardTroop();
    }

    private void MoveTowardTroop()
    {
        if (_detection.TroopsInRange.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, _detection.TroopsInRange[0].transform.position, _speed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Troop"))
        {
            Destroy(gameObject);
        }
    }
}
