using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Collaboration : MonoBehaviour
{
    private bool pressed;
    private Rigidbody2D _rigid;

    private Vector2 oldPosition;
    private double _rangeMinCible = 0.30;
    
    private static int _currentNumCible = 0;
    private Vector2 _screenBounds;

    
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _screenBounds.x -= 0.5f;
        _screenBounds.y -= 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentNumCible == 0)//rg //xY
        {
            if ((this.transform.position.x > -6.8 - _rangeMinCible && this.transform.position.x < -6.8 + _rangeMinCible)
                && (this.transform.position.y > 4.03 - _rangeMinCible && this.transform.position.y < 4.03 + _rangeMinCible))
                // on peut considérer que la balle est dans la cible
            {
                _currentNumCible++;//creation de la new blanche
                Instantiate(this.gameObject, new Vector3(-6.8f, -4.03f, 0), Quaternion.identity); // create next bubble
                Destroy(this.gameObject);
            }
        } else if (_currentNumCible == 1)//vrt
        {
            if ((this.transform.position.x > 6.8 - _rangeMinCible && this.transform.position.x < 6.8 + _rangeMinCible)
                && (this.transform.position.y > 4.03 - _rangeMinCible && this.transform.position.y < 4.03 + _rangeMinCible))
                // on peut considérer que la balle est dans la cible
            {
                _currentNumCible++;
                //Instantiate(this.gameObject, new Vector3(-4.8f, -2.03f, 0), Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        

        if (pressed)
        {
            var position = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            //_rigid.position = new Vector3(position.x, position.y, 0);
            oldPosition = position;
            _rigid.position = oldPosition;
        }
        
        // empêche la balle de sortir du cadre
        this.gameObject.transform.position = new Vector3(
            Mathf.Clamp(this.gameObject.transform.position.x, -_screenBounds.x, _screenBounds.x),
            Mathf.Clamp(this.gameObject.transform.position.y, -_screenBounds.y, _screenBounds.y),
            0);
    }

    private void OnMouseDown()
    {
        pressed = true;
    }

    private void OnMouseUp()
    {
        pressed = false;
        var speed = ((Vector2)Camera.main!.ScreenToWorldPoint(Input.mousePosition) - oldPosition) / Time.deltaTime;
        _rigid.velocity = speed;
    }
    
    private void OnMouseDrag()
    {
        pressed = false;
        var speed = ((Vector2)Camera.main!.ScreenToWorldPoint(Input.mousePosition) - oldPosition) / Time.deltaTime / 15;
        // limiter la vitesse de la balle
        /*speed.x = speed.x % 20;
        speed.y = speed.y % 20;*/
        _rigid.velocity = speed;
    }
}
