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

   
}
