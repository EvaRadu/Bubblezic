using Assets.Script;
using System.Collections;
using ProudLlama.CircleGenerator;
using UnityEngine;


public class Bubble : MonoBehaviour
{
    private Bulle thisBubble; 
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer _srenderer;
    [SerializeField] private float _speed=1000;
    [SerializeField] private Ring _ringPrefab;
    [SerializeField] private float _radius;
    [SerializeField] private int id;
    private Vector3 _dragOffset;
    private Camera _cam; 
    
     // Variable pour stocker le cercle 
    private GameObject _circle;
    float duration; // duration of the apparition of the circle
    int type; // type of the circle

    public void SetRadius(float radius) => _radius = radius;

    private void Start() 
    {
        gameObject.AddComponent<CircleCollider2D>();
        //gameObject.AddComponent<Boundaries>();
       
        // Creation d'un new GameObject pour le circle, c'est un "enfant" de la balle
        _circle = new GameObject("Circle");
        _circle.transform.SetParent(transform);

        // def des propriétés intitiale du cercle

        _circle.AddComponent<SpriteRenderer>().color = Color.black;
        _circle.transform.localScale = Vector3.one * 0.1f;

        CreateBubble();
    }


    private void Awake()
    {
        _cam = Camera.main;
    }

    private Vector3 GetMouseWorldPosition()
    {
        return _cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void setType(int type)
    {
        this.type = type;
    }


    public void setBubble(Bulle b)
    {
        this.thisBubble = b;
    }

    public void setColor(Color color)
    {
        this.color = color;
        _srenderer.material.color = color;

    }

    public void setDuration(float dur)
    {
        this.duration = dur;
    }

    public void setColor(string color)
    {
        this.color = (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
        _srenderer.material.color = this.color;
    }


    private void CreateBubble()
    {
        var bubble = Instantiate(_ringPrefab, transform);
        var localScale = transform.localScale;
        bubble.transform.localScale = new Vector3(1 / localScale.x, 1 / localScale.y, 1);
        bubble.SetSpeed(_speed);
        bubble.SetRadius(_radius);
        bubble.SetDuration(duration);
    }

    private void multiTouch(){
         for(int i = 0; i < Input.touchCount; i++)  // Pour chaque toucher sur l'écran
        {   
            if(type == 0){  // Si on touche une balle de type 0 : on la détruit
                Touch touch = Input.GetTouch(i);
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(touchPos.x,touchPos.y), Vector2.zero);       
                if (hitinfo.collider != null){
                    float time = TimerScript.Instance.time;
                    WsClient.Instance.updateScore(this.thisBubble, time);
                    Destroy(hitinfo.collider.gameObject);
                }
            }

            else 
            {
                if(type == 1) { // Si on touche une balle de type 1 : on la déplace
                    if(Input.GetTouch(i).phase == TouchPhase.Moved){
                        //Debug.Log("Down");
                        Touch touch = Input.GetTouch(i);
                        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                        touchPos.z = 0;
                        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(touchPos.x,touchPos.y), Vector2.zero);       
                        if (hitinfo.collider != null){
                            hitinfo.collider.gameObject.transform.position = touchPos;
                            //Color tmp = _srenderer.color;
                            //tmp.a = 0.5f;
                            //_srenderer.color = tmp;
                        }
                    }

                    else if(Input.GetTouch(i).phase == TouchPhase.Ended){
                        //Debug.Log("Up");                 
                        //Color tmp = _srenderer.color;
                        //tmp.a = 1f;
                        //_srenderer.color = tmp;
                    }
                }
            }

          

        }
    }

    public void Update(){

        /* --- GESTION DE LA DISPARITION DU CERCLE --- */

        duration -= Time.deltaTime;
        gameObject.SetActive(true);

        if (duration <= 0)
        {
            if (gameObject.activeSelf)   
            {  
                gameObject.SetActive(false); 
            }      
        }
        else { gameObject.SetActive(true);}

        /* --- GESTION DU MULTI-TOUCH --- */
        
        multiTouch();

    }

}