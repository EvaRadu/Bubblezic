using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private TMPro.TextMeshProUGUI thisText;
    private int score;

    void Start()
    {
        thisText = GetComponent<TMPro.TextMeshProUGUI>();

        // set score value to be zero
        score = 0;
    }

    void Update()
    {
        // When P is hit
        if (Input.GetKeyDown(KeyCode.P))
        {
            // add 500 points to score
            score += 500;
        }
        // update text of Text element
        thisText.text = "Score is " + score;
    }

}
