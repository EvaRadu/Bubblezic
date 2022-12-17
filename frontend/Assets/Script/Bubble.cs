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



    //Si la bulle est de type toucher prolonge 
    public GameObject _trajectory;

    public void SetRadius(float radius) => _radius = radius;

    private void Start() 
    {
        gameObject.AddComponent<CircleCollider2D>();
        //gameObject.AddComponent<Boundaries>();


        //PARENT
        _trajectory = new GameObject("Trajectory");
        transform.SetParent(_trajectory.transform);

       
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

    private void OnMouseDown()
    {
        _dragOffset = transform.position - GetMousePos();
    }
    
    private void OnMouseDrag()
    {
        Color tmp = _srenderer.color;
        tmp.a = 0.5f;
        _srenderer.color = tmp;
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime * 1000);
    }

    private void OnMouseUp()
    {
        Color tmp = _srenderer.color;
        tmp.a = 1f;
        _srenderer.color = tmp;
    }

    private Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
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

    public void Update(){
        duration -= Time.deltaTime;
        gameObject.SetActive(true);

         // Mise a jour de la taille du cercle avec Mathf.PingPong() (stack)
        //float scale = Mathf.PingPong(Time.time, 0.5f) + 0.1f;
        //_circle.transform.localScale = Vector3.one * scale;

        // Rendre invisible une balle 
        if (duration <= 0)
        {
            if (gameObject.activeSelf)   
            {  
                gameObject.SetActive(false); 
            }      
        }
        else { gameObject.SetActive(true);}

        //pour qu'une bulle suive la souris
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);

        if ((Input.GetMouseButtonDown(0)) && (type == 0)){ // from le R
            //Debug.Log("BLEU");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(mousePos);
            RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(mousePos.x,mousePos.y), Vector2.zero);       
            if (hitinfo.collider != null){
                float time = TimerScript.Instance.time;
                WsClient.Instance.updateScore(this.thisBubble, time);
                Destroy(hitinfo.collider.gameObject);
            }
        }
    }

}