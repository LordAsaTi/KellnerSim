using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    public static DialogueSystem Instance { get; set; }

    public GameObject dialoguePanel;
    public GameObject selectPanel;
    private GameObject choicePanel;
    public List<string> dialogueLines = new List<string>();
    private int dialogueIndex = 0;
    private string npcName = "Empty";
    private Text dialogueText;
    private Button continueButton;
    public Button buttonPref;
    private Text nameText;


    private void Awake()
    {
        dialoguePanel.SetActive(true);
        selectPanel.SetActive(true);

        dialogueText = dialoguePanel.transform.GetChild(0).GetComponent<Text>();
        continueButton = dialoguePanel.transform.GetChild(1).GetComponent<Button>();
        nameText = dialoguePanel.transform.GetChild(2).GetComponent<Text>();
        choicePanel = selectPanel.transform.GetChild(0).GetChild(0).gameObject;

        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });

        dialoguePanel.SetActive(false);
        selectPanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        //stopMovement
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        this.npcName = npcName;
        CreateDialogue();
    }
    public void AddNewDialogue(string line, string npcName)
    {
        string[] lines = { line };
        AddNewDialogue(lines, npcName);
    }
    public void AddChoice(string choice)
    {

    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
    }
    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
            if (selectPanel != null)
            {
                selectPanel.SetActive(false);
                //removeChoices();
            }
        }
    }
}