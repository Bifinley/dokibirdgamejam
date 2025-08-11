using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyMoveSpeed = 2f;

    [SerializeField] private float maxDistanceOutSideOfBorder = -12f;

    [SerializeField] public Transform enemyTransform;

    [SerializeField] public float distanceFromPlayer;


    private void Start()
    {
        enemyMoveSpeed = Random.Range(2.5f, 5f);
    }

    private void Update()
    {
        if(transform.position.x <= maxDistanceOutSideOfBorder)
        {
            //Destroy(gameObject);
        }
        else
        {
            transform.position += Vector3.left * enemyMoveSpeed * Time.deltaTime;
        }
    }
}
