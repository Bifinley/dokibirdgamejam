using UnityEngine;

public class DragoonAndEnemyManager : MonoBehaviour
{
    // Game Enums
    GameEnums.GameDifficulty GameDifficultyEnum;
    GameEnums.DragoonType DragoonTypeEnum;
    GameEnums.EnemyType EnemyTypeEnum;

    // GameObject Arrays
    [SerializeField] private GameObject[] DragoonSkins;
    [SerializeField] private GameObject[] EnemySkins;

    private void Awake()
    {
        SetUpGameEnums();
        HideAllDragoonsAndEnemies();
    }

    private void Start()
    {
        ShowChosenDragoon();
        //ShowChosenEnemy();
    }

    private void ShowChosenDragoon()
    {
        switch (DragoonTypeEnum)
        {
            case GameEnums.DragoonType.Egg_Dragoon:
                DragoonSkins[(int)GameEnums.DragoonType.Egg_Dragoon].SetActive(true);
                break;
            case GameEnums.DragoonType.Regular_Dragoon:
                DragoonSkins[(int)GameEnums.DragoonType.Regular_Dragoon].SetActive(true);
                break;
            case GameEnums.DragoonType.Long_Dragoon:
                DragoonSkins[(int)GameEnums.DragoonType.Long_Dragoon].SetActive(true);
                break;
            case GameEnums.DragoonType.Chonky_Dragoon:
                DragoonSkins[(int)GameEnums.DragoonType.Chonky_Dragoon].SetActive(true);
                break;
        }
    }
    private void ShowChosenEnemy()
    {
        switch (GameDifficultyEnum)
        {
            case GameEnums.GameDifficulty.Easy:
                EnemySkins[(int)GameEnums.EnemyType.Easy_Fish].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Normal:
                EnemySkins[(int)GameEnums.EnemyType.Normal_Fish].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Hard:
                EnemySkins[(int)GameEnums.EnemyType.Hard_Fish].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Expert:
                EnemySkins[(int)GameEnums.EnemyType.Expert_Pepper].SetActive(true);
                break;
        }
    }
    private void HideAllDragoonsAndEnemies()
    {
        for (int i = 0; i < DragoonSkins.Length; i++)
        {
            DragoonSkins[i].SetActive(false);
        }

        for (int i = 0; i < EnemySkins.Length; i++)
        {
            EnemySkins[i].SetActive(false);
        }
    }
    private void SetUpGameEnums()
    {
        GameDifficultyEnum = GameEnums.SelectedDifficulty;
        DragoonTypeEnum = GameEnums.SelectedDragoon;
        EnemyTypeEnum = GameEnums.SelectedEnemy;
    }
}
