using TMPro;
using UnityEngine;

public class CheckDifficultyText : MonoBehaviour
{
    MainMenu.GameDifficulty gameDifficulty;
    [SerializeField] TMP_Text setDifficultyText;


    private void Awake()
    {
        gameDifficulty = MainMenu.SelectedDifficulty;
    }
    private void Update()
    {
        setDifficultyText.text = $"Difficulty: {gameDifficulty}";
    }
}
