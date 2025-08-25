using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string SceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName); 
    }
}
