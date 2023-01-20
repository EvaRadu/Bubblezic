using UnityEngine;
using System.Collections;
public class PersistentManagerScript : MonoBehaviour
{
    public static PersistentManagerScript Instance { get; private set; }
    public int score = 0;
    public int opponentScore = 0;
    public string bubbleToDelete = "";
    public string circleToMove = "";
    public float circleToMovePosX = 0;
    public float circleToMovePosY = 0;
    public float freezeDuration = 0;
    public int counter = 0;
    public bool FREEZE = false;

    //Malus Multiple
    public bool MALUSMULTIPLE = false;

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

    private void malusMultiple()
    {
        //Instanciate();
    }

     void freeze() {
        var foundBubbles = FindObjectsOfType<Bubble>();
        foreach (Bubble bubble in foundBubbles)
        {
            Debug.Log("freezing " + bubble.name);
            bubble.setFreeze(true);
        }
        var foundSemiCircles = FindObjectsOfType<SemiCircle>();
        foreach (SemiCircle semiCircle in foundSemiCircles)
        {
            Debug.Log("freezing " + semiCircle.name);
            semiCircle.setFreeze(true);
        }
        StartCoroutine(timer(foundBubbles, foundSemiCircles));
        
    }

    private IEnumerator timer(Bubble[] foundBubble, SemiCircle[] foundSemiCircle)
    {

        yield return new WaitForSeconds(freezeDuration);
        foreach (Bubble bubble in foundBubble)
        {
            bubble.setFreeze(false);
        }
        foreach (SemiCircle semiCircle in foundSemiCircle)
        {
            semiCircle.setFreeze(false);
        }
        FREEZE = false;
    }

    private void Update()
    {
        Destroy(GameObject.Find(bubbleToDelete));

        if (GameObject.Find(circleToMove) != null)
        {
            GameObject.Find(circleToMove).transform.position = new Vector3(circleToMovePosX, circleToMovePosY, 0);
        }

        if (FREEZE)
        {
            freeze();
        }

        if (MALUSMULTIPLE)
        {

            MALUSMULTIPLE = false;
        }
     
    }
}