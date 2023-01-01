using UnityEngine;
using WebSocketSharp;
using System;
using System.Collections.Generic;
using Assets.Script;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using System.Globalization;

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

            else if (e.Data.Contains("Opponent score"))
            {
                int pos1 = e.Data.IndexOf("=");
                OpponentScore.Instance.score = Int16.Parse(e.Data.Substring(pos1 + 2));
                PersistentManagerScript.Instance.opponentScore = Int16.Parse(e.Data.Substring(pos1 + 2));
            }

            else if(e.Data.Contains("Delete Bubble"))
            {
                int pos1 = e.Data.IndexOf("=");
                string name = e.Data.Substring(pos1 + 2);
                PersistentManagerScript.Instance.bubbleToDelete = name;                                 
            }

            else if(e.Data.Contains("Move Circle")){
                int pos1 = e.Data.IndexOf("=");
                int pos2 = e.Data.IndexOf("posX");
                string name =  e.Data.Substring(pos1 + 2, pos2 - pos1 - 4);
                int pos3 = e.Data.IndexOf("posX =");
                int pos4 = e.Data.IndexOf("posY =");
                float posX = float.Parse(e.Data.Substring(pos3 + 7, pos4 - pos3 - 9), CultureInfo.InvariantCulture.NumberFormat);
                float posY = float.Parse(e.Data.Substring(pos4 + 7), CultureInfo.InvariantCulture.NumberFormat);
                PersistentManagerScript.Instance.circleToMove = name;
                PersistentManagerScript.Instance.circleToMovePosX = posX;
                PersistentManagerScript.Instance.circleToMovePosY = posY;
            }

        };

        ws.Connect();
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

    public void getObjects()
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

    public void MoveCircle(string name, float posX, float posY){
        try
        {
            if (ws == null)
            {
                Debug.Log("Null");
                return;
            }
            else
            {

                ws.Send("Move Circle. circleName =" + name + ", posX= " + posX + ", poxY= " + posY);
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