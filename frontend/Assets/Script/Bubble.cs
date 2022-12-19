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

    //  --- CHAMPS POUR LE PUZZLE --- 
    private int leftSide = 0;      // Variable pour savoir si on a la pièce de puzzle de gauche
    private int rightSide = 0;     // Variable pour savoir si on a la pièce de puzzle de droite
    private GameObject leftPiece;  // Variable pour stocker la pièce de puzzle de gauche
    private GameObject rightPiece; // Variable pour stocker la pièce de puzzle de droite
    // --------------------------------
    
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

        CreateRing();

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

    /*
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
    }*/

    public void setDuration(float dur)
    {
        this.duration = dur;
    }

    public void setColor(string color)
    {
        this.color = (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
        _srenderer.material.color = this.color;
    }

    public void setTexture(string texture)
    {
        Sprite sp = Resources.Load<Sprite>(texture);
        _srenderer.sprite = sp;
    }



    private void CreateRing()
    {
        var bubble = Instantiate(_ringPrefab, transform);
        var localScale = transform.localScale;
        bubble.transform.localScale = new Vector3(1 / localScale.x, 1 / localScale.y, 1);
        bubble.SetSpeed(_speed);
        bubble.SetRadius(_radius);
        bubble.SetDuration(duration);
    }




    /* --- DETECTION DES COLLISIONS --- */
    private void OnTriggerEnter2D(Collider2D other)
    {

        /* --- Détection des collisions entre la cible du puzzle et chacune des pièces --- */
        if((type == 6) && (other.gameObject.GetComponent<SemiCircle>() != null)){ // Si on a un côté du puzzle qui rentre en collision avec la cible

            // COTE GAUGHE
            if(other.gameObject.GetComponent<SemiCircle>().GetSide() == 1){ 
                other.gameObject.GetComponent<SemiCircle>().setCanMove(0);  // On bloque le déplacement 
                other.transform.position = new Vector3(gameObject.transform.position.x-0.7f, gameObject.transform.position.y, 0);  // On place le morceau au bon endroit
                leftSide = 1;  // On indique qu'on a la pièce de gauche
                leftPiece = other.gameObject;  // On stocke la pièce de gauche
            }

            // COTE DROIT
            else if(other.gameObject.GetComponent<SemiCircle>().GetSide() == 2){ 
                other.gameObject.GetComponent<SemiCircle>().setCanMove(0);  // On bloque le déplacement
                other.transform.position = new Vector3(gameObject.transform.position.x+0.7f, gameObject.transform.position.y, 0); // On place le morceau au bon endroit
                rightSide = 1;  // On indique qu'on a la pièce de droite  
                rightPiece = other.gameObject;  // On stocke la pièce de droite          
            }

            // SI ON A LES DEUX MORCEAUX --> ON DETRUIT LE PUZZLE ET ON ENVOIE LE SCORE
            if((leftSide == 1) && (rightSide == 1)){  
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
    
    private void multiTouch(){
         for(int i = 0; i < Input.touchCount; i++)  // Pour chaque toucher sur l'écran
        {   
            /*  --- TYPE 0 : Balle qui disparait  --- */
            if(type == 0){  // Si on touche une balle de type 0 : on la détruit
                Touch touch = Input.GetTouch(i);
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(touchPos.x,touchPos.y), Vector2.zero);       
                if (hitinfo.collider != null){
                    float time = TimerScript.Instance.time;
                    WsClient.Instance.updateScore(this.thisBubble, time, 0);
                    Destroy(hitinfo.collider.gameObject);
                }
            }

            else 
            {
                /*  --- TYPE 1 : Balle qu'on déplace  --- */
                if(type == 1) { 
                    if(Input.GetTouch(i).phase == TouchPhase.Moved){
                        Touch touch = Input.GetTouch(i);
                        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                        touchPos.z = 0;
                        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(touchPos.x,touchPos.y), Vector2.zero);       
                        if (hitinfo.collider != null){
                            hitinfo.collider.gameObject.transform.position = touchPos;
                        }
                    }

                    else if(Input.GetTouch(i).phase == TouchPhase.Ended){
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