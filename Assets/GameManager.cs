using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Transforms Info")] // I was messing around with headers, this is all unorganized. It will probably stay that way.
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform spawnEnemyPosition;

    [Header("Enemy Spawn List Info")]
    [SerializeField] public List<GameObject> activeEnemyList = new List<GameObject>();
    [SerializeField] private Dictionary<GameObject, float> enemyDistances = new Dictionary<GameObject, float>();

    [Header("Enemy Info")]
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float hitRange = 3f;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float enemyMaxDistanceOutSideOfBorder = -12f;

    [Header("Text Info")]
    [SerializeField] private TMP_Text hitsText;
    [SerializeField] private TMP_Text missText;
    [SerializeField] private TMP_Text castleHealthAmountText;

    [Header("Scores Info")]
    [SerializeField] private int enemyHits;
    [SerializeField] private int enemyMisses;
    //[SerializeField] private int castleHealthAmount;
    private int setDamageAmount;

    [Header("Countdown Timer: Spawning Enemies Info")]
    [SerializeField] private float defaultResetTime = 3f;
    [SerializeField] private float startCountDownTime = 0f;

    [Header("Countdown GameTimer Info")]
    [SerializeField] private float defaultGameResetTime = 60f;
    [SerializeField] private float startGameCountDownTime = 0f;
    [SerializeField] private TMP_Text gameTimerText;
    private bool isGameOver = false;

    [SerializeField] private string DialogueScene = "Dialogue";
    [SerializeField] private string EndScene = "EndScene";

    [SerializeField] private bool isTest = true;

    MainMenu.GameDifficulty gameDifficulty;

    [SerializeField] private GameObject[] difficultySpriteGoons;

    public static bool isLevel1 = true; // change enemies via here - setting true just to keep it working
    public static bool isLevel2 = false;
    public static bool isLevel3 = false;
    public static bool isLevelEnd = false;
    private enum Dragoons
    {
        EggGoon,
        Dragoon,
        LongGoon,
        ChonkyGoon
    }

    private void Start()
    {
        if (isLevel1)
        {
            switch (gameDifficulty)
            {
                case MainMenu.GameDifficulty.Easy:
                    defaultResetTime = 5f;
                    if (!isTest) { startGameCountDownTime = 120f; }
                    if (isTest) { startGameCountDownTime = 3f; }
                    setDamageAmount = 1;
                    difficultySpriteGoons[(int)Dragoons.EggGoon].SetActive(true);
                    break;
                case MainMenu.GameDifficulty.Normal:
                    defaultResetTime = 3f;
                    if (!isTest) { startGameCountDownTime = 60f; }
                    if (isTest) { startGameCountDownTime = 3f; }
                    setDamageAmount = 2;
                    difficultySpriteGoons[(int)Dragoons.Dragoon].SetActive(true);
                    break;
                case MainMenu.GameDifficulty.Medium:
                    defaultResetTime = 1f;
                    if (!isTest) { startGameCountDownTime = 20f; }
                    if (isTest) { startGameCountDownTime = 3f; }
                    setDamageAmount = 3;
                    difficultySpriteGoons[(int)Dragoons.LongGoon].SetActive(true);
                    break;
                case MainMenu.GameDifficulty.Hard:
                    defaultResetTime = 0.8f;
                    if (!isTest) { startGameCountDownTime = 30f; }
                    if (isTest) { startGameCountDownTime = 3f; }
                    setDamageAmount = 4;
                    difficultySpriteGoons[(int)Dragoons.ChonkyGoon].SetActive(true);
                    break;
                default:
                    gameDifficulty = MainMenu.GameDifficulty.Normal;
                    break;
            }
        }
        if (isLevel2) // change enemies via here
        {
            switch (gameDifficulty)
            {
                case MainMenu.GameDifficulty.Easy:
                    defaultResetTime = 5f;
                    if (!isTest) { startGameCountDownTime = 120f; }
                    setDamageAmount = 1;
                    difficultySpriteGoons[(int)Dragoons.EggGoon].SetActive(true);
                    break;
                case MainMenu.GameDifficulty.Normal:
                    defaultResetTime = 3f;
                    if (!isTest) { startGameCountDownTime = 60f; }
                    setDamageAmount = 2;
                    difficultySpriteGoons[(int)Dragoons.Dragoon].SetActive(true);
                    break;
                case MainMenu.GameDifficulty.Medium:
                    defaultResetTime = 1f;
                    if (!isTest) { startGameCountDownTime = 20f; }
                    setDamageAmount = 3;
                    difficultySpriteGoons[(int)Dragoons.LongGoon].SetActive(true);
                    break;
                case MainMenu.GameDifficulty.Hard:
                    defaultResetTime = 0.8f;
                    if (!isTest) { startGameCountDownTime = 30f; }
                    setDamageAmount = 4;
                    difficultySpriteGoons[(int)Dragoons.ChonkyGoon].SetActive(true);
                    break;
                default:
                    gameDifficulty = MainMenu.GameDifficulty.Normal;
                    break;
            }
        }

        if (isLevel3) // change enemies via here
        {
            switch (gameDifficulty)
            {
                case MainMenu.GameDifficulty.Easy:
                    defaultResetTime = 5f;
                    startGameCountDownTime = 120f;
                    setDamageAmount = 1;
                    difficultySpriteGoons[(int)Dragoons.EggGoon].SetActive(true);
                    break;
                case MainMenu.GameDifficulty.Normal:
                    defaultResetTime = 3f;
                    startGameCountDownTime = 60f;
                    setDamageAmount = 2;
                    difficultySpriteGoons[(int)Dragoons.Dragoon].SetActive(true);
                    break;
                case MainMenu.GameDifficulty.Medium:
                    defaultResetTime = 1f;
                    startGameCountDownTime = 20f;
                    setDamageAmount = 3;
                    difficultySpriteGoons[(int)Dragoons.LongGoon].SetActive(true);
                    break;
                case MainMenu.GameDifficulty.Hard:
                    defaultResetTime = 0.8f;
                    startGameCountDownTime = 30f;
                    setDamageAmount = 4;
                    difficultySpriteGoons[(int)Dragoons.ChonkyGoon].SetActive(true);
                    break;
                default:
                    gameDifficulty = MainMenu.GameDifficulty.Normal;
                    break;
            }
        }

        castleHealthAmountText.text = $"DokiCastle Health: {CastleData.Instance.castleHealthAmount}";


    }

    private void Awake()
    {
        difficultySpriteGoons[(int)Dragoons.EggGoon].SetActive(false);
        difficultySpriteGoons[(int)Dragoons.Dragoon].SetActive(false);
        difficultySpriteGoons[(int)Dragoons.LongGoon].SetActive(false);
        difficultySpriteGoons[(int)Dragoons.ChonkyGoon].SetActive(false);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("There can only be ONE GameManager!");
            Destroy(gameObject);
        }

        gameDifficulty = MainMenu.SelectedDifficulty;

    }

    private void Update()
    {
        GameTimer();
        if (!isGameOver)
        {
            SpawnEnemyCountDown();
            NewAttackPlayerLogic();
        }
    }

    private void SaveLevelCompletion(string levelKey, bool completed)
    {
        PlayerPrefs.SetInt(levelKey, completed ? 1 : 0);
        PlayerPrefs.Save();
    }

    private bool LoadLevelCompletion(string levelKey)
    {
        return PlayerPrefs.GetInt(levelKey, 0) == 1;
    }

    private void GameTimer() // Ends game when timer is done
    {
        if (!isGameOver)
        {
            startGameCountDownTime -= Time.deltaTime;
            gameTimerText.text = $"Time Left: {startGameCountDownTime:N0} Seconds!";
            if (startGameCountDownTime <= 0f)
            {
                isGameOver = true;
                gameTimerText.text = "TIMES UP!";
            }
        }

        if (isGameOver && startGameCountDownTime <= 0)
        {
            StartCoroutine(GoToCutScene());
        }
    }
    public IEnumerator GoToCutScene()
    {
        yield return new WaitForSeconds(4f);

        if (!CastleData.Instance.isLevel1Completed)
        {
            CastleData.Instance.isLevel1Completed = true;
            SaveLevelCompletion("Level1Completed", true);
            isLevel1 = true;   // Keep true for dialogue
            isLevel2 = false;
            isLevel3 = false;
            isLevelEnd = false;
        }
        else if (CastleData.Instance.isLevel1Completed && !CastleData.Instance.isLevel2Completed)
        {
            CastleData.Instance.isLevel2Completed = true;
            SaveLevelCompletion("Level2Completed", true);
            isLevel1 = false;
            isLevel2 = true;   
            isLevel3 = false;
            isLevelEnd = false;
        }
        else if (CastleData.Instance.isLevel2Completed && !CastleData.Instance.isLevel3Completed)
        {
            CastleData.Instance.isLevel3Completed = true;
            SaveLevelCompletion("Level3Completed", true);
            isLevel1 = false;
            isLevel2 = false;
            isLevel3 = true;   
            isLevelEnd = false;
        }
        else if (CastleData.Instance.isLevel3Completed)
        {
            isLevel1 = false;
            isLevel2 = false;
            isLevel3 = false;
            isLevelEnd = true; // Final ending dialogue
        }

        if (CastleData.Instance.isLevel3Completed)
        {
            Debug.Log("Ending Scene");
            SceneManager.LoadScene(EndScene);
        }
        else
        {
            SceneManager.LoadScene(DialogueScene);
        }
    }


    private void NewAttackPlayerLogic() // this removes the error InvalidOperationException: Collection was modified; Using a forloop instead.
    {
        for (int i = activeEnemyList.Count - 1; i >= 0; i--)
        {
            GameObject enemy = activeEnemyList[i];
            if (enemy == null)
            {
                activeEnemyList.RemoveAt(i);
                continue; // skip to the next iteration if the enemy is null
            }
            float distanceFromPlayer = Vector3.Distance(playerTransform.position, enemy.transform.position); // checks distance between player and enemy
            enemyDistances[enemy] = distanceFromPlayer;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (distanceFromPlayer <= hitRange)
                {
                    Debug.Log(enemy.name + " Hit player!");
                    activeEnemyList.RemoveAt(i);
                    //Destroy(enemy);
                    if (enemy.GetComponent<ChainFishTEST>() != null)
                        enemy.GetComponent<ChainFishTEST>().alive = false; // this is a workaround to not destroy the enemy, but to stop it from moving and rendering.
                    else
                        enemy.GetComponent<Enemy>().alive = false; // this is a workaround to not destroy the enemy, but to stop it from moving and rendering.

                    enemyHits += 1;
                    hitsText.text = $"Hits: {enemyHits}";
                    continue;
                }
            }

            if (enemy.transform.position.x <= enemyMaxDistanceOutSideOfBorder)
            {
                Debug.Log(enemy.name + " Attacked the castle!");
                activeEnemyList.RemoveAt(i);
                Destroy(enemy);
                enemyMisses += 1;

                if (CastleData.Instance != null)
                {
                    CastleData.Instance.castleHealthAmount -= setDamageAmount;
                }

                UpdateCastleStatusUI();
                missText.text = $"Misses: {enemyMisses}";
            }
        }
    }

    private void UpdateCastleStatusUI()
    {
        castleHealthAmountText.text = $"DokiCastle Health: {CastleData.Instance.castleHealthAmount}";
    }
    private void SpawnEnemyCountDown()
    {
        startCountDownTime -= Time.deltaTime;
        if (startCountDownTime <= 0) // when it hits zero, spawn enemy. Enemy Spawns every startCountDownTime seconds.
        {
            startCountDownTime = defaultResetTime;

            // TODO: instantiate random enemy from a list of availible prefabs. spawning chain fish for now.

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
