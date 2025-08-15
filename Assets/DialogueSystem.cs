using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private string dialogueNPCName; // not being used yet
    //[SerializeField] private string[] dialogueMessage; // do not add in inspector

    [SerializeField] private List<string> dialogueMessage = new List<string>(); // dont touch this in inspector please :)

    [SerializeField] private int currentDialogueIndex;
    [SerializeField] private int currentDialogueIndexMax;

    [SerializeField] private TMP_Text dialogueText;
    public bool didPlayerWin;

    [SerializeField] private string DefeatedScene = "DefeatedScene";

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

                if (CastleData.Instance.castleHealthAmount <= 0)
                {
                    SceneManager.LoadScene(DefeatedScene);
                }else if (CastleData.Instance.castleHealthAmount <= 20)
                {
                    dialogueMessage.Add("Wow.. I have no words. Just how?");
                    dialogueMessage.Add("The castle is on its knees right now, there is no way you can do this Dragoon.");
                    dialogueMessage.Add("I have lost faith really. Good luck, you're gonna need it.");
                    dialogueMessage.Add("");
                }else if (CastleData.Instance.castleHealthAmount <= 50)
                {
                    dialogueMessage.Add("The castle is in shambles..");
                    dialogueMessage.Add("How did you even fail this badly Dragoon?");
                    dialogueMessage.Add("Okay, everything will be alright as long as we push through!");
                    dialogueMessage.Add("");
                }else if (CastleData.Instance.castleHealthAmount <= 70)
                {
                    dialogueMessage.Add("The castle did get beat up a bit.");
                    dialogueMessage.Add("But that will not stop us from taking them down, I believe you Dragoon.");
                    dialogueMessage.Add("Everything should be fine, we have a chance to fight back!");
                    dialogueMessage.Add("");
                }else if (CastleData.Instance.castleHealthAmount > 70)
                {
                    dialogueMessage.Add("You did a great job Dragoon!");
                    dialogueMessage.Add("If you keep up this pace, we will win against the fish in no time!");
                    dialogueMessage.Add("Good Luck to you!");
                    dialogueMessage.Add("");
                }

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

                if (CastleData.Instance.castleHealthAmount <= 0)
                {
                    SceneManager.LoadScene(DefeatedScene);
                }
                else if (CastleData.Instance.castleHealthAmount <= 20)
                {
                    dialogueMessage.Add("Wow there is nothing left how did you do this horrible Dragoon.");
                    dialogueMessage.Add("The castle is barely there, there is no way this happened, everyone is almost dead.");
                    dialogueMessage.Add("You really suck.");
                    dialogueMessage.Add("You really suck.");
                }
                else if (CastleData.Instance.castleHealthAmount <= 50)
                {
                    dialogueMessage.Add("The castle is in shambles..");
                    dialogueMessage.Add("How did you even fail this badly Dragoon?");
                    dialogueMessage.Add("Okay, everything will be alright as long as we push through!");
                    dialogueMessage.Add("Okay, everything will be alright as long as we push through!");
                }
                else if (CastleData.Instance.castleHealthAmount <= 70)
                {
                    dialogueMessage.Add("The castle did get beat up a bit but evryone is safe.");
                    dialogueMessage.Add("But thank you for your effort Dragoon everyone is and the kingdom is well.");
                    dialogueMessage.Add("In time we can rebuild the kingdom, you still did a good job.");
                    dialogueMessage.Add("In time we can rebuild the kingdom, you still did a good job.");
                }
                else if (CastleData.Instance.castleHealthAmount > 70)
                {
                    dialogueMessage.Add("You did a amazing job on protecting the DokiKingdom not a single scratch.");
                    dialogueMessage.Add("The Kingdom is safe thanks to you, now we all can rest peacefully now.");
                    dialogueMessage.Add("Thank you Dragoon!");
                    dialogueMessage.Add("Thank you Dragoon!");
                }

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

                if (CastleData.Instance.castleHealthAmount <= 0)
                {
                    SceneManager.LoadScene(DefeatedScene);
                }
                else if (CastleData.Instance.castleHealthAmount <= 20)
                {
                    dialogueMessage.Add("Wow.. I have no words. How did you do this badly so fast Dragoon?");
                    dialogueMessage.Add("The castle is on its knees right now, there is no way you can do this.");
                    dialogueMessage.Add("I have lost faith really. Good luck, you're gonna need it.");
                    dialogueMessage.Add("I have lost faith really. Good luck, you're gonna need it.");
                }
                else if (CastleData.Instance.castleHealthAmount <= 50)
                {
                    dialogueMessage.Add("The castle is in shambles Dragoon..");
                    dialogueMessage.Add("How did you even fail this badly?");
                    dialogueMessage.Add("Okay, everything will be alright as long as we push through!");
                    dialogueMessage.Add("Okay, everything will be alright as long as we push through!");
                }
                else if (CastleData.Instance.castleHealthAmount <= 70)
                {
                    dialogueMessage.Add("The castle did get beat up a bit.");
                    dialogueMessage.Add("But that will not stop us from taking them down, I believen you.");
                    dialogueMessage.Add("Everything should be fine, we have a chance to fight back!");
                    dialogueMessage.Add("Everything should be fine, we have a chance to fight back!");
                }
                else if (CastleData.Instance.castleHealthAmount > 70)
                {
                    dialogueMessage.Add("You are doing a great job Dragoon!");
                    dialogueMessage.Add("If you continue to keep up this pace, we will win against the fish in no time!");
                    dialogueMessage.Add("Good Luck to you!");
                    dialogueMessage.Add("Good Luck to you!");
                }

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

                if (CastleData.Instance.castleHealthAmount <= 0)
                {
                    SceneManager.LoadScene(DefeatedScene);
                }
                else if (CastleData.Instance.castleHealthAmount <= 20)
                {
                    dialogueMessage.Add("Wow.. I have no words. Goodluck I guess?");
                    dialogueMessage.Add("The castle is on its knees right now Dragoon, you were doing so well, there is no way you can do this.");
                    dialogueMessage.Add("I have lost faith really. Good luck, you're gonna need it.");
                    dialogueMessage.Add("I have lost faith really. Good luck, you're gonna need it.");
                }
                else if (CastleData.Instance.castleHealthAmount <= 50)
                {
                    dialogueMessage.Add("The castle is in shambles..");
                    dialogueMessage.Add("How did you even fail this badly?");
                    dialogueMessage.Add("Okay, everything will be alright as long as we push through!");
                    dialogueMessage.Add("Okay, everything will be alright as long as we push through!");
                }
                else if (CastleData.Instance.castleHealthAmount <= 70)
                {
                    dialogueMessage.Add("The castle did get beat up a bit.");
                    dialogueMessage.Add("But that will not stop us from taking them down, I believen you.");
                    dialogueMessage.Add("Everything should be fine, we have a chance to fight back!");
                    dialogueMessage.Add("Everything should be fine, we have a chance to fight back!");
                }
                else if (CastleData.Instance.castleHealthAmount > 70)
                {
                    dialogueMessage.Add("You did a great job!");
                    dialogueMessage.Add("If you keep up this pace, we will win against the fish in no time!");
                    dialogueMessage.Add("Good Luck to you!");
                    dialogueMessage.Add("Good Luck to you!");
                }

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

                if (CastleData.Instance.castleHealthAmount <= 0)
                {
                    SceneManager.LoadScene(DefeatedScene);
                }
                else if (CastleData.Instance.castleHealthAmount <= 20)
                {
                    dialogueMessage.Add("Wow.. I have no words. Goodluck I guess?");
                    dialogueMessage.Add("The castle is on its knees right now, there is no way you can do this.");
                    dialogueMessage.Add("I have lost faith really. Good luck, you're gonna need it.");
                    dialogueMessage.Add("I have lost faith really. Good luck, you're gonna need it.");
                }
                else if (CastleData.Instance.castleHealthAmount <= 50)
                {
                    dialogueMessage.Add("The castle is in shambles..");
                    dialogueMessage.Add("How did you even fail this badly?");
                    dialogueMessage.Add("Okay, everything will be alright as long as we push through!");
                    dialogueMessage.Add("Okay, everything will be alright as long as we push through!");
                }
                else if (CastleData.Instance.castleHealthAmount <= 70)
                {
                    dialogueMessage.Add("The castle did get beat up a bit.");
                    dialogueMessage.Add("But that will not stop us from taking them down, I believen you.");
                    dialogueMessage.Add("Everything should be fine, we have a chance to fight back!");
                    dialogueMessage.Add("Everything should be fine, we have a chance to fight back!");
                }
                else if (CastleData.Instance.castleHealthAmount > 70)
                {
                    dialogueMessage.Add("You did a great job!");
                    dialogueMessage.Add("If you keep up this pace, we will win against the fish in no time!");
                    dialogueMessage.Add("Good Luck to you!");
                    dialogueMessage.Add("Good Luck to you!");
                }

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

