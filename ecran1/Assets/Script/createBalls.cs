using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBalls : MonoBehaviour
{
    [SerializeField] private Bubble _circlePrefab;
    float startTime; // time to wait before creating the circle
    List<Bubble> bubbles = new List<Bubble>();
   

    void Start() {

        startTime = 2f;
        StartCoroutine(waitAndCreate(startTime));

    }

    IEnumerator waitAndCreate(float delay) {

        yield return new WaitForSeconds(delay);  // wait 

        var spawnedCircle = Instantiate(_circlePrefab, new Vector3(-8.09f, -5.91f, 0), Quaternion.identity) ; // create a new circle
        spawnedCircle.name = "TestCircle";
        spawnedCircle.setColor(Color.red);
        bubbles.Add(spawnedCircle);
    }


}


