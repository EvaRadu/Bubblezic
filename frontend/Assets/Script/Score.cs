using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text myTextScore;
    private int scoreNum;

    // Start is called before the first frame update
    void Start()
    {
        scoreNum = PersistentManagerScript.Instance.score;
        myTextScore.text = "Score = " + scoreNum;
    }

    // Update is called once per frame
    void Update()
    {
        scoreNum = PersistentManagerScript.Instance.score;
    }
}
