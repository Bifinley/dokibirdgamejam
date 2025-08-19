using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatedManager : MonoBehaviour
{
    private enum Defeated_Dragoons
    {
        EggGoon,
        Dragoon,
        LongGoon,
        ChonkyGoon
    }

    GameEnums.GameDifficulty gameDifficulty;

    [SerializeField] GameObject[] dokiDragoons;

    private void Awake()
    {
        gameDifficulty = GameEnums.SelectedDifficulty;
    }

    private void Start()
    {
        HideAllDragoons();
    }

    private void Update()
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

    public void TryAgain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ShowDragoonForDifficulty()
    {
        switch (gameDifficulty)
        {
            case GameEnums.GameDifficulty.Easy:
                dokiDragoons[(int)Defeated_Dragoons.EggGoon].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Normal:
                dokiDragoons[(int)Defeated_Dragoons.Dragoon].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Hard:
                dokiDragoons[(int)Defeated_Dragoons.LongGoon].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Expert:
                dokiDragoons[(int)Defeated_Dragoons.ChonkyGoon].SetActive(true);
                break;
        }
    }
}
