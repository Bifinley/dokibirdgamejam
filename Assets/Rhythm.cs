using UnityEngine;
using System.Collections.Generic;

public class Rhythm : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform pointToHit;

    [Header("Settings")]
    [SerializeField] private float bpm = 120f;
    [SerializeField] private float hitRange = 0.5f;

    private List<Transform> blocks = new List<Transform>();
    private float beatInterval;
    private float nextBeatTime;
    private float moveSpeed;

    private void Start()
    {
        beatInterval = 60f / bpm;

        float distance = spawnPoint.position.x - pointToHit.position.x;
        moveSpeed = distance / beatInterval;

        nextBeatTime = Time.time + beatInterval;
    }

    private void Update()
    {
        SpawnOnBeat();
        MoveBlocks();
        HitOrMiss();
    }

    private void SpawnOnBeat()
    {
        if (Time.time >= nextBeatTime)
        {
            GameObject newBlock = Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity);
            blocks.Add(newBlock.transform);
            nextBeatTime += beatInterval;
        }
    }

    private void MoveBlocks()
    {
        foreach (Transform block in blocks)
        {
            if (block != null)
            {
                block.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void HitOrMiss()
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
}
