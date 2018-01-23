using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collides : MonoBehaviour {



	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "pipe")
		{
            Debug.Log("Collided with Pipe!");
            transform.position = new Vector3(-36.3f, 8.3f, 0.601f);
        }

        if (col.gameObject.tag == "finish")
        {
            Debug.Log("You win!");
        }
    }


}
