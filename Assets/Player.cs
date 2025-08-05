using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;

    [SerializeField] private Vector2 playerInput = new Vector2(0, 0);

    private void Start()
    {
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            playerInput.y = 4;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerInput.y = -4;
        }

        PlayerTransform.position = playerInput;*/
    }
}

