using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerChecker : MonoBehaviour
{
    private enum Dragoons
    {
        EggGoon,
        Dragoon,
        LongGoon,
        ChonkyGoon
    }

    [SerializeField] GameObject[] dokiDragoons;

    MainMenu.GameDifficulty gameDifficulty;

    private void Awake()
    {
        HideAllDragoons();
    }

    private void Start()
    {
        ShowDragoonForDifficulty();
    }

    private void HideAllDragoons()
    {
        for (int i = 0; i < dokiDragoons.Length; i++)
        {
            dokiDragoons[i].SetActive(false);
        }
    }

    public void playAgain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ShowDragoonForDifficulty()
    {
        switch (gameDifficulty)
        {
            case MainMenu.GameDifficulty.Easy:
                dokiDragoons[(int)Dragoons.EggGoon].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Normal:
                dokiDragoons[(int)Dragoons.Dragoon].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Medium:
                dokiDragoons[(int)Dragoons.LongGoon].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Hard:
                dokiDragoons[(int)Dragoons.ChonkyGoon].SetActive(true);
                break;
        }
    }
}
