using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBalls : MonoBehaviour
{
    [SerializeField] private Bubble _circlePrefab;
    [SerializeField] private Trajectory _trajectoryPrefab;

    //[SerializeField] private Bubble _semiCirclePrefab;
    float startTime; // time to wait before creating the circle
    List<Bubble> bubbles = new List<Bubble>();
    List<Trajectory> trajectories = new List<Trajectory>();


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
        foreach (var obj in WsClient.Instance.ObjectsList)
        {

            if (obj.GetType() == typeof(Bulle))
            {
                Bulle ball = (Bulle) obj;
                if (time >= ball.temps - 0.2 && time <= ball.temps + 0.2 && ball.created == false)
                {
                    var spawnedCircle = Instantiate(_circlePrefab, new Vector3(ball.posX, ball.posY, 0), Quaternion.identity); // create a new circle
                    spawnedCircle.name = "Bubble " + ball.id + "";
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

            if (obj.GetType() == typeof(Trajectoire))
            {
                Trajectoire traj = (Trajectoire) obj;
                if (time >= traj.temps - 0.2 && time <= traj.temps + 0.2 && traj.created == false)
                {
                    var spawnedTrajectory = Instantiate(_trajectoryPrefab, new Vector3(traj.posX, traj.posY, 0), Quaternion.identity);
                    trajectories.Add(spawnedTrajectory);
                    traj.created = true;
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


