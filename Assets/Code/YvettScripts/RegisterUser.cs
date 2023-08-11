using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using System.Threading.Tasks;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RegisterUser : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public FirebaseAuth auth;
    public FirebaseUser User;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    FirebaseFirestore db;


    void Awake()
    {
        // Initialize the FirebaseFirestore instance in the Awake method
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log($"Auth manager instance ID: {gameObject.GetInstanceID()}");

        // added variables
        auth = AuthManager.instance.auth;
        User = AuthManager.instance.User;
    }

    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }


    internal IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result

                User = new FirebaseUser(RegisterTask.Result.User);


                if (User != null)
                {

                    // Your Firestore data insertion code here...
                    DocumentReference docRef = db.Collection("Users").Document(User.UserId);

                    Dictionary<string, object> user = new Dictionary<string, object>
                        {
                             {"username", _username},
                             { "money", 0 },
                             { "hamster", "" }
                        };

                    docRef.SetAsync(user).ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCompleted)
                        {
                            Debug.Log("Added new user to the  users collection.");

                            CollectionReference inventoryCollectionRef = docRef.Collection("inventory");

                            // Create an array of inventory data (example data)
                            List<Dictionary<string, object>> inventoryDataList = new List<Dictionary<string, object>>
                                {
                                    new Dictionary<string, object>
                                    {
                                        {"name", "broom"},
                                        {"type", "furniture"},
                                        {"equipped", false}
                                    },
                                    new Dictionary<string, object>
                                    {
                                        {"name", "cardboard box"},
                                        {"type", "furniture"},
                                        {"equipped", false }
                                    },
                                    new Dictionary<string, object>
                                    {
                                        {"name", "head leaf"},
                                        {"type", "accessories"},
                                        {"equipped", false }
                                    },
                                    new Dictionary<string, object>
                                    {
                                        {"name", "hair bows"},
                                        {"type", "accessories"},
                                        {"equipped", false }
                                    }
                                };

                            foreach (var inventoryData in inventoryDataList)
                            {
                                inventoryCollectionRef.AddAsync(inventoryData).ContinueWithOnMainThread(inventoryTask =>
                                {
                                    if (inventoryTask.IsCompleted)
                                    {
                                        Debug.Log("Inventory data added to subcollection.");

                                        ScenesManager.Instance.LoadPickAHamsterPage();
                                    }
                                    else if (inventoryTask.IsFaulted)
                                    {
                                        Debug.LogError("Error adding inventory data to Firestore: " + inventoryTask.Exception);
                                    }
                                });
                            }
                        }
                        else if (task.IsFaulted)
                        {
                            Debug.LogError("Error adding data to Firestore: " + task.Exception);
                        }
                    });

                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }

}