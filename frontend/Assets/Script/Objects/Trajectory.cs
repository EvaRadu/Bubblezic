using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] public int _id;
    public Bubble _bubble;
    public void SetId(int id) => _id = id;

    public void SetBubble(Bubble bubble) => _bubble = bubble;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
