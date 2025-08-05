using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(ThyDeleteSelf());
    }

    IEnumerator ThyDeleteSelf()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
