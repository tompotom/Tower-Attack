using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

    Towers _towers;

    List<GameObject> _troopsInRange = new List<GameObject>();

    public List<GameObject> TroopsInRange { get => _troopsInRange; set => _troopsInRange = value; }

    private void Start()
    {
        _towers = GameObject.FindGameObjectWithTag("Troop").GetComponent<Towers>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Troop"))
        {
            TroopsInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Troop"))
        {
            _troopsInRange.Remove(collision.gameObject);
        }
    }
}
