using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    public void SetDifficultyEasy() => SetDifficulty(MainMenu.GameDifficulty.Easy);
    public void SetDifficultyMedium() => SetDifficulty(MainMenu.GameDifficulty.Medium);
    public void SetDifficultyNormal() => SetDifficulty(MainMenu.GameDifficulty.Normal);
    public void SetDifficultyHard() => SetDifficulty(MainMenu.GameDifficulty.Hard);

    public TMP_Text currentDifficultyText;

    public int currentDifficultyIndex;

    public AudioSource buttonDifficultySelectorSoundEffect;

    private const string GamePlayScene = "Gameplay";

    [SerializeField] private GameObject[] goonImages;

    private void SetDifficulty(MainMenu.GameDifficulty difficulty)
    {
        MainMenu.SelectedDifficulty = difficulty;

        SceneManager.LoadScene(GamePlayScene);
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("CutScene");
    }

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        MainMenu.SelectedDifficulty = MainMenu.GameDifficulty.Easy;
    }

    private void Update()
    {
        if (currentDifficultyIndex == 0) // easy goon
        {
            goonImages[0].SetActive(true);
        }
        else if (currentDifficultyIndex != 0)
        {
            goonImages[0].SetActive(false);
        }

        if (currentDifficultyIndex == 1) // normal goon
        {
            goonImages[1].SetActive(true);
        }
        else if (currentDifficultyIndex != 1)
        {
            goonImages[1].SetActive(false);
        }

        if (currentDifficultyIndex == 2) // long goon
        {
            goonImages[2].SetActive(true);
        }
        else if (currentDifficultyIndex != 2)
        {
            goonImages[2].SetActive(false);
        }

        if (currentDifficultyIndex == 3) // chonky goon
        {
            goonImages[3].SetActive(true);
        }
        else if (currentDifficultyIndex != 3)
        {
            goonImages[3].SetActive(false);
        }

        currentDifficultyText.text = $"Current Difficulty: {MainMenu.SelectedDifficulty}";
    }

    public void SelectDifficulty()
    {
        switch (currentDifficultyIndex)
        {
            case 0: MainMenu.SelectedDifficulty = MainMenu.GameDifficulty.Easy; break;

            case 1: MainMenu.SelectedDifficulty = MainMenu.GameDifficulty.Normal; break;

            case 2: MainMenu.SelectedDifficulty = MainMenu.GameDifficulty.Medium; break;

            case 3: MainMenu.SelectedDifficulty = MainMenu.GameDifficulty.Hard; break;
        }

        currentDifficultyText.text = $"Current Difficulty: {MainMenu.SelectedDifficulty}";
    }
    public void LeftDifficultyButton()  // I know Im using magic numbers here, please bear with me :(
    {
        if (currentDifficultyIndex < 0)
        {
            currentDifficultyIndex = 0;
        }
        if(currentDifficultyIndex > 0)
        {
            currentDifficultyIndex--;
        }
        SelectDifficulty();
        buttonDifficultySelectorSoundEffect.Play();                // 0 - Easy
    }                                                              // 1 - Normal
    public void RightDifficultyButton()                            // 2 - Medium
    {                                                              // 3 - Hard
        if (currentDifficultyIndex > 3)
        {
            currentDifficultyIndex = 3;
        }
        if (currentDifficultyIndex < 3)
        {
            currentDifficultyIndex++;
        }

        SelectDifficulty();
        buttonDifficultySelectorSoundEffect.Play();
    }
}
