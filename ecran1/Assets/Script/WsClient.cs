using UnityEngine;
using WebSocketSharp;
public class WsClient : MonoBehaviour
{
    WebSocket ws;
    private void Start()
    {
        ws = new WebSocket("ws://ea06-134-59-215-253.eu.ngrok.io/ws");
       
        ws.Connect();
        Debug.Log("Connected");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from "+((WebSocket)sender).Url+", Data : "+e.Data);
        };
    }
private void Update()
    {
        if(ws == null)
        {
            return;
        }
if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("Hello");
            Debug.Log("Message Sent");
        }  
    }
}