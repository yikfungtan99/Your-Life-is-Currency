﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public float genAreaRadiusX = 3f;
    public float genAreaRadiusY = 3f;

    public int spawnNumbers;
    public GameObject[] objectsToSpawn;

    private Vector3 spawnPosition;

    private bool canSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        StartSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSpawn()
    {
        int safetyNet = 0;
        int randomselect = 0;

        for (int i = 0; i < spawnNumbers; i++)
        {
            while (!canSpawn)
            {
                Vector3 origin = transform.position;
                Vector3 range = transform.localScale / 2.0f;
                Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x),
                                                  Random.Range(-range.y, range.y));

                int chance = Random.Range(0, 100);

                if (chance <= 30)
                {
                    randomselect = 0;
                }

                if (chance > 30)
                {
                    randomselect = 1;
                }

                spawnPosition = origin + randomRange;

                if (Physics2D.OverlapCircle(spawnPosition, 3f) == null)
                {
                    canSpawn = true;
                }

                if (canSpawn)
                {
                    GameObject newObject = Instantiate(objectsToSpawn[randomselect], spawnPosition, Quaternion.identity);
                    canSpawn = false;
                    break;
                }

                safetyNet++;

                if (safetyNet > 50)
                {
                    Debug.Log("Too many attempts");
                    break;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
