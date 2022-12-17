using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour {
    public void getReady()
    {
        WsClient.Instance.getBalls();
    }


    public void LoadScene(string SceneName){
        if (WsClient.Instance.ready)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
