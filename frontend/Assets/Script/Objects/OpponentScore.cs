using UnityEngine;
using UnityEngine.UI;

public class OpponentScore : MonoBehaviour
{
    public int score = 0;
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
        gameObject.GetComponent<Text>().text = score + " pts";
        GameObject.Find("Slider Opponent").GetComponent<Slider>().value = score;
    }

    void Update()
    {
        //Debug.Log("Update score");
        gameObject.GetComponent<Text>().text = score + " pts";
        GameObject.Find("Slider Opponent").GetComponent<Slider>().value = score;
    }
}
