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
    FirebaseFirestore db;

    void Awake()
    {
        // Initialize the FirebaseFirestore instance in the Awake method
        db = FirebaseFirestore.DefaultInstance;

    }

    void Start()
    {
        // Call the method to add data to Firestore
        
        okTaskButton.onClick.AddListener(AddTaskToFirestore);
        AddTaskToFirestore();
    }

    public void AddTaskToFirestore()
    {
        taskName = taskInputField.GetComponent<TMP_InputField>().text;
        // Your Firestore data insertion code here...
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
                Debug.Log("it worked");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Error adding data to Firestore: " + task.Exception);
            }
        });
    }
}








// public class NewBehaviourScript : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

