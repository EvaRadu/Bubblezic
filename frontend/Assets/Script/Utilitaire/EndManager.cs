using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class EndManager : MonoBehaviour {

    public static EndManager Instance { get; private set; }


    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start(){

    }

    public void reStart(){
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }


    void Update(){
        Debug.Log("ScoreTeam : " + PlayerPrefs.GetInt("ScoreTeam"));
        Debug.Log("ScoreOpponent : " + PlayerPrefs.GetInt("ScoreOpponent"));
        GameObject.Find("ScoreTeam").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("ScoreTeam").ToString() + " pts";
        GameObject.Find("ScoreOpponent").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("ScoreOpponent").ToString() + " pts";
    }

}