using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {
    
    public float endTime = 60f; // time after which the game scene will be switched to the end scene
    public bool updated = false; // to make sure the scores at the end of the game are updated only once

    public void getReady()
    {
        WsClient.Instance.getObjects();
    }


    public void LoadScene(string SceneName){
        if (WsClient.Instance.ready)
        {      
            if(SceneName == "Start" && SceneManager.GetActiveScene().name == "Scene2")
            {
                SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
                WsClient.Instance.StartScene();   
                Destroy(WsClient.Instance.gameObject);
                //Destroy(GameObject.Find("Scene2"));
            }
            else if(SceneName == "Scene2" && SceneManager.GetActiveScene().name == "Start")
            {
                SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
                WsClient.Instance.Scene2();
                Destroy(WsClient.Instance.gameObject);
                //Destroy(GameObject.Find("Start"));
            }
            else{
                // 
            }
                   
        }
    }


    public void switchTime()
    {
        TimerScript.Instance.switchTime();
    }

    public void Update(){
        // Get the current time
        if(SceneManager.GetActiveScene().name == "Scene2"){
        float currentTime = TimerScript.Instance.time;
        
        // If the current time is greater than the end time
        if ((currentTime > endTime) && updated == false)
            {
                // Restores the scores to -100000 to make sure the scores are updated
                PlayerPrefs.SetInt("ScoreTeam", -100000);
                PlayerPrefs.SetInt("ScoreOpponent", -100000);
                PlayerPrefs.Save();

                // End the scene and wait for the scores to be updated
                WsClient.Instance.EndScene();
                while(PersistentManagerScript.Instance.scoreTeam == -100000 && PersistentManagerScript.Instance.scoreOpponent == -100000)
                {
                    // wait for the scores to be updated
                }

                // Save the scores and load the end scene
                PlayerPrefs.SetInt("ScoreTeam", PersistentManagerScript.Instance.scoreTeam);
                PlayerPrefs.SetInt("ScoreOpponent", PersistentManagerScript.Instance.scoreOpponent);
                PlayerPrefs.Save();
                SceneManager.LoadScene("End", LoadSceneMode.Single);
                updated = true; // to make sure the scores are updated only once
            }
        }

        if(SceneManager.GetActiveScene().name == "Start")
        {
            updated = false;
        }
    }

    IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(10);
    }

   
}
