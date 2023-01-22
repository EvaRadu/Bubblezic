using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float time;
    public static TimerScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        time = Time.timeSinceLevelLoad;
    }

    // Pause the time and music + change the button text
    public void Pause()
    {
        Time.timeScale = 0;
        if(PersistentManagerScript.Instance.music == "1"){
        GameObject.Find("Music1").GetComponent<AudioSource>().Pause();
        }
        else if(PersistentManagerScript.Instance.music == "2"){
        GameObject.Find("Music2").GetComponent<AudioSource>().Pause();
        }
        GameObject.Find("PauseText").GetComponentInChildren<TextMeshProUGUI>().text = "RESUME";

    }

    // Resume the time and music + change the button text
    public void Resume()
    {
        Time.timeScale = 1;
        if(PersistentManagerScript.Instance.music == "1"){
        GameObject.Find("Music1").GetComponent<AudioSource>().Play();
        }
        else if(PersistentManagerScript.Instance.music == "2"){
        GameObject.Find("Music2").GetComponent<AudioSource>().Play();
        }
        GameObject.Find("PauseText").GetComponentInChildren<TextMeshProUGUI>().text = "PAUSE";
        
    }

    public void switchTime()
    {
        if (Time.timeScale == 1)
        {
            Pause();
            WsClient.Instance.Pause();
        }
        else
        {
            Resume();
            WsClient.Instance.Resume();

        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Time = "+time;
        time = Time.timeSinceLevelLoad;
    }
}
