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
        {            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
           
        }
    }

   
}
