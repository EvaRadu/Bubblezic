using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer _srenderer;

    public void setColor(Color color)
    {
        this.color = color;
        _srenderer.material.color = color;
    }

    public void Update(){
        
        if(Input.GetMouseButtonDown(0)){ // from le R
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePos);
        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(mousePos.x,mousePos.y), Vector2.zero);       
        if (hitinfo.collider != null){
            Debug.Log("Clicked on the bubble"); 
            Destroy(hitinfo.collider.gameObject);
            }
        }
        }
        
    }

   
