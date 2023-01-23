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
    public int scoreTeam = -1;
    public int scoreOpponent = -1;
    float screenWidth;
    float screenHeight;
    [SerializeField] private Bubble _bubblePrefab;
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

    public void malusMultiple()
    {
        WsClient.Instance.TEST("Malus creation");
        Camera.main.backgroundColor = Color.red;
        /*Debug.Log("Malus creation");
        


        //Ecran Joueur
        WsClient.Instance.TEST("test0");

       // Vector3 pos = new Vector3(Random.Range(-screenWidth + newObject.transform.localScale.x / 2, screenWidth - newObject.transform.localScale.x / 2), Random.Range(-screenHeight + newObject.transform.localScale.y / 2, screenHeight - newObject.transform.localScale.y / 2), 0);


        // Generate random positions within the screen boundaries
        float x = Random.Range(-screenWidth, screenWidth);
        float y = Random.Range(-screenHeight, screenHeight);
        
       
        // Create a new object at the random position
        Bubble newObject = Instantiate(_bubblePrefab, new Vector3(1, 2, 0), Quaternion.identity);
    
        WsClient.Instance.TEST("test1");

       // Debug.Log(pos);
        newObject.name = "MALUS " + (-id).ToString();
        WsClient.Instance.TEST("test2");
       // newObject.transform.localScale = pos;
        WsClient.Instance.TEST("test3");
        newObject.setDuration(3);
        WsClient.Instance.TEST("test4");

        newObject.SetId(-id);
        WsClient.Instance.TEST("test5");

        newObject.setColor(Color.yellow);
        WsClient.Instance.TEST("test6");

        newObject.setType(10);
        WsClient.Instance.TEST("test7");

        newObject.SetRadius(4);
        WsClient.Instance.TEST("test8");

        Debug.Log("Malus created");
        WsClient.Instance.TEST("SET CREATION");*/
        StartCoroutine(timerMalusMultiple());

    }

    void freeze() {
        Camera.main.backgroundColor = Color.blue;
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

    private IEnumerator timerMalusMultiple()
    {
        yield return new WaitForSeconds(0.5f);
        Camera.main.backgroundColor = bckColor;
        WsClient.Instance.TEST("MALUS MULTIPLE END with bck color = " + bckColor.ToString());
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

        /*if (MALUSMULTIPLE)
        {
            malusMultiple();
            MALUSMULTIPLE = false;
        }*/
     
    }
}