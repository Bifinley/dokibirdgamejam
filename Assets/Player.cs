using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;

    [SerializeField] private Vector2 playerInput = new Vector2(0, 0);
    [SerializeField] private Vector2 moveOnY = new Vector2(0, 0);

    private void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerInput.y = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerInput.y = -1;
        }else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            playerInput.y = 0;
        }

        moveOnY = new Vector2(0, playerInput.y) * 5 * Time.deltaTime;

        PlayerTransform.position += new Vector3(0,moveOnY.y,0);
    }
}

