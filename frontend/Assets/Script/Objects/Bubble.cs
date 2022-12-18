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
    [SerializeField] public int _id;
    [SerializeField] public int _idTrajectory;
    private bool _draggable = true;

    private Vector3 _dragOffset;
    private Camera _cam; 
    
    float duration; // duration of the apparition of the circle
    int type; // type of the circle



    //Si la bulle est de type toucher prolonge 
    [SerializeField] private Trajectory _trajectory;

    public void SetRadius(float radius) => _radius = radius;
    public void SetId(int id) => _id = id;
    public void SetIdTrajectory(int id) => _idTrajectory = id;


    private void Start() 
    {
        
        //gameObject.AddComponent<Boundaries>();


        if (type == 1) //si la bulle est de type toucher prolongé
        {
            GameObject traj = GameObject.Find("Trajectory " + _idTrajectory + "");
            if (traj != null)
            {
                //la trajectoire devient le parent de la bulle
                _trajectory = traj.GetComponent<Trajectory>();
                transform.parent = _trajectory.transform;

            }
        }
        else { gameObject.GetComponent<CircleCollider2D>().isTrigger = false; }

        CreateRing();
    }

    private CollisionSide CheckIfFloorIsUnder(Collider2D otherCollider)
    {
        var closestPoint = otherCollider.ClosestPoint(gameObject.GetComponent<CircleCollider2D>().bounds.center);
        var distance = closestPoint - (Vector2)otherCollider.bounds.center;
        var angle = Vector2.Angle(Vector2.right, distance);
        
        /*if (angle < 135 && angle > 45)
        {
            return CollisionSide.Under;
        } else if 
        //The rest of sides by angle*/
        return CollisionSide.None;
    }

    public enum CollisionSide
    {
        Under,
        Above,
        Sides,
        None,
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected between : " + name + " and " + collision.gameObject.name);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Collision exited between : " + name + " and " + collision.gameObject.name);
        _draggable = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -5, 0);

        /*switch (CheckIfFloorIsUnder(collision))
        {
            case CollisionSide.Above:
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -5, 0);
            default:
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -5, 0);
                ;

        }*/
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        _draggable = true;
        Debug.Log("Collision stayed between : " + name + " and " + collision.gameObject.name);
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
        Debug.Log("onMouseDown " + name);

        _dragOffset = transform.position - GetMousePos();
    }
    
    private void OnMouseDrag()
    {
        if (_draggable)
        {
            Debug.Log("onMouseDrag " + name);
            Color tmp = _srenderer.color;
            tmp.a = 0.5f;
            _srenderer.color = tmp;
            transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime * 1000);
        }
    }

    private void OnMouseUp()
    {
        Debug.Log("onMouseUp " + name);

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

    private void CreateRing()
    {
        var ring = Instantiate(_ringPrefab, transform);
        var localScale = transform.localScale;
        ring.transform.localScale = new Vector3(1 / localScale.x, 1 / localScale.y, 1);
        ring.SetSpeed(_speed);
        ring.SetRadius(_radius);
        ring.SetDuration(duration);
        ring.transform.parent = transform;
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