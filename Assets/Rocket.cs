using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    Rigidbody _rigidbody;
    AudioSource rocketAudio;
    GameObject launchpad;
    GameObject targetPad;
    GameObject LeftEngine;
    GameObject RightEngine;
    GameObject rocket;
    AudioSource fanfare;
    [SerializeField] GameObject explode;
    public AudioClip explodeSound;
    Material material;
    public string CurrentLevel;
    public bool DebugMode = false;
    bool LeftThrust = false;
    bool RightThrust = false;
    bool MainThrust = false;
    public float MainThrustSpeed;
    public float RCSThrustSpeed;
    public float WarningDistance;
    public float DistanceToFinish = 0f;
    bool colorChange = false;
    bool alive = true;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
        CurrentLevel = SceneManager.GetActiveScene().name;
        if (CurrentLevel.Equals("M3T15"))
        {
            CurrentLevel = "Level1";
        }
        launchpad = GameObject.Find("Launchpad A");
        targetPad = GameObject.Find("TargetPad");
        LeftEngine = GameObject.Find("LeftEngine");
        RightEngine = GameObject.Find("RightEngine");
        rocket = GameObject.Find("Rocket");
        LeftEngine.GetComponent<EllipsoidParticleEmitter>().emit = false;
        RightEngine.GetComponent<EllipsoidParticleEmitter>().emit = false;
        MainThrustSpeed = 80f;
        RCSThrustSpeed = 80f;
        WarningDistance = 10f;
        DistanceToFinish = Vector3.Distance(transform.position, targetPad.transform.position);
        material = targetPad.GetComponent<Renderer>().material;
    }
	
	void Update () {
        if (alive)
        {
            ProcessInput();
        }
        nearFinish();
	}

    
    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_rigidbody.velocity.magnitude > 10)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * 10;
            }
            _rigidbody.AddRelativeForce(Vector3.up * MainThrustSpeed * Time.deltaTime);
            if(!rocketAudio.isPlaying)
                rocketAudio.Play();
            MainThrust = true;
            if (!LeftThrust)
            {
                LeftEngine.GetComponent<EllipsoidParticleEmitter>().emit = true;
            }
            if(!RightThrust){
                RightEngine.GetComponent<EllipsoidParticleEmitter>().emit = true;
            }
        }
        else
        {
            rocketAudio.Stop();
            MainThrust = false;
            LeftEngine.GetComponent<EllipsoidParticleEmitter>().emit = false;
            RightEngine.GetComponent<EllipsoidParticleEmitter>().emit = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * RCSThrustSpeed * Time.deltaTime);
            LeftThrust = true;
            LeftEngine.GetComponent<EllipsoidParticleEmitter>().emit = false;

        }
        else
        {
            LeftThrust = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * RCSThrustSpeed * Time.deltaTime);
            RightThrust = true;
            RightEngine.GetComponent<EllipsoidParticleEmitter>().emit = false;
        }
        else
        {
            RightThrust = false;
        }
        if(Input.GetKeyUp(KeyCode.O))
        {
            if (DebugMode)
                DebugMode = false;
            else
                DebugMode = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Collided with Finish!");
            _rigidbody.velocity = Vector3.zero;
            transform.Rotate(0f, 0f, 0f);
        }
        if (col.gameObject.tag == "pipe" && !DebugMode)
        {
            Debug.Log("Collided with Pipe!");
            explode = Instantiate(explode, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(explodeSound, transform.position, 1.0f);
            rocket.SetActive(false);
            StartCoroutine(restart());
            rocketAudio.Stop();
            alive = false;
        }

        if (col.gameObject.tag == "finish")
        {
            nextLevel();
        }

    }

    void nearFinish()
    {
        DistanceToFinish = Vector3.Distance(transform.position, targetPad.transform.position);

        if (DistanceToFinish <= WarningDistance && !colorChange)
        {
            Debug.Log("Player1 is near finish");
            material.color = Color.cyan;
            colorChange = true;
        }
        else if (DistanceToFinish > WarningDistance)
        {
            material.color = Color.red;
            colorChange = false;
        }

    }

    void nextLevel()
    {
        if (CurrentLevel.Equals("Level1"))
        {
            SceneManager.LoadScene("Level2");
        }
        else if (CurrentLevel.Equals("Level2"))
        {
            SceneManager.LoadScene("Level3");
        }
        else if (CurrentLevel.Equals("Level3"))
        {
            Debug.Log("You win!");
            Debug.Log("Collided with Finish!");
            material.color = Color.green;
        }
    }
    IEnumerator restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("M3T15");
    }
}
