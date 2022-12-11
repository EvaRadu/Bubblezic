using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer _srenderer;
    [SerializeField] private float _speed=1000;
    private Vector3 _dragOffset;
    private Camera _cam;
    
    float duration; // duration of the apparition of the circle

    private void Start()
    {
        duration = 25;
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.AddComponent<Boundaries>();
    }

    private void Awake()
    {
        _cam = Camera.main;
    }
    private Vector3 GetMouseWorldPosition()
    {
        return _cam.ScreenToWorldPoint(Input.mousePosition);
    }
 
    public void setColor(Color color)
    {
        this.color = color;
        _srenderer.material.color = color;

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
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
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

    public void Update(){

        duration -= Time.deltaTime;   
        gameObject.SetActive(true);

        if (duration <= 0)
        {
            if (gameObject.activeSelf)   {  gameObject.SetActive(false);}      
        
        }
            else { gameObject.SetActive(true);}






        //pour qu'une bulle suive la souris
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0)){ // from le R
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log(mousePos);
                RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(mousePos.x,mousePos.y), Vector2.zero);       
                if (hitinfo.collider != null){
                    Debug.Log("Clicked on the bubble");
                    //Destroy(hitinfo.collider.gameObject);
                    
            }
                }
    }
}
        
        
       
        
    

   
