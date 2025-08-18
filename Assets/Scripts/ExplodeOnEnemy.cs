using System.Collections;
using UnityEngine;

public class ExplodeOnEnemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer explosionRenderer;

    private float explosionTimer = 0.5f;
    private float explosionRemover = 3f;

    [SerializeField] private AudioSource explosionSound;

    private void Start()
    {
        StartCoroutine(RemoveThySelf());
    }

    public IEnumerator RemoveThySelf()
    {
        yield return new WaitForSeconds(explosionTimer);

        explosionRenderer.enabled = false;

        yield return new WaitForSeconds(explosionRemover);
        Destroy(gameObject);

    }
}


