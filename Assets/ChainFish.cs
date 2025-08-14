using UnityEngine;

public class ChainFish : MonoBehaviour
{

    [SerializeField] private float enemyMoveSpeed = 2f;

    [SerializeField] public float distanceFromPlayer;

    [SerializeField] public GameObject FISH1;
    [SerializeField] public GameObject FISH2;
    [SerializeField] public float fish1positionX = 0f;
    [SerializeField] public float fish2positionX = 0f;





    private float enemyMinSpeed = 2.5f;
    private float enemyMaxSpeed = 5f;
    private float enemyStartingPosition = 12f;

    float[] specificYaxisSpawningRange = { 3.32f, 0.21f, -2.94f }; // this is very specific so enemies stay on a very specific 3 layer path

    MainMenu.GameDifficulty gameDifficulty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {




        gameDifficulty = MainMenu.SelectedDifficulty;

        switch (gameDifficulty)
        {
            case MainMenu.GameDifficulty.Easy:
                enemyMinSpeed = 1.2f;
                enemyMaxSpeed = 3f;
                break;
            case MainMenu.GameDifficulty.Normal:
                enemyMinSpeed = 1.5f;
                enemyMaxSpeed = 3.5f;
                break;
            case MainMenu.GameDifficulty.Medium:
                enemyMinSpeed = 3.7f;
                enemyMaxSpeed = 6f;
                break;
            case MainMenu.GameDifficulty.Hard:
                enemyMinSpeed = 7.5f;
                enemyMaxSpeed = 14f;
                break;
        }
        SetEnemyPositionAndSpeed();


        // instantiate two fish enemies to be "connected" to. 
        enemyMoveSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
        float enemyY = specificYaxisSpawningRange[Random.Range(0, specificYaxisSpawningRange.Length)];
        Vector3 fish1pos = new Vector3(enemyStartingPosition, enemyY, 0);

        enemyMoveSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
        enemyY = specificYaxisSpawningRange[Random.Range(0, specificYaxisSpawningRange.Length)];
        Vector3 fish2pos = new Vector3(enemyStartingPosition, enemyY, 0);

        GameObject Fish1 = Instantiate(FISH1, fish1pos, Quaternion.identity);
        GameObject Fish2 = Instantiate(FISH2, fish2pos, Quaternion.identity);

        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.activeEnemyList.Add(Fish1);
        gameManager.activeEnemyList.Add(Fish2);



    }


    private void Update()
    {
        EnemyMovement();
        fish1positionX = FISH1.transform.position.x;
        fish2positionX = FISH2.transform.position.x;

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
