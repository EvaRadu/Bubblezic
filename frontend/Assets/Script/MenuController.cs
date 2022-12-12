using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour {
   public void LoadScene(string SceneName){
        WsClient.Instance.getBalls();

        while (WsClient.Instance.ready == false)
        {
            Debug.Log("waiting for opponent");
        }

        SceneManager.LoadScene(SceneName);
   }
}
