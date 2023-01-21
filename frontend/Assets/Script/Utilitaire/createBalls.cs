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

    /* --- OPPONENT SCREEN --- */
    List<Bubble> opponentBubbles = new List<Bubble>();
    List<Trajectory> opponentTrajectories = new List<Trajectory>();
    List<SemiCircle> opponentSemiCircles = new List<SemiCircle>();

    // Taille des deux écrans : 
    // Grand écran (player)
    float x1 = -8.88f;
    float x2 = 8.8f;
    float y1 = -5f;
    float y2 = 5f;

    // Petit écran (opponent)
    float x3 = 5.8f;
    float x4 = 8.6f;
    float y3 = -4.6f;
    float y4 = -3.27f;



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
                    for (int i = ball.nbMalusMultiple; i > 0; i--)
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
                            spawnedSemiCircle.setScale((0.1f * ball.rayon)/2);
                            semiCircles.Add(spawnedSemiCircle);
                            ball.created = true;
                            spawnedSemiCircle.setBubble(ball);


                            /* --- OPPONENT SCREEN --- */

                            var opponentSemiCircle = Instantiate(_semiCirclePrefab, new Vector3(
                                ((ball.posX - x1) / (x2 - x1)) * (x4 - x3) + x3,
                                ((ball.posY - y1) / (y2 - y1)) * (y4 - y3) + y3,
                                0),

                                    Quaternion.identity); // create a new circle
                            opponentSemiCircle.name = "Opponent Semi Circle " + ball.id + "";
                            opponentSemiCircle.setDuration(ball.duration);
                            opponentSemiCircle.setColor(ball.couleur);
                            opponentSemiCircle.setType(ball.type);
                            opponentSemiCircle.setRotation(ball.rotation);
                            opponentSemiCircle.setSide(ball.side);
                            opponentSemiCircle.setScale(0.025f);
                            opponentSemiCircle.setCanMove(0);
                            opponentSemiCircle.SetIsOpponentSemiCircle(true);
                            opponentSemiCircles.Add(opponentSemiCircle);
                            ball.created = true;
                            opponentSemiCircle.setBubble(ball);
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

                            if (ball.type == 6 || ball.type == 9 || ball.type == 4)
                            {
                                spawnedCircle.setTexture(ball.texture);
                            }

                            //MALUS
                            if (ball.type == 4)
                            {
                                spawnedCircle.setImpulsion(ball.impulsion);
                                spawnedCircle.setPosOpponent(ball.posXOpponent, ball.posYOpponent);
                                spawnedCircle.setFreezeDuration(ball.freezeDuration);
                            }
                            if (ball.type == 5)
                            {
                                spawnedCircle.setImpulsion(ball.impulsion);
                                spawnedCircle.setPosOpponent(ball.posXOpponent, ball.posYOpponent);
                                spawnedCircle.setNbMalusMultiple(i);
                                spawnedCircle.SetId(ball.id+ (i/10));
                            }

                            bubbles.Add(spawnedCircle);
                            ball.created = true;
                            spawnedCircle.setBubble(ball);

                            /* --- OPPONENT SCREEN --- */

                            var opponentSpawnedCircle = Instantiate(_circlePrefab, new Vector3(
                                ((ball.posX - x1) / (x2 - x1)) * (x4 - x3) + x3,
                                ((ball.posY - y1) / (y2 - y1)) * (y4 - y3) + y3,
                                0),

                                  Quaternion.identity); // create a new circle
                            opponentSpawnedCircle.name = "Opponent Bubble " + ball.id + "";
                            opponentSpawnedCircle.SetId(ball.id);
                            opponentSpawnedCircle.setDuration(ball.duration);
                            opponentSpawnedCircle.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                            opponentSpawnedCircle.setColor(ball.couleur);
                            opponentSpawnedCircle.setType(ball.type);
                            opponentSpawnedCircle.SetRadius(0.5f);
                            opponentSpawnedCircle.SetIsOpponentCircle(true);

                            if (ball.type == 6 || ball.type == 9 || ball.type == 4 || ball.type == 5)
                            {
                                opponentSpawnedCircle.setTexture(ball.texture);
                            }

                            //MALUS
                            if (ball.type == 4)
                            {
                                opponentSpawnedCircle.setImpulsion(ball.impulsion);
                                opponentSpawnedCircle.setPosOpponent(ball.posXOpponent, ball.posYOpponent);
                            }
                            if (ball.type == 5)
                            {
                                opponentSpawnedCircle.setImpulsion(ball.impulsion);
                                opponentSpawnedCircle.setPosOpponent(ball.posXOpponent, ball.posYOpponent);
                                opponentSpawnedCircle.setNbMalusMultiple(ball.nbMalusMultiple);
                            }

                            opponentBubbles.Add(opponentSpawnedCircle);
                            opponentSpawnedCircle.setBubble(ball);


                        }
                    }
                }
            }

                if (obj.GetType() == typeof(Trajectoire))
            {
                Trajectoire traj = (Trajectoire) obj;
                if (time >= traj.temps - 0.2 && time <= traj.temps + 0.2 && traj.created == false)
                {
                    Bubble bubble = bubbles.Find(i => i._id == (traj.idBubble - 1));
                    bubble.SetIdTrajectory(traj.id);
                    Bubble cible = bubbles.Find(i => i._id == (traj.idBubble + 1));
                    Debug.Log(cible._id);
                    cible.SetIdTrajectory(traj.id);
                    
                    var spawnedTrajectory = Instantiate(_trajectoryPrefab, new Vector3(traj.posX, traj.posY, 0), Quaternion.identity);
                    spawnedTrajectory.name = "Trajectory " + traj.id + "";
                    spawnedTrajectory.SetId(traj.id);
                    spawnedTrajectory.SetDuration(traj.duration);
                    spawnedTrajectory.SetColor(traj.couleur);
                    spawnedTrajectory.SetBubble(bubble);
                    spawnedTrajectory.SetCible(cible);
                    spawnedTrajectory.SetSize(traj.posX, traj.posY, traj.width, traj.height);

                    trajectories.Add(spawnedTrajectory);
                    traj.created = true;

                    /* --- OPPONENT SCREEN --- */

                    var opponentSpawnedTrajectory = Instantiate(_trajectoryPrefab, new Vector3(
                            ((traj.posX - x1) / (x2 - x1)) * (x4-x3) + x3,
                            ((traj.posY - y1) / (y2 - y1)) * (y4-y3) + y3,
                            0), 
                              
                              Quaternion.identity); 
                    opponentSpawnedTrajectory.name = "Opponent Trajectory " + traj.id + "";
                    opponentSpawnedTrajectory.SetId(traj.id);
                    opponentSpawnedTrajectory.SetDuration(traj.duration);
                    opponentSpawnedTrajectory.SetColor(traj.couleur);
                    opponentSpawnedTrajectory.SetBubble(bubble);
                    opponentSpawnedTrajectory.SetCible(cible);
                    opponentSpawnedTrajectory.setScale(0.5f);
                    opponentTrajectories.Add(opponentSpawnedTrajectory);
                }
            }


        }

    }
}


