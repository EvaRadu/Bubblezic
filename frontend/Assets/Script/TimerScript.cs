using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Time = "+time;
        time = Time.timeSinceLevelLoad;
    }
}
