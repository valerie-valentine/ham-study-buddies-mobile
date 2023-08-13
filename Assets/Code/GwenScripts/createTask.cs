using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddTask : MonoBehaviour
{   
    public string taskName;
    public GameObject taskInputField;
    public Button okTaskButton;

    public TMP_Text taskText;
    FirebaseFirestore db;

    void Awake()
    {
        db = FirebaseManager.instance.db;
    }

    void Start()
    {
        // Call the method to add data to Firestore
        taskText.enabled = false;
        okTaskButton.onClick.AddListener(MarkTaskComplete);
        MarkTaskComplete();
    }

    public void AddTaskToFirestore()
    {
        taskName = taskInputField.GetComponent<TMP_InputField>().text;

        DocumentReference docRef = db.Collection("tasks").Document($"{taskName}");

        taskText.SetText(taskName);
        taskText.enabled = true;



        Dictionary<string, object> task = new Dictionary<string, object>
        {

            {"task", $"{taskName}"},
            {"isComplete", false}

        };

        docRef.SetAsync(task).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("it worked");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Error adding data to Firestore: " + task.Exception);
            }
        });

        if (taskName != null)
        {
            taskInputField.SetActive(false);
        };

    }
       public void MarkTaskComplete()
        {
            if (taskText.text == $"<s>{taskName}</s>")
            {
                taskText.SetText(taskName);
            }
            else
            {
                taskText.SetText($"<s>{taskName}</s>");
            }
        }
}










