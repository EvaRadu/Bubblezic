using Assets.Script;
using System.Collections;
using ProudLlama.CircleGenerator;
using UnityEngine;

/* --- Demi cercle pour l'interaction puzzle --- */
public class SemiCircle : MonoBehaviour
{
    private Bulle thisBubble; 
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer _srenderer;
    //[SerializeField] private float _speed=1000;
    [SerializeField] private float _radius;
    [SerializeField] private int id;
    [SerializeField] private float _rotation;
    [SerializeField] private int side;
    private Vector3 _dragOffset;
    private Camera _cam; 
    private int canMove = 1;  // 1 = on peut déplacer le demi cercle, 0 = on ne peut pas le déplacer
    
     // Variable pour stocker le cercle 
    private GameObject _circle;
    float duration; // duration of the apparition of the circle
    int type; // type of the circle

    private bool _isOpponentSemiCircle = false;


    public void SetRadius(float radius) => _radius = radius;
    public void SetIsOpponentSemiCircle(bool b) => _isOpponentSemiCircle = b;

    private void Start() 
    {
        gameObject.AddComponent<CircleCollider2D>();
        //gameObject.AddComponent<Boundaries>();
       
        // Creation d'un new GameObject pour le circle, c'est un "enfant" de la balle
        _circle = new GameObject("SemiCircle");
        _circle.transform.SetParent(transform);

        // def des propriétés intitiale du cercle

        _circle.AddComponent<SpriteRenderer>().color = Color.black;
        _circle.transform.localScale = Vector3.one * 0.1f;

    }


    private void Awake()
    {
        _cam = Camera.main;
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

    public void setRotation(float rotation)
    {
        this._rotation = rotation;
        this.transform.Rotate(0, 0, rotation);
    }

    public void setSide(int side)
    {
        this.side = side;
    }

    public void setCanMove(int canMove)
    {
        this.canMove = canMove;
        if(canMove == 0){
            GetComponent<AudioSource>().Play(); // On joue le son
        }
    }

    public void setScale(float scale)
    {
        this.transform.localScale = new Vector3(scale, scale, 1);
    }

    public int GetSide()
    {
        return this.side;
    }

    public int getCanMove()
    {
        return this.canMove;
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
    
    private void multiTouch(){
           for(int i = 0; i < Input.touchCount; i++)  // Pour chaque toucher sur l'écran
        {   
                if(Input.GetTouch(i).phase == TouchPhase.Moved){
                    Touch touch = Input.GetTouch(i);
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    touchPos.z = 0;
                    RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(touchPos.x,touchPos.y), Vector2.zero);       
                    if (hitinfo.collider != null){
                        if(hitinfo.collider.gameObject == gameObject){  // Si le toucher est sur le demi cercle
                        if(canMove == 1){
                        gameObject.transform.position = touchPos;
                        }
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
        if(!_isOpponentSemiCircle){
        multiTouch();
        }
       
    }
    

}