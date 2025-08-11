using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyMoveSpeed = 2f;

    [SerializeField] public float distanceFromPlayer;

    private float enemyMinSpeed = 2.5f;
    private float enemyMaxSpeed = 5f;
    private float enemyStartingPosition = 12f;

    float[] specificYaxisSpawningRange = { 3.32f, 0.21f, -2.94f }; // this is very specific so enemies stay on a very specific 3 layer path

    MainMenu.GameDifficulty gameDifficulty;

    private void Start()
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
                enemyMinSpeed = 3.2f;
                enemyMaxSpeed = 5f;
                break;
            case MainMenu.GameDifficulty.Hard:
                enemyMinSpeed = 5f;
                enemyMaxSpeed = 6f;
                break;
        }
        SetEnemyPositionAndSpeed();
    }

    private void Update()
    {
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
