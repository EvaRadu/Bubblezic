using Assets.Script;
using System.Collections;
using ProudLlama.CircleGenerator;
using UnityEngine;


public class SemiCircle : MonoBehaviour
{
    private SemiCircle thisBubble; 
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer _srenderer;
    [SerializeField] private float _speed=1000;
    [SerializeField] private float _radius;
    [SerializeField] private int id;
    [SerializeField] private float _rotation;
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
        _circle = new GameObject("SemiCircleLeft");
        _circle.transform.SetParent(transform);

        // def des propriétés intitiale du cercle

        _circle.AddComponent<SpriteRenderer>().color = Color.black;
        _circle.transform.localScale = Vector3.one * 0.1f;

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


    public void setBubble(SemiCircle b)
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



}