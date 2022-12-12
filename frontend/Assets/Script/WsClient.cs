using UnityEngine;
using WebSocketSharp;
using System;
using System.Collections.Generic;
using Assets.Script;

public class WsClient : MonoBehaviour
{
    WebSocket ws;
    public static WsClient Instance { get; private set; }
    public List<Bulle> ballsList = new List<Bulle>();
    public bool ready = false;

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
        ws = new WebSocket("ws://localhost:8080/ws");

        ws.OnError += (sender, e) =>
        {
            Debug.Log("error : " + e);
        };

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("Connexion is on");
        };

        ws.Connect();
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
}