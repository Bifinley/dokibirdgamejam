using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform spawnEnemyPosition;

    [SerializeField] private List<GameObject> activeEnemyList = new List<GameObject>();
    [SerializeField] private Dictionary<GameObject, float> enemyDistances = new Dictionary<GameObject, float>();

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float hitRange = 3f;

    [SerializeField] private float distanceFromPlayer;

    private bool hasSpawned = false;

    private void Awake()
    {
        if (Instance == null)
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
            GameObject newEnemy = Instantiate(enemyPrefab, spawnEnemyPosition.position, Quaternion.identity);
            activeEnemyList.Add(newEnemy);
        }

        enemyDistances.Clear();
        foreach (GameObject enemy in activeEnemyList) // checking every enemy in the List of ActiveEnemyList
        {
            distanceFromPlayer = Vector3.Distance(playerTransform.position, enemy.transform.position);
            enemyDistances[enemy] = distanceFromPlayer;

            if (distanceFromPlayer <= hitRange)
            {
                Debug.Log(enemy.name + "Hit player!");
                activeEnemyList.Remove(enemy);
                Destroy(enemy);
            }

            //Debug.Log(distanceFromPlayer);
        }


    }

    private void OnDrawGizmos()
    {
        if (playerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position, hitRange);
        }
    }

}
