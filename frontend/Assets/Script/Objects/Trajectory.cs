using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] public int _id;
    public Bubble _bubble;
    [SerializeField] private Color _color;
    [SerializeField] private SpriteRenderer _srenderer;
    public float _width;
    public float _height;
    public float _posX;
    public float _posY;
    public Collider2D[] overlaps = new Collider2D[2];
    public float _duration;


    public void SetDuration(float duration) => _duration = duration;


    public void SetId(int id) => _id = id;
    public void SetBubble(Bubble bubble) => _bubble = bubble;

    public void SetSize(float posX, float posY, float width, float height )
    {
        _posX = posX;
        _posY = posY;
        _width = width;
        _height = height;

        transform.localScale = new Vector2(_width, _height);
    }

    public Vector3[] GetSpriteCorners()
    {
        Vector3 topRight = _srenderer.transform.TransformPoint(_srenderer.sprite.bounds.max);
        Vector3 topLeft = _srenderer.transform.TransformPoint(new Vector3(_srenderer.sprite.bounds.min.x, _srenderer.sprite.bounds.max.y, 0));
        Vector3 botLeft = _srenderer.transform.TransformPoint(_srenderer.sprite.bounds.min);
        Vector3 botRight = _srenderer.transform.TransformPoint(new Vector3(_srenderer.sprite.bounds.max.x, _srenderer.sprite.bounds.min.y, 0));
        return new Vector3[] { topRight, topLeft, botLeft, botRight };
    }

    public void SetColor(string color)
    {
        _color = (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
        _srenderer.material.color = _color;
    }


        // Start is called before the first frame update
        void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _duration -= Time.deltaTime;
        gameObject.SetActive(true);

        // Mise a jour de la taille du cercle avec Mathf.PingPong() (stack)
        //float scale = Mathf.PingPong(Time.time, 0.5f) + 0.1f;
        //_circle.transform.localScale = Vector3.one * scale;

        // Rendre invisible une balle 
        if (_duration <= 0)
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
        else { gameObject.SetActive(true); }

        //var test = this.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), overlaps);
        //Debug.Log( overlaps[0].gameObject.name);
    }
}
