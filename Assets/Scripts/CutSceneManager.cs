using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    // Text UI
    [SerializeField] TMP_Text actTitleText;
    [SerializeField] TMP_Text currentDifficultyText;

    // Game Enums
    GameEnums.GameDifficulty GameDifficultyEnum;
    [SerializeField] GameEnums.DragoonType DragoonTypeEnum;
    GameEnums.EnemyType EnemyTypeEnum;

    // GameObject Arrays
    [SerializeField] private GameObject[] DragoonCutSceneSkins;
    [SerializeField] private GameObject[] EnemyCutSceneSkins;

    // SceneManager
    private const string GamePlayScene = "Gameplay";
    private float inputDelayTimer = 4f;

    private void Awake()
    {
        SetUpGameEnums();
        HideAllDragoonsAndEnemies();
    }

    private void Start()
    {
        UpdateCutSceneInfoUI();
        UpdateDragoonAndEnemyCutSceneSkin();
        StartCoroutine(EnterGamePlayScene(inputDelayTimer));
    }

    private void Update()
    {
        currentDifficultyText.text = $"Difficulty: {GameDifficultyEnum}";
    }
    private void UpdateCutSceneInfoUI()
    {
        if (actTitleText != null)
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
        if (currentDifficultyText != null)
        {
            currentDifficultyText.text = $"Difficulty: {GameDifficultyEnum}";
        }
    }

    private void UpdateDragoonAndEnemyCutSceneSkin() // future self: We dont want difficulty to set skins anymore (for dragoons)
    {
        switch (GameDifficultyEnum)
        {
            case GameEnums.GameDifficulty.Easy:
                EnemyCutSceneSkins[(int)GameEnums.EnemyType.Easy_Fish].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Normal:
                EnemyCutSceneSkins[(int)GameEnums.EnemyType.Normal_Fish].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Hard:
                EnemyCutSceneSkins[(int)GameEnums.EnemyType.Hard_Fish].SetActive(true);
                break;
            case GameEnums.GameDifficulty.Expert:
                EnemyCutSceneSkins[(int)GameEnums.EnemyType.Expert_Pepper].SetActive(true);
                break;
        }
        switch (DragoonTypeEnum)
        {
            case GameEnums.DragoonType.Egg_Dragoon:
                DragoonCutSceneSkins[(int)GameEnums.DragoonType.Egg_Dragoon].SetActive(true);
                break;
            case GameEnums.DragoonType.Regular_Dragoon:
                DragoonCutSceneSkins[(int)GameEnums.DragoonType.Regular_Dragoon].SetActive(true);
                break;
            case GameEnums.DragoonType.Long_Dragoon:
                DragoonCutSceneSkins[(int)GameEnums.DragoonType.Long_Dragoon].SetActive(true);
                break;
            case GameEnums.DragoonType.Chonky_Dragoon:
                DragoonCutSceneSkins[(int)GameEnums.DragoonType.Chonky_Dragoon].SetActive(true);
                break;
        }
    }
    private void HideAllDragoonsAndEnemies()
    {
        for (int i = 0; i < DragoonCutSceneSkins.Length; i++)
        {
            DragoonCutSceneSkins[i].SetActive(false);
        }

        for (int i = 0; i < EnemyCutSceneSkins.Length; i++)
        {
            EnemyCutSceneSkins[i].SetActive(false);
        }
    }

    IEnumerator EnterGamePlayScene(float delayTimer)
    {
        yield return new WaitForSeconds(delayTimer);

        SceneManager.LoadScene(GamePlayScene);
    }

    private void SetUpGameEnums()
    {
        GameDifficultyEnum = GameEnums.SelectedDifficulty;
        DragoonTypeEnum = GameEnums.SelectedDragoon;
        EnemyTypeEnum = GameEnums.SelectedEnemy;
    }
}
