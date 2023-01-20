using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {
    

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
                Destroy(GameObject.Find("Scene2"));
            }

            else{
                SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
            }
                   
        }
    }

    public void switchTime()
    {
        TimerScript.Instance.switchTime();
    }

   
}
