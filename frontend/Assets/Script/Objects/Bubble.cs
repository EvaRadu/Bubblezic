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


    //  --- CHAMPS POUR LE PUZZLE --- 
    private int leftSide = 0;      // Variable pour savoir si on a la pièce de puzzle de gauche
    private int rightSide = 0;     // Variable pour savoir si on a la pièce de puzzle de droite
    private GameObject leftPiece;  // Variable pour stocker la pièce de puzzle de gauche
    private GameObject rightPiece; // Variable pour stocker la pièce de puzzle de droite
    // --------------------------------

    public void SetRadius(float radius) => _radius = radius;
    public void SetDraggable(bool drag) => _draggable = drag;

    public void SetId(int id) => _id = id;
    public void SetIdTrajectory(int id) => _idTrajectory = id;


    private void Start() 
    {
        
        


        if (type == 1 || type == 9) //si la bulle est de type toucher prolongé
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



    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("Collision detected between : " + name + " and " + collision.gameObject.name);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        Debug.Log(other.bounds.Intersects(GetComponent<CircleCollider2D>().bounds));

        /* --- Détection des collisions entre la cible d'une trajectoire et la bulle --- */
        if ((type == 9) && (other.gameObject.GetComponent<Bubble>() != null))
        {
            other.gameObject.GetComponent<Bubble>().SetDraggable(false);
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y , 0);  // On place le morceau au bon endroit
        }

        /* --- Détection des collisions entre la cible du puzzle et chacune des pièces --- */
        if ((type == 6) && (other.gameObject.GetComponent<SemiCircle>() != null))
        { // Si on a un côté du puzzle qui rentre en collision avec la cible

            // COTE GAUGHE
            if (other.gameObject.GetComponent<SemiCircle>().GetSide() == 1)
            {
                other.gameObject.GetComponent<SemiCircle>().setCanMove(0);  // On bloque le déplacement 
                other.transform.position = new Vector3(gameObject.transform.position.x - 0.7f, gameObject.transform.position.y, 0);  // On place le morceau au bon endroit
                leftSide = 1;  // On indique qu'on a la pièce de gauche
                leftPiece = other.gameObject;  // On stocke la pièce de gauche
            }

            // COTE DROIT
            else if (other.gameObject.GetComponent<SemiCircle>().GetSide() == 2)
            {
                other.gameObject.GetComponent<SemiCircle>().setCanMove(0);  // On bloque le déplacement
                other.transform.position = new Vector3(gameObject.transform.position.x + 0.7f, gameObject.transform.position.y, 0); // On place le morceau au bon endroit
                rightSide = 1;  // On indique qu'on a la pièce de droite  
                rightPiece = other.gameObject;  // On stocke la pièce de droite          
            }

            // SI ON A LES DEUX MORCEAUX --> ON DETRUIT LE PUZZLE ET ON ENVOIE LE SCORE
            if ((leftSide == 1) && (rightSide == 1))
            {
                float time = TimerScript.Instance.time;
                WsClient.Instance.updateScore(this.thisBubble, time, 7);
                /*
                Destroy(gameObject);  // On détruit la cible
                Destroy(leftPiece);  // On détruit la pièce de gauche
                Destroy(rightPiece);  // On détruit la pièce de droite
                */
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (type == 1 && !collision.Equals(gameObject.GetComponent<CircleCollider2D>())) 
        {
            //Debug.Log("Collision exited between : " + name + " and " + collision.gameObject.name);
            _draggable = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -10, 0);
            Debug.Log(collision.bounds.Intersects(GetComponent<CircleCollider2D>().bounds));

            //on recupere les extremites de la trajectoire
            Vector3[] positions = _trajectory.GetSpriteCorners();
            Vector3 topRight = (Vector3)positions.GetValue(0);
            Vector3 topLeft = (Vector3)positions.GetValue(0);
            Vector3 botLeft = (Vector3)positions.GetValue(0);
            Vector3 botRight = (Vector3)positions.GetValue(0);

            //topRight, topLeft, botLeft, botRight
            Debug.Log(" topRight " + _trajectory.GetSpriteCorners().GetValue(0));
            Debug.Log(" topLeft " + _trajectory.GetSpriteCorners().GetValue(1));
            Debug.Log(" botLeft " + _trajectory.GetSpriteCorners().GetValue(2));
            Debug.Log(" botRight " + _trajectory.GetSpriteCorners().GetValue(3));

            if (transform.position.x < topLeft.x) //on regarde si la balle depasse a gauche
            { transform.position = collision.bounds.center; }
            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(5, 0, 0); }
            if (transform.position.x > topRight.x) //on regarde si la balle depasse a droite
            { transform.position = collision.bounds.center; }
            if (transform.position.y < botLeft.y) //on regarde si la balle depasse en bas
            { transform.position = collision.bounds.center; }
            if (transform.position.y < topLeft.y) //on regarde si la balle depasse en haut
            { transform.position = collision.bounds.center; }

        }
       
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

    public void setTexture(string texture)
    {
        Sprite sp = Resources.Load<Sprite>(texture);
        _srenderer.sprite = sp;
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



    private void multiTouch()
    {
        for (int i = 0; i < Input.touchCount; i++)  // Pour chaque toucher sur l'écran
        {
            /*  --- TYPE 0 : Balle qui disparait  --- */
            if (type == 0)
            {  // Si on touche une balle de type 0 : on la détruit
                Touch touch = Input.GetTouch(i);
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(touchPos.x, touchPos.y), Vector2.zero);
                if (hitinfo.collider != null)
                {
                    float time = TimerScript.Instance.time;
                    WsClient.Instance.updateScore(this.thisBubble, time, 0);
                    Destroy(hitinfo.collider.gameObject);
                }
            }

            else
            {
                /*  --- TYPE 1 : Balle qu'on déplace  --- */
                if (type == 1)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Moved)
                    {
                        Touch touch = Input.GetTouch(i);
                        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                        touchPos.z = 0;
                        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(touchPos.x, touchPos.y), Vector2.zero);
                        if (hitinfo.collider != null)
                        {
                            hitinfo.collider.gameObject.transform.position = touchPos;
                        }
                    }

                    else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                    {
                    }
                }
            }



        }
    }


    public void Update(){
        duration -= Time.deltaTime;
        gameObject.SetActive(true);

        // Rendre invisible une balle 
        if (duration <= 0)
        {
            if (gameObject.activeSelf)   
            {  
                gameObject.SetActive(false); 
            }      
        }
        else { gameObject.SetActive(true);}



        if ((Input.GetMouseButtonDown(0)) && (type == 0)){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(mousePos.x,mousePos.y), Vector2.zero);       
            if (hitinfo.collider != null){
                float time = TimerScript.Instance.time;
                WsClient.Instance.updateScore(this.thisBubble, time, 0);
                //Destroy(hitinfo.collider.gameObject); 
            }
        }
    }

}