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
        db = FirebaseManager.instance.db;
    }

    private void Start()
    {
        TextDisplay.enabled = false;
        clearTaskButton.onClick.AddListener(DeleteTask);
        CheckCompleteButton.onClick.AddListener(MarkTaskComplete);
    }

    public void StoreName()
    {
        taskName = InputField.GetComponent<TMP_InputField>().text;

        if (string.IsNullOrEmpty(taskName))
        {
            Debug.Log("Task name is empty. Cannot add an empty task.");
            return;
        }

        TextDisplay.SetText(taskName);
        TextDisplay.enabled = true;

        // Hide the input field when a valid task name is entered
        InputField.SetActive(false);

        AddTaskToFirestore();
    }


    public void AddTaskToFirestore()
    {
        var currentUser = UserManager.instance.User;
        //DocumentReference docRef = db.Collection("tasks").Document($"{taskName}");
        DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId).Collection("tasks").Document($"{taskName}");



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

        UpdateTaskCompleteStatus();

    }

    public void DeleteTask()
    {
        var currentUser = UserManager.instance.User;
        if (taskName != "" && taskName != null)
        {
            DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId).Collection("tasks").Document($"{taskName}");
            docRef.DeleteAsync();
        }

        TextDisplay.enabled = false;
        InputField.SetActive(true);
        InputField.GetComponent<TMP_InputField>().text = "";
    }

    async void UpdateTaskCompleteStatus()
    {
        var currentUser = UserManager.instance.User;
        DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId).Collection("tasks").Document($"{taskName}");

        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            Dictionary<string, object> data = snapshot.ToDictionary();

            if (data.TryGetValue("isComplete", out object isCompleteObj) && isCompleteObj is bool currentIsComplete)
            {
                bool newIsComplete = !currentIsComplete;

                Dictionary<string, object> update = new Dictionary<string, object>
            {
                {"isComplete", newIsComplete}
            };

                await docRef.SetAsync(update, SetOptions.MergeAll);
            }
            else
            {
                Debug.LogWarning("isComplete field not found or not of boolean type.");
            }
        }
        else
        {
            Debug.LogWarning("Document doesn't exist.");
        }
    }

}
