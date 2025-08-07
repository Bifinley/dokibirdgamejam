using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyMoveSpeed = 2f;

    //[SerializeField] private Transform enemyTransform; might use this for something later


    private void Update()
    {
        if(transform.position.x <= -20)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position += Vector3.left * enemyMoveSpeed * Time.deltaTime;
        }
    }
}
