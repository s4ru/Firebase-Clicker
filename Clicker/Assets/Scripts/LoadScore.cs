using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadScore : MonoBehaviour
{
    [SerializeField]
    private string _sceneceToLoad = "Scores";


    public void OnPointerClick(PointerEventData eventData)
    {      
        SceneManager.LoadScene(_sceneceToLoad);
    }
}
