using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text myTextScore;
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
        myTextScore.GetComponent<Text>().text = "Score = " + score;
    }

    void Update()
    {
        myTextScore.GetComponent<Text>().text = "Score = " + score;
    }
}
