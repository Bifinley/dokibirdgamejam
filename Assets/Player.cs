using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Vector2 playerInput = new Vector2(0, 0);
    [SerializeField] private Vector2 moveOnY = new Vector2(0, 0);
    [SerializeField] private float playerMoveSpeed = 5f;
    [SerializeField] private int lastPlayerLocation;

    [SerializeField] private bool isAllowedToMove = true;

    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (playerTransform.position.y >= 3) // This stops the player from going above the Y axis 3
        {
            playerTransform.position = new Vector3(-7.5f, 3, 0);
            isAllowedToMove = false;
            if (Input.GetKeyDown(KeyCode.S))
            {
                isAllowedToMove = true;
            }
            // Debug.Log("You are less than 3 or at 3.");
        }
        if (playerTransform.position.y <= -3) // This stops the player from going above the Y axis -3
        {
            playerTransform.position = new Vector3(-7.5f, -3, 0);
            isAllowedToMove = false;
            if (Input.GetKeyDown(KeyCode.W))
            {
                isAllowedToMove = true;
            }
            // Debug.Log("You are less than -3 or at -3.");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerInput.y = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerInput.y = -1;
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            playerInput.y = 0;
        }

        if (isAllowedToMove)
        {
            moveOnY = new Vector2(0, playerInput.y) * playerMoveSpeed * Time.deltaTime; // sets the movement up on the Y vector and player speed

            playerTransform.position += new Vector3(0, moveOnY.y, 0); // moves the player up and down on Y
        }
    }
}

