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
    private enum EnemyVariants
    {
        EasyFish,
        NormalFish,
        MediumFish,
        HardSpicyPepper
    }

    MainMenu.GameDifficulty gameDifficulty;
    [SerializeField] TMP_Text setDifficultyText;
    [SerializeField] TMP_Text actTitleText;

    [SerializeField] GameObject[] dokiDragoons;
    [SerializeField] GameObject[] enemyVariants;

    private void Awake()
    {
        HideAllDragoons();
        HideAllEnemies();

        gameDifficulty = MainMenu.SelectedDifficulty;
    }

    private void Start()
    {
        UpdateActTitle();
    }

    private void Update()
    {
        setDifficultyText.text = $"Difficulty: {gameDifficulty}";
        ShowDragoonForDifficulty();
    }

    private void UpdateActTitle()
    {
        if (CastleData.Instance.isLevel1Completed && CastleData.Instance.isLevel2Completed)
        {
            actTitleText.text = "Act 3: Lone Wolf";
        }
        else if (CastleData.Instance.isLevel1Completed)
        {
            actTitleText.text = "Act 2: All for nothing!";
        }
        else
        {
            actTitleText.text = "Act 1: The Beginning";
        }
    }

    private void HideAllDragoons()
    {
        for (int i = 0; i < dokiDragoons.Length; i++)
        {
            dokiDragoons[i].SetActive(false);
        }
    }
    private void HideAllEnemies()
    {
        for (int i = 0; i < enemyVariants.Length; i++)
        {
            enemyVariants[i].SetActive(false);
        }
    }

    private void ShowDragoonForDifficulty()
    {
        switch (gameDifficulty)
        {
            case MainMenu.GameDifficulty.Easy:
                dokiDragoons[(int)Dragoons.EggGoon].SetActive(true);
                enemyVariants[(int)EnemyVariants.EasyFish].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Normal:
                dokiDragoons[(int)Dragoons.Dragoon].SetActive(true);
                enemyVariants[(int)EnemyVariants.NormalFish].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Medium:
                dokiDragoons[(int)Dragoons.LongGoon].SetActive(true);
                enemyVariants[(int)EnemyVariants.MediumFish].SetActive(true);
                break;
            case MainMenu.GameDifficulty.Hard:
                dokiDragoons[(int)Dragoons.ChonkyGoon].SetActive(true);
                enemyVariants[(int)EnemyVariants.HardSpicyPepper].SetActive(true);
                break;
        }
    }
}
