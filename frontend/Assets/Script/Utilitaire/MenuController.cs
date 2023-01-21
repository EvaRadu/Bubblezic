using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {
    
    public float endTime = 30f; // time after which the game scene will be switched to the end scene

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
        float currentTime = Time.timeSinceLevelLoad;
        // If the current time is greater than the end time
        if (currentTime > endTime)
        {
            // Load the scene
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            WsClient.Instance.EndScene();
            while(PersistentManagerScript.Instance.scoreTeam == 0 && PersistentManagerScript.Instance.scoreOpponent == 0)
            {
                // wait for the scores to be updated
            }
            PlayerPrefs.SetInt("ScoreTeam", PersistentManagerScript.Instance.scoreTeam);
            PlayerPrefs.SetInt("ScoreOpponent", PersistentManagerScript.Instance.scoreOpponent);
            PlayerPrefs.Save();
            SceneManager.LoadScene("End", LoadSceneMode.Single);
        }
        }
    }

    IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(10);
    }

   
}
