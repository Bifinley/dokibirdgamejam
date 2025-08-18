using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public enum GameDifficulty { Easy, Medium, Normal, Hard };

    public static GameDifficulty SelectedDifficulty = GameDifficulty.Normal;
}
