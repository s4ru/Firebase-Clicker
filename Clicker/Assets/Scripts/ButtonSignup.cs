using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSignup : MonoBehaviour
{
    [SerializeField]
    private Button _registrationButton;
    private Coroutine _registrationCoroutine;

    private DatabaseReference mDatabaseRef;
    void Reset()
    {
        _registrationButton = GetComponent<Button>();
    }

    private void Start()
    {
        _registrationButton.onClick.AddListener(HandleRegisterButtonClick);
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void HandleRegisterButtonClick()
    {
        string email = GameObject.Find("InputEmail").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("InputPassword").GetComponent<TMP_InputField>().text;

        StartCoroutine(RegisterUser(email, password));
    }

    private IEnumerator RegisterUser(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance; 
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => registerTask.IsCompleted); 

        if (registerTask.IsCanceled) 
        { 
            Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled."); 
        }
        else if (registerTask.IsFaulted) 
        { 
            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + registerTask.Exception); 
        }
        else
        {            
                  Firebase.Auth.AuthResult result = registerTask.Result;
                  Debug.LogFormat("Firebase user created successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);

                  string name = GameObject.Find("InputUsername").GetComponent<TMP_InputField>().text;
                  mDatabaseRef.Child("users").Child(result.User.UserId).Child("username").SetValueAsync(name);
        }
    }
}