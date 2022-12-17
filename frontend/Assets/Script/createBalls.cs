using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBalls : MonoBehaviour
{
    [SerializeField] private Bubble _circlePrefab;
    [SerializeField] private SemiCircle _semiCirclePrefab;
    //[SerializeField] private Bubble _semiCirclePrefab;
    float startTime; // time to wait before creating the circle
    List<Bubble> bubbles = new List<Bubble>();
    List<SemiCircle> semiCircles = new List<SemiCircle>();

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
                if(ball.type == 7) { // Type 7 = Semi Circle
                    var spawnedSemiCircle = Instantiate(_semiCirclePrefab, new Vector3(ball.posX, ball.posY, 0), Quaternion.identity); // create a new semi circle
                    spawnedSemiCircle.name = ball.id+"";
                    spawnedSemiCircle.setDuration(ball.duration);
                    spawnedSemiCircle.setColor(ball.couleur);
                    spawnedSemiCircle.setType(ball.type);
                    spawnedSemiCircle.setRotation(ball.rotation);
                    semiCircles.Add(spawnedSemiCircle);
                    ball.created = true;
                    //spawnedSemiCircle.setBubble(ball);
                }
                else{
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
    }

    /*IEnumerator waitAndCreate(float delay) {
        foreach (var ball in WsClient.Instance.ballsList)
        {
            yield return new WaitForSeconds(ball.temps);
            if(ball.type == 7) { // it will be a semi circle
                // TODO
                //var spawnedSemiCircle = Instance(_semiCirclePrefab, new Vector3(ball.posX, ball.posY, 0), Quaternion.identity); // create a new semi circle

            }
            else{
            var spawnedCircle = Instantiate(_circlePrefab, new Vector3(ball.posX, ball.posY, 0), Quaternion.identity); // create a new circle
            spawnedCircle.name = ball.id+"";
            spawnedCircle.setDuration(ball.duration);
            spawnedCircle.transform.localScale = new Vector3(ball.rayon, ball.rayon, 1);
            spawnedCircle.setColor(ball.couleur);
            spawnedCircle.setType(ball.type);
            spawnedCircle.setId(ball.id);
            //spawnedCircle.setBubble(ball);
            spawnedCircle.SetRadius(ball.rayon);
            bubbles.Add(spawnedCircle);
            }
        }
    }*/
}


