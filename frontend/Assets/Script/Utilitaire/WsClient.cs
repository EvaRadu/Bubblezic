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
    public bool freeze = false;

    WebSocket ws;
    public static WsClient Instance { get; private set; }
    public List<myObjects> ObjectsList = new List<myObjects>();
    public bool ready = false;
    public bool connected = false;  
    public bool demo = false;

    public string serverUrl;

    public bool bonus = false;
    public int bonusScore = 0;


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
        Debug.Log("Start");
        serverUrl = "ws://localhost:8080";
        inputURL.text = serverUrl;

    }

    public void setDemo()
    {
        bool previousDemo = demo;
        Toggle toggle = GameObject.Find("Toggle").GetComponent<Toggle>();
        this.demo = toggle.isOn;
        if(previousDemo != demo){
            updateDemo();
        }
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
                int pos2 = e.Data.IndexOf("bonusStatus");
                Score.Instance.score = Int16.Parse(e.Data.Substring(pos1 + 2, pos2 - pos1 - 4));
                PersistentManagerScript.Instance.score = Int16.Parse(e.Data.Substring(pos1 + 2, pos2 - pos1 - 4));

                int pos3 = e.Data.IndexOf("bonusStatus =");
                int pos4 = e.Data.IndexOf("bonusPoints =");

                int pos5 = e.Data.IndexOf("points =");

                bool.TryParse(e.Data.Substring(pos3 + 14, pos4 - pos3 - 16), out Score.Instance.bonusStatus);
                Score.Instance.bonusPoints = Int16.Parse(e.Data.Substring(pos4 + 14, pos5 - pos4 - 16));

                Score.Instance.points = Int16.Parse(e.Data.Substring(pos5 + 9));

                Debug.Log("TEST A : " + Int16.Parse(e.Data.Substring(pos4 + 14, pos5 - pos4 - 16)));
                Debug.Log("TEST B : " + Int16.Parse(e.Data.Substring(pos4 + 14, pos5 - pos4 - 16)));


            }

            else if (e.Data.Contains("Opponent score"))
            {
                int pos1 = e.Data.IndexOf("=");
                int pos2 = e.Data.IndexOf("bonusStatus");
                OpponentScore.Instance.score = Int16.Parse(e.Data.Substring(pos1 + 2, pos2 - pos1 - 4));
                PersistentManagerScript.Instance.opponentScore = Int16.Parse(e.Data.Substring(pos1 + 2, pos2 - pos1 - 4));

                int pos3 = e.Data.IndexOf("bonusStatus =");
                int pos4 = e.Data.IndexOf("bonusPoints =");
                int pos5 = e.Data.IndexOf("points =");
                bool.TryParse(e.Data.Substring(pos3 + 14, pos4 - pos3 - 16), out OpponentScore.Instance.bonusStatus);
                OpponentScore.Instance.bonusPoints = Int16.Parse(e.Data.Substring(pos4 + 14, pos5 - pos4 - 16));
                OpponentScore.Instance.points = Int16.Parse(e.Data.Substring(pos5 + 9));

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

            else if(e.Data.Contains("Freeze Malus Received"))
            {
                Debug.Log("FREEZE MALUS RECEIVED");
                int pos1 = e.Data.IndexOf("=");
                PersistentManagerScript.Instance.freezeDuration = float.Parse(e.Data.Substring(pos1 + 2));
                Debug.Log("freezeDuration : " + PersistentManagerScript.Instance.freezeDuration);
                freeze = true;
                PersistentManagerScript.Instance.FREEZE = true;
            }
            else if(e.Data.Contains("Pause")){
                TimerScript.Instance.Pause();
            }

            else if(e.Data.Contains("Resume")){
                TimerScript.Instance.Resume();
            }

            else if(e.Data.Contains("Demo =")){
                int pos1 = e.Data.IndexOf("=");
                string demo = e.Data.Substring(pos1 + 1);
                if(demo == "True"){
                    this.demo = true;
                    // update on toggle
                    Toggle toggle = GameObject.Find("Toggle").GetComponent<Toggle>();
                    toggle.isOn = true;
                }
                else{
                    this.demo = false;
                    // update on toggle
                    Toggle toggle = GameObject.Find("Toggle").GetComponent<Toggle>();
                    toggle.isOn = false;
                }
            }

            else if(e.Data.Contains("Start Scene")){
                SceneManager.LoadScene("Start", LoadSceneMode.Single);
                TimerScript.Instance.Resume();
                Destroy(WsClient.Instance.gameObject);

            }

            else if(e.Data.Contains("Scene 2")){
                SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
                Destroy(WsClient.Instance.gameObject);

            }

            else if(e.Data.Contains("ScoreTeam")){
                int pos1 = e.Data.IndexOf("=");
                int pos2 = e.Data.IndexOf("ScoreOpponent");
                int scoreT = Int16.Parse(e.Data.Substring(pos1 + 2, pos2 - 14));
                int scoreO = Int16.Parse(e.Data.Substring(pos2 + 16));
        
                PersistentManagerScript.Instance.scoreTeam = scoreT;
                PersistentManagerScript.Instance.scoreOpponent = scoreO;            
            }
            

            else if (e.Data.Contains("Multiple Malus Received"))
            {
                Debug.Log("MULTIPLE MALUS RECEIVED");
                int pos1 = e.Data.IndexOf("=");
                // PersistentManagerScript.Instance.MALUSMULTIPLE = true;
                PersistentManagerScript.Instance.malusMultiple();
                WsClient.Instance.updateScore(null, 0f, 10);
            }


        };

        ws.Connect();

        ws.OnClose += (sender, e) =>
        {
            Debug.Log("Connexion is off");
            connected = false;
            ready = false;
        };
    }

    public void closeSocket()
    {
        ws.Close();
    }


    private void Update()
    {
        setDemo();
        if (connected && !ready)
        {
            readyButton.interactable = true;
            connectButton.interactable = false;
        }
        if (ready && connected)
        {
            if(startButton != null && readyButton != null){
            startButton.interactable = true;
            readyButton.interactable = false;
            }
        }
    }

    public void getObjects()
    {
        try
        {
            if (ws == null)
            {
                Debug.Log("getObjects returned null");
                return;
            }
            else
            {
                if (this.demo == true)
                {
                    // Delete the previous ObjectsList
                    ObjectsList.Clear();
                    ws.Send("Ready Demo");
                }
                else
                {
                    ws.Send("Ready");
                }
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
                if (type == 10)
                {
                    ws.Send("Update Score. ballId =" + 0 + ", time= " + 0 + ", type= " + type);
                }
                else
                {
                    ws.Send("Update Score. ballId =" + b.id + ", time= " + time + ", type= " + type);
                }
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

    public void EndMalusFreeze()
    {
        freeze = false;
    }


    public void MalusSentFreeze(string name, float posX, float posY, int duration)
    {
        if (!freeze)
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

                    ws.Send("Freeze Malus Sent. circleName =" + name + ", posX= " + posX + ", poxY= " + posY + ", duration= " + duration);
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

    public void MalusSentMultiple(string name, float posX, float posY, float id)
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

                ws.Send("Multiple Malus Sent. circleName =" + name + ", posX= " + posX + ", poxY= " + posY + ", id= " + id);
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

    public void Pause()
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
                
                ws.Send("Pause");
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

    public void Resume()
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

                ws.Send("Resume");
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

    public void updateDemo(){
        try
        {
            if (ws == null)
            {
                Debug.Log("Null");
                return;
            }
            else
            {

                ws.Send("Demo ="+this.demo);
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

    public void StartScene(){
        try
        {
            if (ws == null)
            {
                Debug.Log("Null");
                return;
            }
            else
            {
                ws.Send("Start Scene");
                TimerScript.Instance.Resume();
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

    public void Scene2(){
        try
        {
            if (ws == null)
            {
                Debug.Log("Null");
                return;
            }
            else
            {

                ws.Send("Scene 2");
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

    public void TEST(String msg)
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

                ws.Send(msg);
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
    public void EndScene(){
        try
        {
            if (ws == null)
            {
                Debug.Log("Null");
                return;
            }
            else
            {

                ws.Send("End Scene");
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