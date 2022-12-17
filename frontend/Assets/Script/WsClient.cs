using UnityEngine;
using WebSocketSharp;
using System;
using System.Collections.Generic;
using Assets.Script;
using UnityEngine.UI;

public class WsClient : MonoBehaviour
{
    public Button startButton;
    public Button readyButton;
    public Button connectButton;
    public InputField inputURL;

    WebSocket ws;
    public static WsClient Instance { get; private set; }
    public List<Bulle> ballsList = new List<Bulle>();
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
            if(e.Data.Contains("New score"))
            {
                int pos1 = e.Data.IndexOf("=");
                Score.Instance.score = Int16.Parse(e.Data.Substring(pos1+2));
                PersistentManagerScript.Instance.score = Int16.Parse(e.Data.Substring(pos1+2));
            }
        };

        ws.Connect();

        ws.OnMessage += (sender, e) =>
        {
            if(e.Data.Contains("New score =")){
                Debug.Log(e.Data);
            }
            //PersistentManagerScript.Instance.score++;
        };
    }

    private void Update()
    {
        if(connected && !ready)
        {
            readyButton.interactable = true;
            connectButton.interactable = false;
        }
        if(ready && connected)
        {
            startButton.interactable = true;
            readyButton.interactable = false;
        }

    }

    private void Update()
    {
        if(connected && !ready)
        {
            readyButton.interactable = true;
            connectButton.interactable = false;
        }
        if(ready && connected)
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
                    var bulle = Bulle.CreateFromJSON(e.Data);
                    ballsList.Add(bulle);
                    ready = true;
                };
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }


    public void updateScore(Bulle b, float time)
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

                ws.Send("Update Score. ballId ="+b.id+", time= "+time);
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