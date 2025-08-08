using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform dragoonTransform; // this is the player gameobject btw

    [SerializeField] private Transform spawnEnemyPosition;

    [SerializeField] private List<GameObject> activeEnemyList = new List<GameObject>();

    [SerializeField] private float hitRange = 3f;

    private bool hasSpawned = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("There can only be ONE GameManager!");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(activeEnemyList[0], spawnEnemyPosition.position, Quaternion.identity);
        }

        foreach (GameObject enemy in activeEnemyList)
        {
            if(enemy != null)
            {
                float distanceFromPlayer = Vector3.Distance(dragoonTransform.position, enemy.transform.position);

                if (distanceFromPlayer <= hitRange)
                {
                    Debug.Log("Hit player!");
                }

                Debug.Log(distanceFromPlayer);
            }
        }

    }
}
