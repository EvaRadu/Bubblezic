using UnityEngine;
using WebSocketSharp;
public class WsClient : MonoBehaviour
{
    WebSocket ws;
    private void Start()
    {
        
        ws = new WebSocket("ws://4.tcp.eu.ngrok.io:17323/ws");
       
        Debug.Log("Connected");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from "+((WebSocket)sender).Url+", Data : "+e.Data);
        };

        ws.OnError += (sender, e) =>
        {
            Debug.Log("error : " + e);
        };

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("open : " + e);
        };

        ws.Connect();

        /*

        ws = new WebSocket("ws://ece3-134-59-215-253.eu.ngrok.io/ws");

        using (var ws = new WebSocket ("ws://dragonsnest.far/Laputa")) {
        ws.OnMessage += (sender, e) =>
            Debug.Log("Message Received from "+((WebSocket)sender).Url+", Data : "+e.Data);
        ws.Connect ();
        ws.Send ("HELLO");
        //Console.ReadKey (true);
      }
      */
    }
private void Update()
    {
        try
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
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
          
    }
}