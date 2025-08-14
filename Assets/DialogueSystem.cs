using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private string dialogueNPCName; // not being used yet
    //[SerializeField] private string[] dialogueMessage; // do not add in inspector

    [SerializeField] private List<string> dialogueMessage = new List<string>(); 

    [SerializeField] private int currentDialogueIndex;
    [SerializeField] private int currentDialogueIndexMax;

    [SerializeField] private TMP_Text dialogueText;
    public bool didPlayerWin;

    private void Start()
    {
        currentDialogueIndex = -1;
        currentDialogueIndexMax = dialogueMessage.Count -1;

        //Debug.Log(dialogueMessage.Count);
    }

    private void Update()
    {
        // Level 1 first-time completion
        if (CastleData.Instance.isLevel1Completed && !GameManager.isLevelEnd && !CastleData.Instance.isLevel2Completed && !CastleData.Instance.isLevel3Completed)
        {
            if (currentDialogueIndex == -1) 
            {
                dialogueMessage.Clear();
                dialogueMessage.Add("First time seeing this message!");
                dialogueMessage.Add("Congrats on beating the first level!");
                dialogueMessage.Add("Congrats on beating the first level!");
                currentDialogueIndexMax = dialogueMessage.Count - 1;
            }

            if (Input.GetMouseButtonDown(0) && currentDialogueIndex != currentDialogueIndexMax)
            {
                if (currentDialogueIndex > currentDialogueIndexMax)
                    currentDialogueIndex = currentDialogueIndexMax;
                else
                    currentDialogueIndex++;

                dialogueText.text = dialogueMessage[currentDialogueIndex];

                if (currentDialogueIndex >= currentDialogueIndexMax)
                    SceneManager.LoadScene("CutScene");
            }
        }

        // Final ending (all levels complete)
        else if (GameManager.isLevelEnd && CastleData.Instance.isLevel1Completed && CastleData.Instance.isLevel2Completed && CastleData.Instance.isLevel3Completed)
        {
            if (currentDialogueIndex == -1)
            {
                dialogueMessage.Clear();
                dialogueMessage.Add("YOU HAVE BEATEN ALL THE LEVELS!");
                dialogueMessage.Add("LESSSS GOOOOOO");
                dialogueMessage.Add("LESSSS GOOOOOO");
                currentDialogueIndexMax = dialogueMessage.Count - 1;
            }

            if (Input.GetMouseButtonDown(0) && currentDialogueIndex != currentDialogueIndexMax)
            {
                if (currentDialogueIndex > currentDialogueIndexMax)
                    currentDialogueIndex = currentDialogueIndexMax;
                else
                    currentDialogueIndex++;

                dialogueText.text = dialogueMessage[currentDialogueIndex];

                if (currentDialogueIndex >= currentDialogueIndexMax)
                    SceneManager.LoadScene("CutScene");
            }
        }

        // Level 1 cutscene
        else if (GameManager.isLevel1 && CastleData.Instance.isLevel1Completed)
        {
            if (currentDialogueIndex == -1)
            {
                dialogueMessage.Clear();
                dialogueMessage.Add("YOU BEAT LEVEL 1");
                dialogueMessage.Add("RAWWWWWW");
                dialogueMessage.Add("RAWWWWWW");
                currentDialogueIndexMax = dialogueMessage.Count - 1;
            }

            if (Input.GetMouseButtonDown(0) && currentDialogueIndex != currentDialogueIndexMax)
            {
                if (currentDialogueIndex > currentDialogueIndexMax)
                    currentDialogueIndex = currentDialogueIndexMax;
                else
                    currentDialogueIndex++;

                dialogueText.text = dialogueMessage[currentDialogueIndex];

                if (currentDialogueIndex >= currentDialogueIndexMax)
                    SceneManager.LoadScene("CutScene");
            }
        }

        // Level 2 cutscene
        else if (GameManager.isLevel2 && CastleData.Instance.isLevel2Completed)
        {
            if (currentDialogueIndex == -1)
            {
                dialogueMessage.Clear();
                dialogueMessage.Add("YOU BEAT LEVEL 2");
                dialogueMessage.Add("RAWWWWWW 2 22222");
                dialogueMessage.Add("RAWWWWWW 2 22222");
                currentDialogueIndexMax = dialogueMessage.Count - 1;
            }

            if (Input.GetMouseButtonDown(0) && currentDialogueIndex != currentDialogueIndexMax)
            {
                if (currentDialogueIndex > currentDialogueIndexMax)
                    currentDialogueIndex = currentDialogueIndexMax;
                else
                    currentDialogueIndex++;

                dialogueText.text = dialogueMessage[currentDialogueIndex];

                if (currentDialogueIndex >= currentDialogueIndexMax)
                    SceneManager.LoadScene("CutScene");
            }
        }

        // Level 3 cutscene
        else if (GameManager.isLevel3 && CastleData.Instance.isLevel3Completed)
        {
            if (currentDialogueIndex == -1)
            {
                dialogueMessage.Clear();
                dialogueMessage.Add("YOU BEAT LEVEL 3");
                dialogueMessage.Add("3333333333333333333333RAWWWWWW");
                dialogueMessage.Add("3333333333333333333333RAWWWWWW");
                currentDialogueIndexMax = dialogueMessage.Count - 1;
            }

            if (Input.GetMouseButtonDown(0) && currentDialogueIndex != currentDialogueIndexMax)
            {
                if (currentDialogueIndex > currentDialogueIndexMax)
                    currentDialogueIndex = currentDialogueIndexMax;
                else
                    currentDialogueIndex++;

                dialogueText.text = dialogueMessage[currentDialogueIndex];

                if (currentDialogueIndex >= currentDialogueIndexMax)
                    SceneManager.LoadScene("CutScene");
            }
        }
    }


}

