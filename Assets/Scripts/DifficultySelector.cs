using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
    public TMP_Text currentDifficultyText;

    private int currentDifficultyIndex;

    public AudioSource buttonSelectorSoundEffect;

    private const string GamePlayScene = "Gameplay";

    [SerializeField] private GameObject[] goonImages;

    /*private void SetDifficulty(GameEnums.GameDifficulty difficulty)
    {
        GameEnums.SelectedDifficulty = difficulty;

        SceneManager.LoadScene(GamePlayScene);
    }*/

    public void StartGame()
    {
        PlayerPrefs.DeleteAll(); // here just in case
        SceneManager.LoadScene("CutScene");
    }

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        //UpdateCurrentDifficultyUI();
        /*if (currentDifficultyIndex == 0) // easy goon
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
        }*/
    }

    public void SelectDifficulty()
    {
        switch (currentDifficultyIndex)
        {
            case 0: GameEnums.SelectedDifficulty = GameEnums.GameDifficulty.Easy; break;

            case 1: GameEnums.SelectedDifficulty = GameEnums.GameDifficulty.Normal; break;

            case 2: GameEnums.SelectedDifficulty = GameEnums.GameDifficulty.Hard; break;

            case 3: GameEnums.SelectedDifficulty = GameEnums.GameDifficulty.Expert; break;
        }
        UpdateCurrentDifficultyUI();
    }
    public void LeftDifficultyButton()  
    {
        currentDifficultyIndex = Mathf.Max(0, currentDifficultyIndex - 1);
        SelectDifficulty();
        buttonSelectorSoundEffect.Play();
    }                                                         
    public void RightDifficultyButton()                      
    {
        int maxIndex = System.Enum.GetValues(typeof(GameEnums.GameDifficulty)).Length - 1;
        currentDifficultyIndex = Mathf.Min(maxIndex, currentDifficultyIndex + 1);
        SelectDifficulty();
        buttonSelectorSoundEffect.Play();
    }
    private void UpdateCurrentDifficultyUI()
    {
        currentDifficultyText.text = $"Current Difficulty: {GameEnums.SelectedDifficulty}";
    }
}


// 0 - Easy
// 1 - Normal
// 2 - Hard
// 3 - Expert