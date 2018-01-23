using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collides2 : MonoBehaviour {



	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "pipe")
		{
            Debug.Log("Collided with Pipe!");
            transform.position = new Vector3(-36.3f, 26.6f, 0.601f);
        }

        if (col.gameObject.tag == "finish")
        {
            Debug.Log("You win!");
        }
    }


}
