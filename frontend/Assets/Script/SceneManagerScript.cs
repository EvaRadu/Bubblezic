using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    public bool actualMode;

    private void Start()
    {
       this.actualMode = PersistentManagerScript.Instance.mode;
    }

    public void modeCreate()
    {
        PersistentManagerScript.Instance.mode = true;
        //Debug.Log(PersistentManagerScript.Instance.mode);
    }

    public void modeMove()
    {
        PersistentManagerScript.Instance.mode = false;
        //Debug.Log(PersistentManagerScript.Instance.mode);
    }
}
