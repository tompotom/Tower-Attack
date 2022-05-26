using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorRandomly : MonoBehaviour
{
    [SerializeField] private string spawnPointTag;
    [SerializeField] private bool alwaysSpawn = true;

    [Space]
    [SerializeField] private List<GameObject> prefabsToSpawn;
    

    GameObject[] spawnPoints;


    private void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag(spawnPointTag);
    }

    private void Start()
    {
        RandomSpawn();
    }
    void RandomSpawn()
    {
        foreach  (GameObject spawnPoint in spawnPoints)
        {
            int randomPrefab = Random.Range(0, prefabsToSpawn.Count);

            if (alwaysSpawn)
            {
                GameObject points = Instantiate(prefabsToSpawn[randomPrefab]);
                points.transform.position = spawnPoint.transform.position;
        }
            else
            {
                int spawnOrNot = Random.Range(0, 2);
                if (spawnOrNot == 0)
                {
                    GameObject points = Instantiate(prefabsToSpawn[randomPrefab]);
                    points.transform.position = spawnPoint.transform.position;
                }
            }
        }

        

    }
}
