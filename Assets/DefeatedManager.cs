using TMPro;
using UnityEngine;

public class DefeatedManager : MonoBehaviour
{
    private enum Defeated_Dragoons
    {
        EggGoon,
        Dragoon,
        LongGoon,
        ChonkyGoon
    }

    MainMenu.GameDifficulty gameDifficulty;

    [SerializeField] GameObject[] dokiDragoons;

    private void Awake()
    {
        gameDifficulty = MainMenu.SelectedDifficulty;
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

    private void ShowDragoonForDifficulty()
    {
        switch (gameDifficulty)
        {
            case MainMenu.GameDifficulty.Easy:
                dokiDragoons[(int)Defeated_Dragoons.EggGoon].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Normal:
                dokiDragoons[(int)Defeated_Dragoons.Dragoon].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Medium:
                dokiDragoons[(int)Defeated_Dragoons.LongGoon].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Hard:
                dokiDragoons[(int)Defeated_Dragoons.ChonkyGoon].SetActive(true);
                break;
        }
    }
}
