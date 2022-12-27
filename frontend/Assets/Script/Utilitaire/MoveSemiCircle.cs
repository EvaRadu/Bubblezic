using UnityEngine;
using UnityEngine.UI;

public class MoveSemiCircle : MonoBehaviour
{
    public string semiCircleToMove = "";
    public float semiCircleToMovePosX = 0;
    public float semiCircleToMovePosY = 0;    
    public static MoveSemiCircle Instance { get; private set; }

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

    void Start()    
    {
    }

    void Update()
    {
        if(GameObject.Find(semiCircleToMove) != null)
        {
            GameObject.Find(semiCircleToMove).transform.position = new Vector3(semiCircleToMovePosX, semiCircleToMovePosY, 0);
        }
    }
}
