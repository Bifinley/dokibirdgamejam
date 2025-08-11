using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private string dialogueNPCName; // not being used yet
    [SerializeField] private string[] dialogueMessage;

    [SerializeField] private int currentDialogueIndex;

    [SerializeField] private TMP_Text dialogueText;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentDialogueIndex++;
            dialogueText.text = $"{dialogueMessage[currentDialogueIndex]}"; // goes through all the messages by the NPC in the array
        }
    }
}
