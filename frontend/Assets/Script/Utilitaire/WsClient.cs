using UnityEngine;
using WebSocketSharp;
using System;
using System.Collections.Generic;
using Assets.Script;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class WsClient : MonoBehaviour
{
    public Button startButton;
    public Button readyButton;
    public Button connectButton;
    public InputField inputURL;

    WebSocket ws;
    public static WsClient Instance { get; private set; }
    public List<myObjects> ObjectsList = new List<myObjects>();
    public bool ready = false;
    public bool connected = false;

    public string serverUrl;

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

    private void Start()
    {
        serverUrl = "ws://localhost:8080";
        inputURL.text = serverUrl;
    }

    public void changeUrl()
    {
        serverUrl = inputURL.text;
        Debug.Log(serverUrl);
    }

    public void connectToServer()
    {
        ws = new WebSocket(serverUrl);

        ws.OnError += (sender, e) =>
        {
            Debug.Log("error : " + e);
        };

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("Connexion is on");
            connected = true;

        };

        ws.OnMessage += (sender, e) =>
        {
            if (e.Data.Contains("New score"))
            {
                int pos1 = e.Data.IndexOf("=");
                Score.Instance.score = Int16.Parse(e.Data.Substring(pos1 + 2));
                PersistentManagerScript.Instance.score = Int16.Parse(e.Data.Substring(pos1 + 2));
            }

            if (e.Data.Contains("Opponent score"))
            {
                int pos1 = e.Data.IndexOf("=");
                OpponentScore.Instance.score = Int16.Parse(e.Data.Substring(pos1 + 2));
            }

            if(e.Data.Contains("Delete Bubble"))
            {
                //Debug.Log("HERE Delete Bubble");
                int pos1 = e.Data.IndexOf("=");
                string name = e.Data.Substring(pos1 + 2);
                Debug.Log("NAME=" + name);
                Scene scene = SceneManager.GetSceneByName("Scene2");
                GameObject[] rootObjects = scene.GetRootGameObjects();
                foreach (GameObject rootObject in rootObjects)
                {
                    Debug.Log("ROOT OBJECT NAME=" + rootObject.name);
                }                //Destroy(name.Instance);
                /*GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                foreach (GameObject go in allObjects)
                {
                    Debug.Log("GO NAME=" + go.name);
                }*/
                /*foreach(Bubble b in allObjects){
                    Debug.Log("BUBBLE NAME=" + b.gameObject.name);
                    if(b.gameObject.name == name)
                        Debug.Log("Destroying " + b.gameObject.name);
                        Destroy(b);
                }*/
                //GameObject obj = GameObject.Find(name);
                //Debug.Log("NAME=" + name);
                //Debug.Log("OBJ=" + obj.GetComponent<Bubble>().getBubbleName());
                //Destroy(GameObject.Find(name).GetComponent<Bubble>());
                //GameObject.Find(
            }

        };

        ws.Connect();

        ws.OnMessage += (sender, e) =>
        {
            if (e.Data.Contains("New score ="))
            {
                Debug.Log(e.Data);
            }
            //PersistentManagerScript.Instance.score++;
        };
    }


    private void Update()
    {
        if (connected && !ready)
        {
            readyButton.interactable = true;
            connectButton.interactable = false;
        }
        if (ready && connected)
        {
            startButton.interactable = true;
            readyButton.interactable = false;
        }
    }

    public void getBalls()
    {
        try
        {
            if (ws == null)
            {
                Debug.Log("getBalls returned null");
                return;
            }
            else
            {
                ws.Send("Ready");
                ws.OnMessage += (sender, e) =>
                {
                    String typeName = DesarializedObject.typeForTheList(e.Data);
                    var objet = DesarializedObject.CreateFromJSON(e.Data);

                    switch (typeName)
                    {
                        case "bubble":
                            ObjectsList.Add((Bulle)objet);
                            break;
                        default:
                            ObjectsList.Add((Trajectoire)objet);
                            break;

                    }
                    Debug.Log(ObjectsList.ToString());
                    ready = true;

                };
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }


    public void updateScore(Bulle b, float time, int type)
    {
        try
        {
            if (ws == null)
            {
                Debug.Log("updateScore returned null");
                return;
            }
            else
            {

                ws.Send("Update Score. ballId =" + b.id + ", time= " + time + ", type= " + type);
                ws.OnMessage += (sender, e) =>
                {
                    Debug.Log(e.Data);
                };
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void deleteBubble(string name)
    {
        try
        {
            if (ws == null)
            {
                Debug.Log("Null");
                return;
            }
            else
            {

                ws.Send("Delete Bubble. bubbleName =" + name);
                ws.OnMessage += (sender, e) =>
                {
                    Debug.Log(e.Data);
                };
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}