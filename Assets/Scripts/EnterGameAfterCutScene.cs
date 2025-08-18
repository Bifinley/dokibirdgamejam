using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGameAfterCutScene : MonoBehaviour
{
    [SerializeField] private string SceneName;

    [SerializeField] private float inputDelayTimer = 4f;

    void Update()
    {
        StartCoroutine(RunAfterDelay(inputDelayTimer));
    }

    IEnumerator RunAfterDelay(float delayTimer)
    {
        yield return new WaitForSeconds(delayTimer);

        EnterNextScene();
    }

    private void EnterNextScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
