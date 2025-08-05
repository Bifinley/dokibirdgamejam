using UnityEngine;
using System.Collections.Generic;

public class Rhythm : MonoBehaviour
{
    [SerializeField] private List<Transform> blocks;
    [SerializeField] private Transform pointToHit;
    [SerializeField] private float hitRange;
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        //Debug.Log(blocks[0].transform.position.x);    
        MoveBlocks();
        HitOrMiss();
    }

    private void MoveBlocks()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            if (blocks[i] != null)
            {
                blocks[i].Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
        }
    }

    public void HitOrMiss()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool hitRegistered = false;

            for (int i = 0; i < blocks.Count; i++)
            {
                float distance = Mathf.Abs(blocks[i].position.x - pointToHit.position.x);

                if (distance <= hitRange)
                {
                    Debug.Log("Key hit");

                    Destroy(blocks[i].gameObject);
                    blocks.RemoveAt(i);

                    hitRegistered = true;
                    break;
                }
            }

            if (!hitRegistered)
            {
                Debug.Log("Missed");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (pointToHit == null)
            return;

        Gizmos.color = Color.green;

        float lineY = pointToHit.position.y;
        float lineZ = pointToHit.position.z;

        // Draw a line across the hit range
        Vector3 leftEdge = new Vector3(pointToHit.position.x - hitRange, lineY, lineZ);
        Vector3 rightEdge = new Vector3(pointToHit.position.x + hitRange, lineY, lineZ);

        Gizmos.DrawLine(leftEdge, rightEdge);

        // Optional: draw vertical lines at edges
        Vector3 top = leftEdge + Vector3.up * 1f;
        Vector3 bottom = leftEdge + Vector3.down * 1f;
        Gizmos.DrawLine(top, bottom);

        top = rightEdge + Vector3.up * 1f;
        bottom = rightEdge + Vector3.down * 1f;
        Gizmos.DrawLine(top, bottom);
    }

}
