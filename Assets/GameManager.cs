using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Transforms Info")] // I was messing around with headers, this is all unorganized. It will probably stay that way.
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform spawnEnemyPosition;

    [Header("Enemy Spawn List Info")]
    [SerializeField] private List<GameObject> activeEnemyList = new List<GameObject>();
    [SerializeField] private Dictionary<GameObject, float> enemyDistances = new Dictionary<GameObject, float>();

    [Header("Enemy Info")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float hitRange = 3f;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float enemyMaxDistanceOutSideOfBorder = -12f;

    [Header("Text Info")]
    [SerializeField] private TMP_Text hitsText;
    [SerializeField] private TMP_Text missText;

    [Header("Scores Info")]
    [SerializeField] private int enemyHits;
    [SerializeField] private int enemyMisses;

    [Header("Countdown Timer Info")]
    [SerializeField] private float defaultResetTime = 3f;
    [SerializeField] private float startCountDownTime = 0f;

    [SerializeField] private enum GameDifficulty { Easy, Medium, Normal, Hard };

    [SerializeField] private GameDifficulty gameDifficulty;

    private void Start()
    {
        switch (gameDifficulty)
        {
            case GameDifficulty.Easy:
                defaultResetTime = 5f;
                break;
            case GameDifficulty.Normal:
                defaultResetTime = 3f;
                break;
            case GameDifficulty.Medium:
                defaultResetTime = 1f;
                break;
            case GameDifficulty.Hard:
                defaultResetTime = 0.8f;
                break;
        }
    }

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
        AttackPlayerLogic();
    }

    private void AttackPlayerLogic()
    {
        enemyDistances.Clear();
        foreach (GameObject enemy in activeEnemyList) // checking every enemy in the List of ActiveEnemyList
        {
            distanceFromPlayer = Vector3.Distance(playerTransform.position, enemy.transform.position);
            enemyDistances[enemy] = distanceFromPlayer;

            if (Input.GetKeyDown(KeyCode.Space))
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
                enemyMisses += 1;

                missText.text = $"Misses: {enemyMisses}";
            }
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
            Gizmos.DrawWireSphere(playerTransform.position, hitRange); // draws a gizmo over the player transform and showing the hit range
        }
    }

}
