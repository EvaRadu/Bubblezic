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

        var test = this.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), overlaps);
        //Debug.Log( overlaps[0].gameObject.name);
    }
}
