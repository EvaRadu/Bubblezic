using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBalls : MonoBehaviour
{
    [SerializeField] private Bubble _circlePrefab;
    float startTime; // time to wait before creating the circle
   

   
    // Start is called before the first frame update
    void Start() {

        startTime = 2;
        StartCoroutine(waitAndCreate(startTime));

        //GameObject sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        //circle.transform.position = new Vector3(-8.09f, -5.91f, 0);
        //sphere1.AddComponent.
        //sphere1.GetComponent<SpriteRenderer>().color = (new Color(100, 100, 100));

        /*GameObject sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere2.transform.position = new Vector3(5.83f, -6.75f, 0);
        sphere2.GetComponent<SpriteRenderer>().color = (new Color(150, 150, 150));

        GameObject sphere3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere3.transform.position = new Vector3(5.83f, -6.75f, 0);
        sphere3.GetComponent<SpriteRenderer>().color=(new Color(200, 200, 200));
        */
    }

    IEnumerator waitAndCreate(float delay) {

        yield return new WaitForSeconds(delay);  // wait 

        var spawnedCircle = Instantiate(_circlePrefab, new Vector3(-8.09f, -5.91f, 0), Quaternion.identity) ; // create a new circle
        spawnedCircle.name = "TestCircle";
        spawnedCircle.setColor(Color.red);
    }


}


