using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class LogOutUser : MonoBehaviour
{
    public FirebaseAuth auth;

    public void Logout()
    {
        auth = AuthManager.instance.auth;

        if (auth != null)
        {
            auth.SignOut();
            Debug.Log($"User signed out!");
            ScenesManager.Instance.LoadStartPage();
        }
        else
        {
            Debug.LogError("Sign out failed!");
        }
    }
}
