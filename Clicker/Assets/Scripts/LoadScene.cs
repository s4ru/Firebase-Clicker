using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private string _sceneceToLoad = "GameScene";

    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChange;
    }

    // Update is called once per frame
   private void HandleAuthStateChange(object sender, EventArgs e)
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            SceneManager.LoadScene(_sceneceToLoad);
        }
    }

    private void OnDestroy()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChange;
    }


}
