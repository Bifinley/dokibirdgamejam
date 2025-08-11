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

    private void SetDifficulty(MainMenu.GameDifficulty difficulty)
    {
        MainMenu.SelectedDifficulty = difficulty;

        SceneManager.LoadScene("Level 1");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1 - CutScene");
    }

    private void Start()
    {
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
    public void LeftDifficultyButton()
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
    }
    public void RightDifficultyButton()
    {
        if (currentDifficultyIndex > 3)
        {
            currentDifficultyIndex = 3;
        }
        if (currentDifficultyIndex < 3)
        {
            currentDifficultyIndex++;
        }
        SelectDifficulty();
    }
}
