using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskScript : MonoBehaviour
{
    public string taskName;
    public GameObject InputField;
    public TMP_Text TextDisplay;
    FirebaseFirestore db;
    public Button clearTaskButton;
    public Button CheckCompleteButton;

    void Awake()
    {
        // Initialize the FirebaseFirestore instance in the Awake method
        db = FirebaseFirestore.DefaultInstance;

    }

    private void Start()
    {
        TextDisplay.enabled = false;
        clearTaskButton.onClick.AddListener(DeleteTask);
        CheckCompleteButton.onClick.AddListener(MarkTaskComplete);
        DeleteTask();
        MarkTaskComplete();
    }

    public void StoreName()
    {
        taskName = InputField.GetComponent<TMP_InputField>().text;
        TextDisplay.SetText(taskName);
        TextDisplay.enabled = true;

        if (taskName != null)
        {
            //InputField.enabled = false;
            InputField.SetActive(false);
        }

        AddTaskToFirestore();
    }

    public void AddTaskToFirestore()
    {
        DocumentReference docRef = db.Collection("tasks").Document($"{taskName}");


        Dictionary<string, object> task = new Dictionary<string, object>
        {

            {"task", $"{taskName}"},
            {"isComplete", false}

        };

        docRef.SetAsync(task).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Noice! It worked!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Error adding data to Firestore: " + task.Exception);
            }
        });
   
        }

    public void MarkTaskComplete()
    {
        if (TextDisplay.text == $"<s>{taskName}</s>")
        {
            TextDisplay.SetText(taskName);
        }
        else
        {
            TextDisplay.SetText($"<s>{taskName}</s>");
        }
        
    }

    public void DeleteTask()
    {
        if (taskName != "" && taskName != null)
        {
            DocumentReference docRef = db.Collection("tasks").Document(taskName);
            docRef.DeleteAsync();
        }

        TextDisplay.enabled = false;
        InputField.SetActive(true);
        InputField.GetComponent<TMP_InputField>().text = "";
    }

}
