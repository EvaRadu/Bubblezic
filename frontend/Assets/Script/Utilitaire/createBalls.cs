using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBalls : MonoBehaviour
{
    [SerializeField] private Bubble _circlePrefab;
    [SerializeField] private Trajectory _trajectoryPrefab;
    [SerializeField] private SemiCircle _semiCirclePrefab;

    //[SerializeField] private Bubble _semiCirclePrefab;
    float startTime; // time to wait before creating the circle
    List<Bubble> bubbles = new List<Bubble>();
    List<Trajectory> trajectories = new List<Trajectory>();
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
        foreach (var obj in WsClient.Instance.ObjectsList)
        {

            if (obj.GetType() == typeof(Bulle))
            {
                Bulle ball = (Bulle) obj;

                if (time >= ball.temps - 0.2 && time <= ball.temps + 0.2 && ball.created == false)
                {


                    if (ball.type == 7)
                    { // Type 7 = Semi Circle
                        var spawnedSemiCircle = Instantiate(_semiCirclePrefab, new Vector3(ball.posX, ball.posY, 0), Quaternion.identity); // create a new semi circle
                        spawnedSemiCircle.name = "Semi Circle " + ball.id + "";
                        spawnedSemiCircle.setDuration(ball.duration);
                        spawnedSemiCircle.setColor(ball.couleur);
                        spawnedSemiCircle.setType(ball.type);
                        spawnedSemiCircle.setRotation(ball.rotation);
                        spawnedSemiCircle.setSide(ball.side);
                        semiCircles.Add(spawnedSemiCircle);
                        ball.created = true;
                        spawnedSemiCircle.setBubble(ball);
                    }
                    else
                    {
                        var spawnedCircle = Instantiate(_circlePrefab, new Vector3(ball.posX, ball.posY, 0), Quaternion.identity); // create a new circle
                        spawnedCircle.name = "Bubble " + ball.id + "";
                        spawnedCircle.SetId(ball.id);
                        spawnedCircle.setDuration(ball.duration);
                        spawnedCircle.transform.localScale = new Vector3(ball.rayon, ball.rayon, 1);
                        spawnedCircle.setColor(ball.couleur);
                        spawnedCircle.setType(ball.type);
                        spawnedCircle.SetRadius(ball.rayon);

                        if (ball.type == 6)
                        {
                            spawnedCircle.setTexture(ball.texture);
                        }
                        bubbles.Add(spawnedCircle);
                        ball.created = true;
                        spawnedCircle.setBubble(ball);
                    }
                }
            }

            if (obj.GetType() == typeof(Trajectoire))
            {
                Trajectoire traj = (Trajectoire) obj;
                if (time >= traj.temps - 0.2 && time <= traj.temps + 0.2 && traj.created == false)
                {
                    Bubble bubble = bubbles.Find(i => i._id == traj.idBubble);
                    bubble.SetIdTrajectory(traj.id);
                    var spawnedTrajectory = Instantiate(_trajectoryPrefab, new Vector3(traj.posX, traj.posY, 0), Quaternion.identity);
                    spawnedTrajectory.name = "Trajectory " + traj.id + "";
                    spawnedTrajectory.SetDuration(traj.duration);
                    spawnedTrajectory.SetColor(traj.couleur);
                    spawnedTrajectory.SetBubble(bubble);
                    spawnedTrajectory.SetSize(traj.posX, traj.posY, traj.width, traj.height);
                    trajectories.Add(spawnedTrajectory);
                    traj.created = true;
                }
            }


        }

    }
}


