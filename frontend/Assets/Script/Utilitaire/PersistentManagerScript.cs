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
    public int scoreTeam = -100000;
    public int scoreOpponent = -100000;
    float screenWidth;
    float screenHeight;
    public Bubble _bubblePrefab;
    public int idMalusMultiple;
    public Color bckColor = Color.white;


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



    //Malus Multiple
    public bool MALUSMULTIPLE = false;

    private void Awake()
    {
        // Determine the boundaries of the screen
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        screenHeight = Camera.main.orthographicSize;
        bckColor = Camera.main.backgroundColor;
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
        Debug.Log("Malus creation");


        //Ecran Joueur
        Bubble newObject = Instantiate(_bubblePrefab);
        Vector3 pos = new Vector3(Random.Range(-screenWidth + newObject.transform.localScale.x / 2, screenWidth - newObject.transform.localScale.x / 2), Random.Range(-screenHeight + newObject.transform.localScale.y / 2, screenHeight - newObject.transform.localScale.y / 2), 0);
        Debug.Log(pos);
        newObject.name = "MALUS";
        newObject.transform.position = pos;
        newObject.setDuration(3);
        newObject.setColor(Color.magenta);
        newObject.setType(10);
        newObject.SetRadius(4);
        Debug.Log("Malus created");

    }

    void freeze() {
        Camera.main.backgroundColor = Color.red;
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

        //set background color back
        Debug.Log("change of color : " + bckColor);
        Camera.main.backgroundColor = bckColor;

        //self knows end of freezing malus
        WsClient.Instance.EndMalusFreeze();
        Debug.Log("END OF FREEZING");
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
            malusMultiple();
            MALUSMULTIPLE = false;
        }
     
    }
}