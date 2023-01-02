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
    public int freezeDuration = 0;
    public int counter = 0;
    public bool FREEZE = false;

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

     void freeze() {
        var foundBubbles = FindObjectsOfType<Bubble>();
        foreach (Bubble bubble in foundBubbles)
        {
            bubble.setFreeze(true);
        }
        StartCoroutine(Instance.timer());
        foreach (Bubble bubble in foundBubbles)
        {
            bubble.setFreeze(false);
        }
        FREEZE = false;
    }

    private IEnumerator timer()
    {
        while (true) {

            if (counter >= freezeDuration)
            {
                counter = 0;
                yield return null;
            } else
            {
                counter += 1;
                yield return new WaitForSeconds(1);
            }
        }
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
     
    }
}