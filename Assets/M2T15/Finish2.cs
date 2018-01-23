using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish2 : MonoBehaviour {

    GameObject player1;
    GameObject player2;
    bool colorChange1 = false;
    bool colorChange2 = false;

    Material material;
	void Start () {
        material = GetComponent<Renderer>().material;
        player1 = GameObject.Find("Rocketship1");
        player2 = GameObject.Find("Rocketship2");
    }
    void Update()
    {
        nearFinish();
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Collided with Finish!");
            material.color = Color.green;
        }
    }

    void nearFinish()
    {

        if (Vector3.Distance(transform.position, player1.transform.position) <= 15f && !colorChange1)
        {
            Debug.Log("Player1 is near finish");
            material.color = Color.blue;
            colorChange1 = true;
        }
        else if (Vector3.Distance(transform.position, player2.transform.position) <= 15f && !colorChange2)
        {
            Debug.Log("Player2 is near finish");
            material.color = Color.yellow;
            colorChange2 = true;
        }
        else if(Vector3.Distance(transform.position, player2.transform.position) > 15f && Vector3.Distance(transform.position, player1.transform.position) > 20f)
        {
            material.color = Color.red;
            colorChange1 = false;
            colorChange1 = false;
        }

    }
}
