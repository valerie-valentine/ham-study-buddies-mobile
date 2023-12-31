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

public class UserManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;
    public TMP_Text welcomeLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    FirebaseFirestore db;

    public static UserManager instance;


    void Awake()
    {
        // added variables
        db = FirebaseManager.instance.db;
        auth = FirebaseManager.instance.auth;
        User = FirebaseManager.instance.User;

        //Do not destroy object
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //async function returning a task string , with async you need await 
    public async Task<string> GetHamster()
    {

        DocumentReference docRef = db.Collection("Users").Document(User.UserId);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        {

            if (snapshot.Exists)
            {
                string hamsterName = snapshot.GetValue<string>("hamster");
                if (string.IsNullOrEmpty(hamsterName))
                {
                    ScenesManager.Instance.LoadPickAHamsterPage();
                }
                else
                {
                    //ScenesManager.Instance.LoadMainPage();
                    ScenesManager.Instance.LoadHamLoadScene();
                }
                return null;

            }
            else
            {
                Debug.Log(System.String.Format("Document {0} does not exist!", snapshot.Id));
                return null;
            }
        }
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Invalid Email or Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email or Password";
                    break;
                case AuthError.UserNotFound:
                    message = "Invalid Email or Password";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = new FirebaseUser(LoginTask.Result.User);
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
            welcomeLoginText.text = $"Welcome {User.DisplayName}";
            _ = GetHamster();
        }
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
                             { "money", 10 },
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
                                        {"name", "Broom"},
                                        {"type", "furniture"},
                                        {"subtype", "decor"},
                                        {"equipped", true}
                                    },
                                    new Dictionary<string, object>
                                    {
                                        {"name", "Cardboardbox"},
                                        {"type", "furniture"},
                                        {"subtype",  "couch"},
                                        {"equipped", true }
                                    },
                                    new Dictionary<string, object>
                                    {
                                        {"name", "HeadLeaf"},
                                        {"type", "accessories"},
                                        {"subtype", "headgear"},
                                        {"equipped", false }
                                    },
                                    new Dictionary<string, object>
                                    {
                                        {"name", "HairBows"},
                                        {"type", "accessories"},
                                        {"subtype", "headgear"},
                                        {"equipped", false }
                                    },
                                     new Dictionary<string, object>
                                    {
                                        {"name", "GreenWallpaper"},
                                        {"type", "furniture"},
                                        {"subtype", "wallpaper"},
                                        {"equipped", true }
                                    },
                                     new Dictionary<string, object>
                                    {
                                        {"name", "RedCurtain"},
                                        {"type", "furniture"},
                                        {"subtype", "curtain"},
                                        {"equipped", true }
                                    }
                                };

                            foreach (var inventoryData in inventoryDataList)
                            {
                                inventoryCollectionRef.AddAsync(inventoryData).ContinueWithOnMainThread(inventoryTask =>
                                {
                                    if (inventoryTask.IsCompleted)
                                    {
                                        Debug.Log("Inventory data added to subcollection.");
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
                        //Login user
                        yield return StartCoroutine(Login(_email, _password));
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }

}
