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
    public TMP_InputField taskInputField;
    public Button okTaskButton;

    public TMP_Text taskText;
    FirebaseFirestore db;

    void Awake()
    {
        // Initialize the FirebaseFirestore instance in the Awake method
        db = FirebaseFirestore.DefaultInstance;


    }

    void Start()
    {
        // Call the method to add data to Firestore
        taskText.enabled = false;
        okTaskButton.onClick.AddListener(AddTaskToFirestore);
        AddTaskToFirestore();
    }

    public void AddTaskToFirestore()
    {
        taskName = taskInputField.GetComponent<TMP_InputField>().text;
        // Your Firestore data insertion code here...
        DocumentReference docRef = db.Collection("tasks").Document($"{taskName}");

        taskText.SetText(taskName);
        taskText.enabled = true;

        if(taskText.text == taskName){
            System.Console.WriteLine("LETSGOOO");
        }
        

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

        okTaskButton.interactable = false;

    }
}










