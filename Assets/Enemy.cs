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

    float[] specificYaxisSpawningRange = { 3.32f, 0.21f, -2.94f }; // this is very specific so enemies stay on a very specific 3 layer path

    MainMenu.GameDifficulty gameDifficulty;

    [SerializeField] GameObject[] fishVarients;
//[SerializeField] private SpriteRenderer originalFishRenderer;

    private void Awake()
    {
        //originalFishRenderer = GetComponent<SpriteRenderer>();
        fishVarients[(int)EnemyVariants.EasyFish].SetActive(false);
        fishVarients[(int)EnemyVariants.NormalFish].SetActive(false);
        fishVarients[(int)EnemyVariants.MediumFish].SetActive(false);
        fishVarients[(int)EnemyVariants.HardSpicyPepper].SetActive(false);
    }

    private void Start()
    {
        switch (gameDifficulty)
        {
            case MainMenu.GameDifficulty.Easy:
                enemyMinSpeed = 1.2f;
                enemyMaxSpeed = 3f;
                fishVarients[(int)EnemyVariants.EasyFish].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Normal:
                enemyMinSpeed = 1.5f;
                enemyMaxSpeed = 3.5f;
                fishVarients[(int)EnemyVariants.NormalFish].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Medium:
                enemyMinSpeed = 3.7f;
                enemyMaxSpeed = 6f;
                fishVarients[(int)EnemyVariants.MediumFish].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Hard:
                enemyMinSpeed = 7.5f;
                enemyMaxSpeed = 14f;
                fishVarients[(int)EnemyVariants.HardSpicyPepper].SetActive(true);
                break;
        }
        SetEnemyPositionAndSpeed();
    }

    private void Update()
    {
        if (Uninstall)
        {
            Destroy(gameObject);
            return; // uninstalling life.exe
        }

        if (!alive)
        {
            var renderer = GetComponentInChildren<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;

                fishVarients[(int)EnemyVariants.EasyFish].SetActive(false);
                fishVarients[(int)EnemyVariants.NormalFish].SetActive(false);
                fishVarients[(int)EnemyVariants.MediumFish].SetActive(false);
                fishVarients[(int)EnemyVariants.HardSpicyPepper].SetActive(false);
            }
        }
        else 
            EnemyMovement();
    }

    private void EnemyMovement()
    {
        transform.position += Vector3.left * enemyMoveSpeed * Time.deltaTime; // moving towards the player which is left
    }

    private void SetEnemyPositionAndSpeed()
    {
        enemyMoveSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);

        float enemyY = specificYaxisSpawningRange[Random.Range(0, specificYaxisSpawningRange.Length)];

        transform.position = new Vector3(enemyStartingPosition, enemyY, 0);
    }
}
