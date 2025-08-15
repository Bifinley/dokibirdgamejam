using System.Collections;
using UnityEngine;

public class ExplodeOnEnemy : MonoBehaviour
{
    [SerializeField] private float explosionTimer = 0.7f;
    [SerializeField] private AudioSource explosionSound;
    bool soundPlayed = false;

    private void Start()
    {
        if (soundPlayed)
        {
            explosionSound.Stop();
        }
        else if (!soundPlayed)
        {
            explosionSound.Play();
            soundPlayed = true;
        }

        StartCoroutine(RemoveThySelf());
    }

    public IEnumerator RemoveThySelf()
    {
        yield return new WaitForSeconds(explosionTimer);

        Destroy(gameObject);

    }
}


