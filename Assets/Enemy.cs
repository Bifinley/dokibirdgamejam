using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyMoveSpeed = 2f;

    [SerializeField] public Transform enemyTransform;

    [SerializeField] public float distanceFromPlayer;

    private float enemyMinSpeed = 2.5f;
    private float enemyMaxSpeed = 5f;


    private void Start()
    {
        enemyMoveSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
    }

    private void Update()
    {
        transform.position += Vector3.left * enemyMoveSpeed * Time.deltaTime;
    }
}
