using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    public static DialogueSystem Instance { get; set; }

    public bool dialogueActive;

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
        dialogueActive = false;

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

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        dialoguePanel.SetActive(true);

        dialogueActive = true;
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
            CloseDialogue();
        }
    }
    public void CloseDialogue()
    {
        if (selectPanel != null)
        {
            selectPanel.SetActive(false);
            RemoveChoices();
        }
        dialoguePanel.SetActive(false);
        //StopStartPlayerMovement(true);
        continueButton.gameObject.SetActive(true);

        dialogueActive = false;
    }

    public void AddChoice(string choice, UnityEngine.Events.UnityAction method)
    {
        Button button = Instantiate(buttonPref, choicePanel.transform);
        button.transform.GetChild(0).GetComponent<Text>().text = choice;
        button.GetComponent<Button>().onClick.AddListener(method);
    }
    public void CreateChoice()
    {
        selectPanel.SetActive(true);
    }
    private void RemoveChoices()
    {
        for(int i = 0; i < choicePanel.transform.childCount; i++)
        {
            Destroy(choicePanel.transform.GetChild(i).gameObject);
        }
    }
}