using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Firebase;
using System.Threading.Tasks;


public class HamsterManager : MonoBehaviour
{
    FirebaseFirestore db;
    FirebaseUser currentUser;

    void Awake()
    {
        db = FirebaseManager.instance.db;
        currentUser = UserManager.instance.User;
        Debug.Log($"Ready to update hamster for {currentUser.DisplayName}");
    }


    public async Task UpdateHamsterToUser(string name)
    {
        if (currentUser == null)
        {
            Debug.LogError("User is not signed in!");
        }

        DocumentReference usersDocRef = db.Collection("Users").Document(currentUser.UserId);

        Dictionary<string, object> update = new Dictionary<string, object>
            {
                { "hamster", name }
            };
        await usersDocRef.SetAsync(update, SetOptions.MergeAll);
        //ScenesManager.Instance.LoadMainPage();
        ScenesManager.Instance.LoadHamLoadScene();

        Debug.Log($"Hamster {name} has been added to {currentUser.DisplayName}.");
    }

    public void UpdateHamsterToUserButton(string name)
    {
        _ = UpdateHamsterToUser(name);
    }

}