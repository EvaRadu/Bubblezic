using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBalls : MonoBehaviour
{
    [SerializeField] private Bubble _circlePrefab;
    //float startTime; // time to wait before creating the circle
    List<Bubble> bubbles = new List<Bubble>();

    void Start() {
        //startTime = 2f;
        //StartCoroutine(waitAndCreate(startTime));
    }

    private void Update()
    {
        waitAndCreate(TimerScript.Instance.time);
    }

    void waitAndCreate(float time)
    {
        foreach (var ball in WsClient.Instance.ballsList)
        {
            if (time>= ball.temps - 0.2 && time <= ball.temps + 0.2 && ball.created == false)
            {
                var spawnedCircle = Instantiate(_circlePrefab, new Vector3(ball.posX, ball.posY, 0), Quaternion.identity); // create a new circle
                spawnedCircle.name = ball.id+"";
                spawnedCircle.setDuration(ball.duration);
                spawnedCircle.transform.localScale = new Vector3(ball.rayon, ball.rayon, 1);
                spawnedCircle.setColor(ball.couleur);
                spawnedCircle.setType(ball.type);
                spawnedCircle.SetRadius(ball.rayon);
                bubbles.Add(spawnedCircle);
                ball.created = true;
                spawnedCircle.setBubble(ball);
            }
        }
    }

    /*IEnumerator waitAndCreate(float delay) {
        foreach (var ball in WsClient.Instance.ballsList)
        {
            yield return new WaitForSeconds(ball.temps);
            var spawnedCircle = Instantiate(_circlePrefab, new Vector3(ball.posX, ball.posY, 0), Quaternion.identity); // create a new circle
            spawnedCircle.name = ball.id+"";
            spawnedCircle.setDuration(ball.duration);
            spawnedCircle.transform.localScale = new Vector3(ball.rayon, ball.rayon, 1);
            spawnedCircle.setColor(ball.couleur);
            spawnedCircle.setType(ball.type);
            spawnedCircle.SetRadius(ball.rayon);
            bubbles.Add(spawnedCircle);
        }
    }*/
}


