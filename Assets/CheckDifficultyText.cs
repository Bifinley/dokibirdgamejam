using TMPro;
using UnityEngine;

public class CheckDifficultyText : MonoBehaviour
{
    private enum Dragoons
    {
        EggGoon,
        Dragoon,
        LongGoon,
        ChonkyGoon
    }

    MainMenu.GameDifficulty gameDifficulty;
    [SerializeField] TMP_Text setDifficultyText;

    [SerializeField] GameObject[] dokiDragoons;

    private void Start()
    {
        dokiDragoons[(int)Dragoons.EggGoon].SetActive(false);
        dokiDragoons[(int)Dragoons.Dragoon].SetActive(false);
        dokiDragoons[(int)Dragoons.LongGoon].SetActive(false);
        dokiDragoons[(int)Dragoons.ChonkyGoon].SetActive(false);
    }

    private void Awake()
    {
        gameDifficulty = MainMenu.SelectedDifficulty;
    }
    private void Update()
    {
        setDifficultyText.text = $"Difficulty: {gameDifficulty}";

        switch(gameDifficulty)
        {
            case MainMenu.GameDifficulty.Easy:
                dokiDragoons[(int)Dragoons.EggGoon].SetActive(true); break;
            case MainMenu.GameDifficulty.Normal:
                dokiDragoons[(int)Dragoons.Dragoon].SetActive(true); break;
            case MainMenu.GameDifficulty.Medium:
                dokiDragoons[(int)Dragoons.LongGoon].SetActive(true); break;
            case MainMenu.GameDifficulty.Hard:
                dokiDragoons[(int)Dragoons.ChonkyGoon].SetActive(true); break;
        }
    }
}
