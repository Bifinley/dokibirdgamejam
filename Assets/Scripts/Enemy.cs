using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum EnemyVariants
    {
        EasyFish,
        NormalFish,
        MediumFish,
        HardSpicyPepper
    }

    [SerializeField] private float enemyMoveSpeed = 2f;

    [SerializeField] public float distanceFromPlayer;
    [SerializeField] public bool alive = true;
    [SerializeField] public bool Uninstall = false;

    private float enemyMinSpeed = 2.5f;
    private float enemyMaxSpeed = 5f;
    private float enemyStartingPosition = 12f;

    float[] specificYaxisSpawningRange = { 3.32f, 0.21f, -2.94f };

    MainMenu.GameDifficulty gameDifficulty;

    [SerializeField] GameObject[] fishVarients; 

    private void Awake()
    {
        if (fishVarients == null || fishVarients.Length < 4)
        {
            Debug.LogError("Fish variants not properly assigned in Inspector! (Need 4)");
            return;
        }

        DisableAllFish();
    }

    private void Start()
    {
        gameDifficulty = MainMenu.SelectedDifficulty;
        EnableDifficultyFish(gameDifficulty);
        SetEnemyPositionAndSpeed();
    }

    private void Update()
    {
        if (Uninstall)
        {
            Destroy(gameObject);
            return;
        }

        if (!alive)
        {
            var renderer = GetComponentInChildren<SpriteRenderer>();
            if (renderer != null)
                renderer.enabled = false;

            DisableAllFish();
        }
        else
        {
            EnemyMovement();
        }
    }

    private void EnemyMovement()
    {
        transform.position += Vector3.left * enemyMoveSpeed * Time.deltaTime;
    }

    private void SetEnemyPositionAndSpeed()
    {
        enemyMoveSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
        float enemyY = specificYaxisSpawningRange[Random.Range(0, specificYaxisSpawningRange.Length)];
        transform.position = new Vector3(enemyStartingPosition, enemyY, 0);
    }

    private void DisableAllFish()
    {
        foreach (var fish in fishVarients)
        {
            if (fish != null)
                fish.SetActive(false);
        }
    }

    private void EnableDifficultyFish(MainMenu.GameDifficulty difficulty)
    {
        DisableAllFish();

        int index = 0;
        switch (difficulty)
        {
            case MainMenu.GameDifficulty.Easy:
                enemyMinSpeed = 1.2f;
                enemyMaxSpeed = 3f;
                index = (int)EnemyVariants.EasyFish;
                break;
            case MainMenu.GameDifficulty.Normal:
                enemyMinSpeed = 1.5f;
                enemyMaxSpeed = 3.5f;
                index = (int)EnemyVariants.NormalFish;
                break;
            case MainMenu.GameDifficulty.Medium:
                enemyMinSpeed = 3.7f;
                enemyMaxSpeed = 6f;
                index = (int)EnemyVariants.MediumFish;
                break;
            case MainMenu.GameDifficulty.Hard:
                enemyMinSpeed = 7.5f;
                enemyMaxSpeed = 14f;
                index = (int)EnemyVariants.HardSpicyPepper;
                break;
        }

        if (fishVarients[index] != null)
            fishVarients[index].SetActive(true);
    }
}
