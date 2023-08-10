using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class LogOutManager : MonoBehaviour
{
    FirebaseUser currentUser;
    public FirebaseAuth auth;

    void Awake()
    {
        currentUser = AuthManager.instance.User;
        auth = AuthManager.instance.auth;
        
    }

    public void Logout()
    {
        string displayName = currentUser.DisplayName;
        if (auth != null)
        {
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
