using UnityEngine;

public class PersistentManagerScript : MonoBehaviour
{
    public static PersistentManagerScript Instance { get; private set; }
    public int score = 0;
    public int opponentScore = 0;
    public string bubbleToDelete = "";
    public string semiCircleToMove = "";
    public float semiCircleToMovePosX = 0;
    public float semiCircleToMovePosY = 0;

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

    private void Update()
    {
        Destroy(GameObject.Find(bubbleToDelete));

        if (GameObject.Find(semiCircleToMove) != null)
        {
            GameObject.Find(semiCircleToMove).transform.position = new Vector3(semiCircleToMovePosX, semiCircleToMovePosY, 0);
        }
     
    }
}