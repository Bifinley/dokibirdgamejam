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

    GameEnums.GameDifficulty gameDifficulty;

    private void Awake()
    {
        HideAllDragoons();

        gameDifficulty = GameEnums.SelectedDifficulty;
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
            case GameEnums.GameDifficulty.Easy:
                dokiDragoons[(int)Dragoons.EggGoon].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Normal:
                dokiDragoons[(int)Dragoons.Dragoon].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Hard:
                dokiDragoons[(int)Dragoons.LongGoon].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Expert:
                dokiDragoons[(int)Dragoons.ChonkyGoon].SetActive(true);
                break;
        }
    }
}
