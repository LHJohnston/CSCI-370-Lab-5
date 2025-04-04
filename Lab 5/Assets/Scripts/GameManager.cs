using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameManager Instance { get; private set; }
    void Awake() {
        if(Instance != null)
            Destroy(Instance);
        else
            Instance = this;
        DontDestroyOnLoad(this);
    }

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialoguePanel;

    private int health;

    

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;

public void StartDialogue(string[] dialogue, int startPosition, string name)
    {
        nameText.text = name + "...";
        dialoguePanel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogue, startPosition));
    }

    IEnumerator RunDialogue(string[] dialogue, int startPosition)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();

        for(int i = startPosition; i < dialogue.Length; i++)
        {
            //dialogueText.text = dialogue[i];
            dialogueText.text = null;
            StartCoroutine(TypeTextUncapped(dialogue[i]));

            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        dialoguePanel.SetActive(false);
        dialogueText.text = null;
        nameText.text = null;
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }

    public void ShowDialogue(string dialogue, string name)
    {
        nameText.text = name + "...";
        StartCoroutine(TypeTextUncapped(dialogue));
        dialoguePanel.SetActive(true);
    }

    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;
        dialoguePanel.SetActive(false);
    }

float charactersPerSecond = 90;

IEnumerator TypeTextUncapped(string line)
{
    float timer = 0;
    float interval = 1 / charactersPerSecond;
    string textBuffer = null;
    char[] chars = line.ToCharArray();
    int i = 0;

    while (i < chars.Length)
    {
        if (timer < Time.deltaTime)
        {
            textBuffer += chars[i];
            dialogueText.text = textBuffer;
            timer += interval;
            i++;
        }
        else
        {
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
void Start()
    {
        dialoguePanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
