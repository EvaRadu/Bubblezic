using UnityEngine;
using UnityEngine.UI;

public class DeleteBubble : MonoBehaviour
{
    public string bubbleToDelete = "";
    public static DeleteBubble Instance { get; private set; }

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
        if(GameObject.Find(bubbleToDelete) != null)
        {
            Destroy(gameObject.GetComponent<Bubble>());
        }
    }
}
