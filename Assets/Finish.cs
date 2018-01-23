using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {


    GameObject player1;
    AudioSource bgm;
    AudioSource fanfare;
	void Start () {
        player1 = GameObject.Find("Rocketship1");
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        fanfare = GameObject.Find("Fanfare").GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            bgm.Stop();
            if(!fanfare.isPlaying)
            {
                fanfare.Play();
            }
        }
    }
}
