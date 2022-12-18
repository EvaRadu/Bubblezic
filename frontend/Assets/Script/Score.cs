using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public static Score Instance { get; private set; }

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

    void Start()
    {
        gameObject.GetComponent<Text>().text = "Score = " + score;
    }

    void Update()
    {
        //Debug.Log("Update score");
        gameObject.GetComponent<Text>().text  = "Score = " + score;
    }
}
