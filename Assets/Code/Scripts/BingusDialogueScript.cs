using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BingusDialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textcomponent;
    public string[] lines;
    public float textSpeed;

    private bool isDialogueVisible = false;
    private bool isShowingLine = false;
    private GameObject dialogueBox;
    private List<int> usedLineIndices = new List<int>();

    private int index;

    void Start()
    {
        dialogueBox = GameObject.Find("BingusDialogueBox");
        HideDialogue();
        dialogueBox.SetActive(false);
    }

    public void OnButtonClick()
    {
        if (!isDialogueVisible && !isShowingLine)
        {
            ShowRandomLine();
        }
        else
        {
            HideDialogue();
        }
    }

    public void ShowRandomLine()
    {
        int randomIndex = GetRandomUnusedLineIndex();
        if (randomIndex != -1)
        {
            dialogueBox.SetActive(true);
            textcomponent.text = string.Empty;
            index = randomIndex;
            StartCoroutine(TypeLine());
            isDialogueVisible = true;
            usedLineIndices.Add(randomIndex);
        }
        else
        {
            Debug.Log("All lines have been displayed.");
            ResetUsedLineIndices();
            HideDialogue();
        }
    }

    public void HideDialogue()
    {
        StopAllCoroutines();
        textcomponent.text = string.Empty;
        dialogueBox.SetActive(false);
        isDialogueVisible = false;
        isShowingLine = false;
    }

    IEnumerator TypeLine()
    {
        isShowingLine = true;

        foreach (char c in lines[index].ToCharArray())
        {
            textcomponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isShowingLine = false;
    }

    public int GetRandomUnusedLineIndex()
    {
        List<int> unusedIndices = new List<int>();
        for (int i = 0; i < lines.Length; i++)
        {
            if (!usedLineIndices.Contains(i))
            {
                unusedIndices.Add(i);
            }
        }

        if (unusedIndices.Count > 0)
        {
            int randomIndex = Random.Range(0, unusedIndices.Count);
            return unusedIndices[randomIndex];
        }
        else
        {
            return -1;
        }
    }

    public void ResetUsedLineIndices()
    {
        usedLineIndices.Clear();
    }
}

