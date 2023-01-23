using UnityEngine;
using UnityEngine.UI;

public class OpponentScore : MonoBehaviour
{
    public int score = 0;
    public bool bonusStatus = false;
    public int bonusPoints = 0;
    public int points = 0;
    public static OpponentScore Instance { get; private set; }

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
        gameObject.GetComponent<Text>().text = score + " pts -> points : " + points +  " | bonus : " + bonusPoints;
        GameObject.Find("Slider").GetComponent<Slider>().value = score;
    }

    void Update()
    {
        gameObject.GetComponent<Text>().text = score + " pts -> points : " + points + " | bonus : " + bonusPoints;
        GameObject.Find("Slider").GetComponent<Slider>().value = score;
        }
    }
