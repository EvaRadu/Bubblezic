using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer _srenderer;
    
    float duration; // duration of the apparition of the circle
    public float moveSpeed = 10;

    Vector3 mousePositionOffset;

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Start()
    {
        duration = 25;
    }


    public void setColor(Color color)
    {
        this.color = color;
        _srenderer.material.color = color;

    }

    private void onMouseDown()
    {
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void onMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
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
        
        
       
        
    

   
