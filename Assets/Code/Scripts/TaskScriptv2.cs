using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskScriptv2 : MonoBehaviour
{
    public List<GameObject> inputFields = new List<GameObject>();
    public List<TMP_Text> textDisplays = new List<TMP_Text>();
    public List<Button> clearTaskButtons = new List<Button>();
    public List<Button> checkCompleteButtons = new List<Button>();

    FirebaseFirestore db;

    async void Awake()
    {
        db = FirebaseManager.instance.db;

        List<Dictionary<string, object>> taskInfoList = await GetTasks();

        for (int i = 0; i < inputFields.Count; i++)
        {
            GameObject inputField = inputFields[i];
            TMP_Text textDisplay = textDisplays[i];
            Button clearTaskButton = clearTaskButtons[i];
            Button checkCompleteButton = checkCompleteButtons[i];

            textDisplay.enabled = false;

            if (i < taskInfoList.Count)
            {
                string taskName = taskInfoList[i]["task"] as string;
                bool isComplete = (bool)taskInfoList[i]["isComplete"];

                textDisplay.SetText(taskName);
                textDisplay.enabled = true;
                inputField.SetActive(false);

                if (isComplete)
                {
                    textDisplay.SetText($"<s>{taskName}</s>");
                }
            }
        }
    }

    public async Task<List<Dictionary<string, object>>> GetTasks()
    {
        var currentUser = UserManager.instance.User;

        List<Dictionary<string, object>> tasksList = new List<Dictionary<string, object>>();
        Query alltasksQuery = db.Collection("Users").Document(currentUser.UserId).Collection("tasks");
        QuerySnapshot alltaskQuerySnapshot = await alltasksQuery.GetSnapshotAsync();

        foreach (DocumentSnapshot documentSnapshot in alltaskQuerySnapshot.Documents)
        {
            Dictionary<string, object> documentData = documentSnapshot.ToDictionary();

            if (documentData.TryGetValue("task", out object taskValue) && taskValue is string task)
            {
                if (documentData.TryGetValue("isComplete", out object isCompleteValue) && isCompleteValue is bool isComplete)
                {
                    Dictionary<string, object> taskInfo = new Dictionary<string, object>
                {
                    {"task", task},
                    {"isComplete", isComplete}
                };
                    tasksList.Add(taskInfo);
                }
            }
        }

        return tasksList;
    }

    public void AddTaskToFirestore(int taskIndex)
    {
        string taskName = inputFields[taskIndex].GetComponent<TMP_InputField>().text;
        TMP_Text textDisplay = textDisplays[taskIndex];

        if (string.IsNullOrEmpty(taskName))
        {
            Debug.Log("Task name is empty. Cannot add an empty task.");
            return;
        }

        textDisplay.SetText(taskName);
        textDisplay.enabled = true;

        inputFields[taskIndex].SetActive(false);

        var currentUser = UserManager.instance.User;
        DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId).Collection("tasks").Document(taskName);

        Dictionary<string, object> task = new Dictionary<string, object>
        {
            {"task", taskName},
            {"isComplete", false}
        };

        docRef.SetAsync(task).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Task added to Firestore: " + taskName);
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Error adding data to Firestore: " + task.Exception);
            }
        });
    }

    public void MarkTaskComplete(int taskIndex)
    {
        if (taskIndex < 0 || taskIndex >= textDisplays.Count)
        {

            Debug.LogError("Invalid task index.");
            return;
        }
        else
        {
            TMP_Text textDisplay = textDisplays[taskIndex];
            string taskName = textDisplay.text;

            if (taskName.Contains("<s>") && taskName.Contains("</s>"))
            {
                taskName = taskName.Replace("<s>", "").Replace("</s>", "");
                textDisplay.SetText(taskName);
            }
            else
            {
                textDisplay.SetText($"<s>{taskName}</s>");
            }

            UpdateTaskCompleteStatus(taskName);
        }

    }


    public void DeleteTask(int taskIndex)
    {
        if(taskIndex < 0 || taskIndex >= textDisplays.Count)
        {

            Debug.LogError("Invalid task index.");
            return;
        }

        TMP_Text textDisplay = textDisplays[taskIndex];
        GameObject inputField = inputFields[taskIndex];
        string taskName = textDisplay.text;

        if (!string.IsNullOrEmpty(taskName))
        {
            var currentUser = UserManager.instance.User;
            DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId).Collection("tasks").Document(taskName);
            docRef.DeleteAsync();
        }
        else
        {
            Debug.LogWarning("Task name is empty. Cannot delete an empty task.");
            return;
        }

        textDisplay.enabled = false;
        textDisplay.SetText("");
        inputField.SetActive(true);
        inputField.GetComponent<TMP_InputField>().text = "";
    }


    async void UpdateTaskCompleteStatus(string taskName)
    {
        if (taskName.Contains("<s>") && taskName.Contains("</s>"))
        {
           taskName = taskName.Replace("<s>", "").Replace("</s>", "");
        }

        if (!string.IsNullOrEmpty(taskName))
        {
            var currentUser = UserManager.instance.User;
            DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId).Collection("tasks").Document(taskName);

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

}
