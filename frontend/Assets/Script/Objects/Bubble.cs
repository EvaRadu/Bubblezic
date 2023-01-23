using Assets.Script;
using System.Collections;
using ProudLlama.CircleGenerator;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 


public class Bubble : MonoBehaviour
{
    //  --- CHAMPS COMMUNS --- 
    private Bulle thisBubble;
    public Bubble _bubblePrefab;
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer _srenderer;
    [SerializeField] private float _speed=1000;
    [SerializeField] private Ring _ringPrefab;
    [SerializeField] private float _radius;
    [SerializeField] public float _id;
    [SerializeField] public int _idTrajectory;
    private bool _draggable = true;
    private bool _isOpponentCircle = false;
    private Vector3 _dragOffset;
    private Camera _cam; 
    private string colorName;
    float duration; // duration of the apparition of the circle
    int type; // type of the circle
    private Rigidbody2D _rb;
    private bool instantiated = false;
    private bool touched = false;
    // --------------------------------


    //  --- CHAMPS POUR LE TOUCHER PROLONGE --- 
    [SerializeField] private Trajectory _trajectory;
    // --------------------------------


    //  --- CHAMPS POUR LE PUZZLE --- 
    private int leftSide = 0;      // Variable pour savoir si on a la pièce de puzzle de gauche
    private int rightSide = 0;     // Variable pour savoir si on a la pièce de puzzle de droite
    private GameObject leftPiece;  // Variable pour stocker la pièce de puzzle de gauche
    private GameObject rightPiece; // Variable pour stocker la pièce de puzzle de droite
    // --------------------------------


    //  --- CHAMPS POUR LE MALUS --- 
    private float _posXOpponent = 0f;
    private float _posYOpponent = 0f;
    private float _impulsion = 0f;

    private bool _freeze = false;
    private int _freezeDuration = 0;

    private bool _freezeMalusSent = false;

    private int _nbMalusMultiple = 0;

    public float force = 10f;

    private bool isOutside = false;

    // --------------------------------


    // --- CHAMPS POUR L'ECRAN DE L'ADVERSAIRE ---
    // Grand écran (player)
    /*float x1 = -8.88f;
    float x2 = 8.8f;
    float y1 = -5f;
    float y2 = 5f;
    */
    // Petit écran (opponent)
    float x3 = 5.8f;
    float x4 = 8.6f;
    float y3 = -4.6f;
    float y4 = -3.27f;
    // ------------------------------------------


    // ---------------- SETTERS -----------------
    public void SetRadius(float radius) => _radius = radius;
    public void SetDraggable(bool drag) => _draggable = drag;
    public void SetId(float id) => _id = id;
    public void SetIdTrajectory(int id) => _idTrajectory = id;
    public void SetIsOpponentCircle(bool b) => _isOpponentCircle = b;
    public void setType(int type) { this.type = type; }
    public void setBubble(Bulle b) { this.thisBubble = b; }
    public void setColor(Color color) { this.color = color; _srenderer.material.color = color;}
    public void setDuration(float dur) { this.duration = dur; }
    public void setTexture(string texture) { Sprite sp = Resources.Load<Sprite>(texture); _srenderer.sprite = sp;}
    public void setColor(string color) 
    {
        this.color = (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
        _srenderer.material.color = this.color;
    }

    public void setImpulsion(float impulsion) => _impulsion = impulsion;
    public void setPosOpponent(float X, float Y) { _posXOpponent = X; _posYOpponent = Y; }
    public void setFreezeDuration(int freezeDuration) => _freezeDuration = freezeDuration;
    public void setFreeze(bool freeze) => _freeze = freeze;
    public void setNbMalusMultiple(int nbMalusMultiple) => _nbMalusMultiple = nbMalusMultiple;

    // ------------------------------------------


    // ---------------- GETTERS -----------------
    public string getBubbleName() {return gameObject.name;}
    public Color getColor() {return color;}
    // ------------------------------------------


    // --------------- FUNCTIONS ----------------

    private void Start() 
    {
        //Debug.Log(_id);
        _rb = this.GetComponent<Rigidbody2D>();

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

        if (type == 4 || type == 5 || type == 10)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic; 
        }

        if (type == 5)
        {
            transform.Find("TextContainer").GetComponent<TMP_Text>().SetText(_nbMalusMultiple.ToString());
        }

        if (type==10)
        {
            // AddForce();

            WsClient.Instance.TEST("Multiple Malus Received AND CREATED");
        }

        if(!_isOpponentCircle){
        CreateRing();
        }
    }

    private void AddForce()
    {
        _rb.velocity = new Vector2(0,0);
        _rb.mass=100;
        //_rb.AddForce(Vector2.up * 500f +250f * _rb.velocity.normalized, ForceMode2D.Impulse);
        Vector2 randomForce = Random.insideUnitCircle * force;
        GetComponent<Rigidbody2D>().AddForce(randomForce);
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if(!_isOpponentCircle){
        /* --- Détection des collisions entre la cible d'une trajectoire et la bulle --- */
        if ((type == 9) && (other.gameObject.GetComponent<Bubble>() != null))
        {
            other.gameObject.GetComponent<Bubble>().SetDraggable(false);
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y , 0);  // On place le morceau au bon endroit
            float time = TimerScript.Instance.time;
            WsClient.Instance.updateScore(this.thisBubble, time, 0);
            //Destroy(gameObject
            Destroy(gameObject);
            Destroy(other.gameObject);
            Destroy(_trajectory.gameObject);
        }

        /* --- Détection des collisions entre la cible du puzzle et chacune des pièces --- */
        if ((type == 6) && (other.gameObject.GetComponent<SemiCircle>() != null))
        { // Si on a un côté du puzzle qui rentre en collision avec la cible

            // COTE GAUGHE
            if (other.gameObject.GetComponent<SemiCircle>().GetSide() == 1)
            {
                other.gameObject.GetComponent<SemiCircle>().setCanMove(0);  // On bloque le déplacement 
                other.transform.position = new Vector3(gameObject.transform.position.x - ((0.7f * _radius) / 3), gameObject.transform.position.y, 0);  // On place le morceau au bon endroit
                leftSide = 1;  // On indique qu'on a la pièce de gauche
                leftPiece = other.gameObject;  // On stocke la pièce de gauche
                WsClient.Instance.MoveCircle(other.gameObject.name, other.gameObject.transform.position.x, other.gameObject.transform.position.y);
            }

            // COTE DROIT
            else if (other.gameObject.GetComponent<SemiCircle>().GetSide() == 2)
            {
                other.gameObject.GetComponent<SemiCircle>().setCanMove(0);  // On bloque le déplacement
                other.transform.position = new Vector3(gameObject.transform.position.x + ((0.7f * _radius) / 3), gameObject.transform.position.y, 0); // On place le morceau au bon endroit
                rightSide = 1;  // On indique qu'on a la pièce de droite  
                rightPiece = other.gameObject;  // On stocke la pièce de droite     
                WsClient.Instance.MoveCircle(other.gameObject.name, other.gameObject.transform.position.x, other.gameObject.transform.position.y);     
            }

            // SI ON A LES DEUX MORCEAUX --> ON ENVOIE LE SCORE
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
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(!_isOpponentCircle){

        if (type == 1 && !collision.Equals(gameObject.GetComponent<CircleCollider2D>())) 
        {
            Debug.Log(type ==1);
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
            WsClient.Instance.MoveCircle(gameObject.name, gameObject.transform.position.x, gameObject.transform.position.y);
        }
       
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(!_isOpponentCircle){
            _draggable = true;
            Debug.Log("Collision stayed between : " + name + " and " + collision.gameObject.name);
        }
    }

    private void Awake()
    {
        _cam = Camera.main;
    }

    private Vector3 GetMouseWorldPosition()
    {
        return _cam.ScreenToWorldPoint(Input.mousePosition);
    }

    /*
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
    }*/

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
        RaycastHit2D hit;

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
                    if(hitinfo.collider.gameObject.name == gameObject.name) // Si on touche la balle
                    {
                    if((touchPos.x < x3 || touchPos.x > x4) && (touchPos.y < y3 || touchPos.y > y4)) // Si on est pas dans l'écran adverse
                    {
                    float time = TimerScript.Instance.time;
                    WsClient.Instance.updateScore(this.thisBubble, time, 0);
                    WsClient.Instance.deleteBubble(gameObject.name);
                    Destroy(hitinfo.collider.gameObject);
                    }
                    }
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
                            if (hitinfo.collider.gameObject.GetComponent<Trajectory>() != null)
                            {
                                if ((touchPos.x < x3 || touchPos.x > x4) && (touchPos.y < y3 || touchPos.y > y4))  // Si on est pas dans l'écran adverse
                                {
                                    hitinfo.collider.gameObject.GetComponent<Trajectory>().getBubble().transform.position = touchPos;
                                    Debug.Log("TOUCHEPOS 2 = " + touchPos);
                                    WsClient.Instance.MoveCircle(gameObject.name, gameObject.transform.position.x, gameObject.transform.position.y);
                                }
                            }
                            //hitinfo.collider.gameObject.transform.position = touchPos;
                        }
                    }

                    else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                    {
                    }
                }

                else if ((type == 4 || type == 5))
                {

                    swipe();
                   /* Touch touch = Input.GetTouch(i);
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(touchPos.x, touchPos.y), Vector2.zero);
                    if (hitinfo.collider != null)
                    {
                        if (hitinfo.collider.gameObject.name == gameObject.name) // Si on touche la balle
                        {
                            if ((touchPos.x < x3 || touchPos.x > x4) && (touchPos.y < y3 || touchPos.y > y4)) // Si on est pas dans l'écran adverse
                            {
                                swipe();
                            }
                        }

                    }*/
                }

                /*else if (type == 4)
                {
                    Touch touch = Input.GetTouch(i);
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(touchPos.x, touchPos.y), Vector2.zero);
                    if (hitinfo.collider != null)
                    {
                        if ((touchPos.x < x3 || touchPos.x > x4) && (touchPos.y < y3 || touchPos.y > y4)) // Si on est pas dans l'écran adverse
                        {
                            AddForce();
                            //Destroy(hitinfo.collider.gameObject);
                        }
                    }
                }*/
            }



        }
    }


    private bool CheckTouchCollision(GameObject obj, out RaycastHit2D hit)
    {
        // Get the touch position
        Vector3 touchPos = Input.GetTouch(0).position;

        // Create a ray using the touch position
        Vector2 touchPos2D = new Vector2(touchPos.x, touchPos.y);

        // Perform the raycast and store the information in hit
        hit = Physics2D.Raycast(touchPos2D, Vector2.zero);

        // Check if the raycast hit the specific object
        if (hit.collider != null && hit.collider.gameObject == obj)
        {
            return true;
        }
        return false;
    }

    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;

    [Range(0.05f, 1f)]
    public float throwForce = 0.3f;


    private void swipe()
    {        

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;
            endPos = Input.GetTouch(0).position;
            direction = startPos - endPos;
            GetComponent<Rigidbody2D>().AddForce(-direction / timeInterval * throwForce);
            //WsClient.Instance.MoveCircle(gameObject.name, gameObject.transform.position.x, gameObject.transform.position.y);



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
               /* if (!touched)
                {
                    float time = TimerScript.Instance.time;
                    WsClient.Instance.updateScore(this.thisBubble, time, 0);
                    Destroy(this);
                }*/
            }
        }
        else { gameObject.SetActive(true);}


        //check if malus is out of screen 
        if ((type == 4)  && (!_isOpponentCircle))
        {
            //WsClient.Instance.MalusSent(gameObject.name, gameObject.transform.position.x, gameObject.transform.position.y, _freezeDuration);

            Vector3 viewPos = _cam.WorldToViewportPoint(gameObject.GetComponent<CircleCollider2D>().transform.position);
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                // Your object is in the range of the camera, you can apply your behaviour
            }
            else
            {
                if (!_freezeMalusSent)
                {
                    WsClient.Instance.MalusSentFreeze(gameObject.name, gameObject.transform.position.x, gameObject.transform.position.y, _freezeDuration);
                    WsClient.Instance.deleteBubble(gameObject.name);
                    _freezeMalusSent = true;
                }
            }

        }


        if ((type == 5) && (!_isOpponentCircle))
        {
            //WsClient.Instance.MalusSent(gameObject.name, gameObject.transform.position.x, gameObject.transform.position.y, _freezeDuration);

            Vector3 viewPos = _cam.WorldToViewportPoint(gameObject.GetComponent<CircleCollider2D>().transform.position);
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                // Your object is in the range of the camera, you can apply your behaviour
            }
            else
            {

                if (_nbMalusMultiple > 0 & !instantiated) {
                    WsClient.Instance.MalusSentMultiple(gameObject.name, gameObject.transform.position.x, gameObject.transform.position.y, 3);

                    WsClient.Instance.TEST("creation of next MALUS " + _nbMalusMultiple--);
                    //recup de la bulle
                    Bubble newB = Instantiate(this);
                    newB.transform.position = new Vector3(thisBubble.posX, thisBubble.posY, 0);
                    newB.setNbMalusMultiple(_nbMalusMultiple--);
                    newB.SetId(_id - 0.1f);
                    newB.name = "Malus " + newB._id;
                    newB.setDuration(thisBubble.duration);
                    newB.setType(5);
                    WsClient.Instance.TEST("End of creation");
                    Destroy(this.gameObject);
                    instantiated = true;
                    WsClient.Instance.deleteBubble(gameObject.name);

                }
            }

        }


        /*
        //if (type==4) { Debug.Log("addForce"); AddForce(); }
        if ((Input.GetMouseButtonDown(0)) && type == 4)
        {
            Debug.Log("addForce");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);
            if (hitinfo.collider != null)
            {
                if ((mousePos.x < x3 || mousePos.x > x4) && (mousePos.y < y3 || mousePos.y > y4)) // Si on est pas dans l'écran adverse
                {
                    AddForce();
                    //Destroy(hitinfo.collider.gameObject);
                }
            }
        }
        
        if ((Input.GetMouseButtonDown(0)) && (type == 0)){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(mousePos.x,mousePos.y), Vector2.zero);       
            if (hitinfo.collider != null){
                float time = TimerScript.Instance.time;
                WsClient.Instance.updateScore(this.thisBubble, time, 0);
                Destroy(hitinfo.collider.gameObject); 
            }
        }*/
        if (type == 10)
        {
            /*Vector3 pos = transform.position;
            float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
            float screenHeight = Camera.main.orthographicSize;
            pos.x = Mathf.Clamp(pos.x, -screenWidth + transform.localScale.x / 2, screenWidth - transform.localScale.x / 2);

            // Clamp the object's y-coordinate between the top and bottom edges of the screen
            pos.y = Mathf.Clamp(pos.y, -screenHeight + transform.localScale.y / 2, screenHeight - transform.localScale.y / 2);
        */
            }

        if (!_freeze)
        {
            multiTouch();
        } else
        {
            Debug.Log("FROZEN");
        }
    }

}