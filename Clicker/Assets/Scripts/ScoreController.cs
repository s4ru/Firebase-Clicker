using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    DatabaseReference mDatabase; 
    string UserId; 
    int score = 0;

    
    public Canvas leaderboardText;
    public GameObject IncrementerButton;
    public GameObject HighScoresButton;
    public List<TextMeshProUGUI> scoreList;
    public List<TextMeshProUGUI> usernameList;

    void Start()
    {
       
       mDatabase = FirebaseDatabase.DefaultInstance.RootReference;
       UserId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
       GetUserScore();
    }
       public void WriteNewScore(int score)
       {
       mDatabase.Child("users").Child(UserId).Child("score").SetValueAsync(score);
       }
       public void GetUserScore()
       {

          FirebaseDatabase.DefaultInstance
            .GetReference("users/"+UserId+"/score")
            .GetValueAsync().ContinueWithOnMainThread(task =>         
          {
          if (task.IsFaulted)
          {
              Debug.Log(task.Exception);
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              Debug.Log("Score: "+snapshot.Value);
              score = (int)snapshot.Value;
              GameObject.Find("LabelScore").GetComponent<TMPro.TMP_Text>().text = "Score: "+score;
          }
          });
       }

    public void GetUsersHighestScores()
    {
        int index = 0;
        FirebaseDatabase.DefaultInstance
            .GetReference("users").OrderByChild("score").LimitToLast(5)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                   
                    DataSnapshot snapshot = task.Result;

                    List<KeyValuePair<string, int>> userScores = new List<KeyValuePair<string, int>>();

                    foreach (var userDoc in (Dictionary<string, object>)snapshot.Value)
                    {
                        var userObject = (Dictionary<string, object>)userDoc.Value;
                        string username = userObject["username"].ToString();
                        int userScore = Convert.ToInt32(userObject["score"]);

                        userScores.Add(new KeyValuePair<string, int>(username, userScore));

                                          
                    }

                    userScores.Sort((a, b) => b.Value.CompareTo(a.Value));

                    foreach (var userScore in userScores)
                    {
                        string username = userScore.Key;
                        int score = userScore.Value;


                        usernameList[index].text = username;
                        scoreList[index].text = score.ToString();

                        index++;
                    }

                }
            });
    }

    public void Scores()
    {
        Destroy(IncrementerButton);
        Destroy(HighScoresButton);
        WriteNewScore(score);
        GetUserScore();
        GetUsersHighestScores();
        leaderboardText.gameObject.SetActive(true);
    }

    public void IncrementScore()
     {
     score += 100;
     GameObject.Find("LabelScore").GetComponent<TMPro.TMP_Text>().text = "Score: " + score;
     WriteNewScore(score);
    }

}

public class UserData
{
    public int score;
    public string username;
}