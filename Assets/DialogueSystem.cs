using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private string dialogueNPCName; // not being used yet
    [SerializeField] private string[] dialogueMessage;

    [SerializeField] private int currentDialogueIndex;
    [SerializeField] private int currentDialogueIndexMax;

    [SerializeField] private TMP_Text dialogueText;

    private void Start()
    {
        currentDialogueIndex = -1;
        currentDialogueIndexMax = dialogueMessage.Length -1;

        Debug.Log(dialogueMessage.Length);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentDialogueIndex != currentDialogueIndexMax)
        {
            if (currentDialogueIndex > currentDialogueIndexMax)
            {
                currentDialogueIndex = currentDialogueIndexMax;
            }
            else if(currentDialogueIndex <= currentDialogueIndexMax) 
            {
                currentDialogueIndex++;
            }
            dialogueText.text = $"{dialogueMessage[currentDialogueIndex]}"; // goes through all the messages by the NPC in the array
        }
    }
}
