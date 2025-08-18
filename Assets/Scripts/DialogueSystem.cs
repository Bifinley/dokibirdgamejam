using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    private List<string> dialogueMessage = new List<string>();

    [SerializeField] private int currentDialogueIndex;
    [SerializeField] private int currentDialogueIndexMax;

    [SerializeField] private string dialogueNPCName;
    [SerializeField] private TMP_Text npcName;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private int castleHealthAmount;

    public bool didPlayerWin;

    private const string DefeatedScene = "DefeatedScene";
    private const string CutScene = "CutScene";

    private void Start()
    {
        currentDialogueIndex = -1;
        currentDialogueIndexMax = dialogueMessage.Count -1;

        npcName.text = dialogueNPCName;

        castleHealthAmount = CastleData.Instance.castleHealthAmount;
    }

    private void Update()
    {
        // Level 1 first-time completion
        if (CastleData.Instance.isLevel1Completed && !GameManager.isLevelEnd && !CastleData.Instance.isLevel2Completed && !CastleData.Instance.isLevel3Completed)
        {
            if (currentDialogueIndex == -1) 
            {
                dialogueMessage.Clear();

                switch (castleHealthAmount)
                {
                    case <= 0:
                        SceneManager.LoadScene(DefeatedScene);
                        break;
                    case <= 20:
                        dialogueMessage.Add("Wow.. I have no words. Just how?");
                        dialogueMessage.Add("The castle is on its knees right now, there is no way you can do this Dragoon.");
                        dialogueMessage.Add("I have lost faith really. Good luck, you're gonna need it.");
                        dialogueMessage.Add("");
                        break;
                    case <= 50:
                        dialogueMessage.Add("The castle is in shambles..");
                        dialogueMessage.Add("How did you even fail this badly Dragoon?");
                        dialogueMessage.Add("Okay, everything will be alright as long as we push through!");
                        dialogueMessage.Add("");
                        break;
                    case <= 70:
                        dialogueMessage.Add("The castle did get beat up a bit.");
                        dialogueMessage.Add("But that will not stop us from taking them down, I believe you Dragoon.");
                        dialogueMessage.Add("Everything should be fine, we have a chance to fight back!");
                        dialogueMessage.Add("");
                        break;
                    case > 70:
                        dialogueMessage.Add("You did a great job Dragoon!");
                        dialogueMessage.Add("If you keep up this pace, we will win against the fish in no time!");
                        dialogueMessage.Add("Good Luck to you!");
                        dialogueMessage.Add("");
                        break;
                }

                currentDialogueIndexMax = dialogueMessage.Count - 1;
            }

            ClickThroughDialogue();
        }

        // Final ending (all levels complete)
        else if (GameManager.isLevelEnd && CastleData.Instance.isLevel1Completed && CastleData.Instance.isLevel2Completed && CastleData.Instance.isLevel3Completed)
        {
            if (currentDialogueIndex == -1)
            {
                dialogueMessage.Clear();

                switch (castleHealthAmount)
                {
                    case <= 0:
                        SceneManager.LoadScene(DefeatedScene);
                        break;
                    case <= 20:
                        dialogueMessage.Add("Wow there is nothing left how did you do this horrible Dragoon.");
                        dialogueMessage.Add("The castle is barely there, there is no way this happened.");
                        dialogueMessage.Add("You really suck.");
                        break;
                    case <= 50:
                        dialogueMessage.Add("The castle is in shambles..");
                        dialogueMessage.Add("How did you even fail this badly Dragoon?");
                        dialogueMessage.Add("Well it could be worse thank you for your effort Dragoon.");
                        break;
                    case <= 70:
                        dialogueMessage.Add("The castle did get beat up a bit but evryone is safe.");
                        dialogueMessage.Add("But thank you for your effort Dragoon everyone is and the kingdom is well.");
                        dialogueMessage.Add("In time we can rebuild the kingdom, you still did a good job.");
                        break;
                    case > 70:
                        dialogueMessage.Add("You did a amazing job on protecting the DokiKingdom not a single scratch.");
                        dialogueMessage.Add("The Kingdom is safe thanks to you, now we all can rest peacefully now.");
                        dialogueMessage.Add("Thank you Dragoon!");
                        break;
                }

                currentDialogueIndexMax = dialogueMessage.Count - 1;
            }

            ClickThroughDialogue();
        }

        // Level 1 cutscene
        else if (GameManager.isLevel1 && CastleData.Instance.isLevel1Completed)
        {
            if (currentDialogueIndex == -1)
            {
                dialogueMessage.Clear();

                switch (castleHealthAmount)
                {
                    case <= 0:
                        SceneManager.LoadScene(DefeatedScene);
                        break;
                    case <= 20:
                        dialogueMessage.Add("Wow.. I have no words. How did you do this badly so fast Dragoon?");
                        dialogueMessage.Add("The castle is on its knees right now, there is no way you can do this.");
                        dialogueMessage.Add("I have lost faith really. Good luck, you're gonna need it.");
                        break;
                    case <= 50:
                        dialogueMessage.Add("The castle is in shambles Dragoon..");
                        dialogueMessage.Add("How did you even fail this badly?");
                        dialogueMessage.Add("Okay, everything will be alright as long as we push through!");
                        break;
                    case <= 70:
                        dialogueMessage.Add("The castle did get beat up a bit.");
                        dialogueMessage.Add("But that will not stop us from taking them down, I believe you Dragoon.");
                        dialogueMessage.Add("Everything should be fine, we have a chance to fight back!");
                        break;
                    case > 70:
                        dialogueMessage.Add("You are doing a great job Dragoon!");
                        dialogueMessage.Add("You are doing really well, the other dragoons have never been this good.");
                        dialogueMessage.Add("Good Luck to you!");
                        break;
                }

                currentDialogueIndexMax = dialogueMessage.Count - 1;
            }

            ClickThroughDialogue();
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
                    dialogueMessage.Add("How did you even fail this badly and that quickly too");
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
                    dialogueMessage.Add("You are doing a amzing job Dragoon!");
                    dialogueMessage.Add("If you keep up this pace, we will defeat the fish in no time!");
                    dialogueMessage.Add("Good Luck to you!");
                    dialogueMessage.Add("Good Luck to you!");
                }

                currentDialogueIndexMax = dialogueMessage.Count - 1;
            }

            ClickThroughDialogue();
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
                    dialogueMessage.Add("Wow..you fell so badly this is the final strech.");
                    dialogueMessage.Add("The castle is on its knees right now, there is no way you can do this Dragoon.");
                    dialogueMessage.Add("Good luck, you're gonna need it.");
                    dialogueMessage.Add("Good luck, you're gonna need it.");
                }
                else if (CastleData.Instance.castleHealthAmount <= 50)
                {
                    dialogueMessage.Add("The castle is in shambles..");
                    dialogueMessage.Add("How did you even fail this badly?");
                    dialogueMessage.Add("This is the final stech but everything will be alright as long as we push through!");
                    dialogueMessage.Add("This is the final stech but everything will be alright as long as we push through!");
                }
                else if (CastleData.Instance.castleHealthAmount <= 70)
                {
                    dialogueMessage.Add("The castle did get beat up a bit.");
                    dialogueMessage.Add("But that will not stop us from taking them down, we are close to the end.");
                    dialogueMessage.Add("Everything should be fine, we have a chance to fight back!");
                    dialogueMessage.Add("Everything should be fine, we have a chance to fight back!");
                }
                else if (CastleData.Instance.castleHealthAmount > 70)
                {
                    dialogueMessage.Add("Keep up the good work!");
                    dialogueMessage.Add("If you keep up this pace, we will win against the fish in no time!");
                    dialogueMessage.Add("Good Luck to you!");
                    dialogueMessage.Add("Good Luck to you!");
                }

                currentDialogueIndexMax = dialogueMessage.Count - 1;
            }

            ClickThroughDialogue();
        }
    }

    private void ClickThroughDialogue()
    {
        if (Input.GetMouseButtonDown(0) && currentDialogueIndex != currentDialogueIndexMax)
        {
            if (currentDialogueIndex > currentDialogueIndexMax)
            {
                currentDialogueIndex = currentDialogueIndexMax;
            }
            else
            {
                currentDialogueIndex++;
            }
            dialogueText.text = dialogueMessage[currentDialogueIndex];

            if (currentDialogueIndex >= currentDialogueIndexMax)
            {
                SceneManager.LoadScene(CutScene);
            }
        }
    }
}

