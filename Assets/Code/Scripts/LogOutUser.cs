using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class LogOutUser : MonoBehaviour
{
    public FirebaseAuth auth;
    FirebaseUser currentUser;

    private void Awake()
    {
        currentUser = UserManager.instance.User;
        auth = FirebaseManager.instance.auth;
    }

    public async void Logout()
    {
        var pomoTimer = PomoTimer.instance;

        if (pomoTimer != null)
        {
            await pomoTimer.loseSeedOnLogout();
        }

        if (auth != null)
        {
            string displayName = currentUser.DisplayName;

            auth.SignOut();
            Debug.Log($"User {displayName} signed out!");
            ScenesManager.Instance.LoadStartPage();
        }
        else
        {
            Debug.LogError("Sign out failed!");
        }
    }
}