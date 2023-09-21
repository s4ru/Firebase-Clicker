using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.UI;

public class ResetPassword : MonoBehaviour
{
    [SerializeField]
    private Button resetPassword;
    [SerializeField]
    private TMP_InputField _emailInputField;
    private void Reset()
    {
        resetPassword = GetComponent<Button>();
        _emailInputField = GameObject.Find("InputEmail").GetComponent<TMP_InputField>();
    }

    void Start()
    {
        resetPassword.onClick.AddListener(HandleResetPasswordButton);
    }

    private void HandleResetPasswordButton()
    {
        string emailAddres = _emailInputField.text;

        var auth = FirebaseAuth.DefaultInstance;

        FirebaseAuth.DefaultInstance.SendPasswordResetEmailAsync(emailAddres).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("Erros Send Password Reset");
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Error Send Passrword encounteres an error: " + task.Exception);
                return;
            }

            Debug.Log("Email was sent!");

        });
    }
}
