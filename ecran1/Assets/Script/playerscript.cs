using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       //implémentation du code ici 
    }

    // Update is called once per frame
    void Update()
    {
        //parfait pour tout ajuster
        
    }
    void FixeUpdate(){
        // gestion améllioré de la physique
        //pour le puzel

        transform.Translate(Vector3.forward * 5f* Time.fixedDeltaTime * Input.GetAxis("Vertical"));

    }
}
