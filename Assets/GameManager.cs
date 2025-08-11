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

    [SerializeField] private float enemyMaxDistanceOutSideOfBorder = -12f;

    [SerializeField] private TMP_Text hitsText;
    [SerializeField] private TMP_Text missText;
    [SerializeField] private int enemyHits;
    [SerializeField] private int enemyMisses;

    private float defaultResetTime = 3f;
    [SerializeField] private float startCountDownTime = 0f;

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
        SpawnEnemyCountDown();

        enemyDistances.Clear();
        foreach (GameObject enemy in activeEnemyList) // checking every enemy in the List of ActiveEnemyList
        {
            distanceFromPlayer = Vector3.Distance(playerTransform.position, enemy.transform.position);
            enemyDistances[enemy] = distanceFromPlayer;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (distanceFromPlayer <= hitRange)
                {
                    Debug.Log(enemy.name + "Hit player!");
                    activeEnemyList.Remove(enemy);
                    Destroy(enemy);
                    enemyHits += 1;

                    hitsText.text = $"Hits: {enemyHits}";
                }
            }

            if (enemy.transform.position.x <= enemyMaxDistanceOutSideOfBorder) // destroy enemy once it leaves border
            {
                Debug.Log(enemy.name + "Went outside the border.");
                activeEnemyList.Remove(enemy);
                Destroy(enemy);
                enemyHits += 1;

                missText.text = $"Misses: {enemyHits}";
            }

            //Debug.Log(distanceFromPlayer);
        }


    }

    private void SpawnEnemyCountDown()
    {
        startCountDownTime -= Time.deltaTime;
        if (startCountDownTime <= 0) // when it hits zero, spawn enemy. Enemy Spawns every startCountDownTime seconds.
        {
            startCountDownTime = defaultResetTime;

            GameObject newEnemy = Instantiate(enemyPrefab, spawnEnemyPosition.position, Quaternion.identity);
            activeEnemyList.Add(newEnemy);
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
