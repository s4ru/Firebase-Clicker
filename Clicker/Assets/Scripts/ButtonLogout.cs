using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonLogout : MonoBehaviour 
{
    [SerializeField]
    private string _sceneceToLoad = "Login";
    public void SignOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        Debug.Log("Salio de la sesion");
        SceneManager.LoadScene(_sceneceToLoad);
    }
}
