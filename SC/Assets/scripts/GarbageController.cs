using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageController : MonoBehaviour
{

    public float Speed = 0f;
    public float startSpeed = 0.1f;
    public float rotationSpeed = 2f;
    
    [SerializeField] private SceneController SceneController;
    private int _id;
    
    void Start()
    {
       // Destroy(gameObject,1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x-Speed,transform.position.y);
        transform.Rotate(0,0,rotationSpeed);
    }
    
    public void SetRandom(float startSpeed,  Sprite image)
    {
        this.startSpeed = startSpeed;
        GetComponent<SpriteRenderer>().sprite = image;
        RandomRotationSpeed();
        RandomSpeed();
        int destroyTimer = Mathf.RoundToInt( 0.5f/Speed);
        //Debug.Log(startSpeed);
      //  Debug.Log(destroyTimer);
        SetDestroy(destroyTimer);
    }
    
    public void SetDestroy(int destroyTimer)
    {
 
        Destroy(gameObject,destroyTimer);
    }
    
    
    public void RandomRotationSpeed()
    {
 
        rotationSpeed = Random.Range(0, rotationSpeed);
    }
    public void RandomSpeed()
    {
        Speed = Random.Range(startSpeed/2, startSpeed*2);
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        Player player = other.GetComponent<Player>();
        if (player != null)
        {

            if (PlayerPrefs.GetInt("HiScore") < ScoreScript.scoreValue )
            {
                PlayerPrefs.SetInt("HiScore", ScoreScript.scoreValue );
            }

            player.Ship_Attack();
            DestroyObj(); 
        }
        
        EndOffArea Scene_Area = other.GetComponent<EndOffArea>();
        if (Scene_Area != null)
        {
            DestroyObject(); 
        }

    } 
    
    public void DestroyObj()
    {
        GameObject blowUp = SceneController.GetComponent<SceneController>().blowUp;
        blowUp.transform.position = transform.position;
        blowUp.SetActive(true);
        this.gameObject.SetActive(false);
        Invoke("DestroyObject",0.5f);
    }
    public void DestroyObject()
    {
        SceneController.GetComponent<SceneController>().blowUp.SetActive(false);
        Destroy(this.gameObject); 
    }
    
}
