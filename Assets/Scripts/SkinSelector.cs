using TMPro;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public TMP_Text currentDragoonSkin;

    [SerializeField] private int currentDragoonIndex;

    public AudioSource buttonSelectorSoundEffect;

    [SerializeField] private GameObject[] dragoonImagePreview;

    private void Awake()
    {
        
    }

    private void Update()
    {
        //UpdateCurrentDifficultyUI();
        if (currentDragoonIndex == 0) // easy goon
        {
            dragoonImagePreview[0].SetActive(true);
        }
        else if (currentDragoonIndex != 0)
        {
            dragoonImagePreview[0].SetActive(false);
        }

        if (currentDragoonIndex == 1) // normal goon
        {
            dragoonImagePreview[1].SetActive(true);
        }
        else if (currentDragoonIndex != 1)
        {
            dragoonImagePreview[1].SetActive(false);
        }

        if (currentDragoonIndex == 2) // long goon
        {
            dragoonImagePreview[2].SetActive(true);
        }
        else if (currentDragoonIndex != 2)
        {
            dragoonImagePreview[2].SetActive(false);
        }

        if (currentDragoonIndex == 3) // chonky goon
        {
            dragoonImagePreview[3].SetActive(true);
        }
        else if (currentDragoonIndex != 3)
        {
            dragoonImagePreview[3].SetActive(false);
        }
    }

    public void SelectDragoonSkin()
    {
        switch (currentDragoonIndex)
        {
            case 0: GameEnums.SelectedDragoon = GameEnums.DragoonType.Egg_Dragoon; break;

            case 1: GameEnums.SelectedDragoon = GameEnums.DragoonType.Regular_Dragoon; break;

            case 2: GameEnums.SelectedDragoon = GameEnums.DragoonType.Long_Dragoon; break;

            case 3: GameEnums.SelectedDragoon = GameEnums.DragoonType.Chonky_Dragoon; break;
        }
        UpdateCurrentDifficultyUI();
    }

    public void LeftDragoonSkinButton()
    {
        currentDragoonIndex = Mathf.Max(0, currentDragoonIndex - 1);
        SelectDragoonSkin();
        buttonSelectorSoundEffect.Play();
    }
    public void RightDragoonSkinButton()
    {
        int maxIndex = System.Enum.GetValues(typeof(GameEnums.DragoonType)).Length - 1;
        currentDragoonIndex = Mathf.Min(maxIndex, currentDragoonIndex + 1);
        SelectDragoonSkin();
        buttonSelectorSoundEffect.Play();
    }
    private void UpdateCurrentDifficultyUI()
    {
        currentDragoonSkin.text = $"Current Selected Skin: {GameEnums.SelectedDragoon}";
    }
}
