using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageController : MonoBehaviour
{

    public float speedX = 0f;
    public float speedY = 0f;
    public float startSpeed = 0.1f;
    public float rotationSpeed = 2f;
    private bool generateLP;
    
    private int _id;
    [SerializeField] private SceneController sceneController;
    
    void Start()
    {
   }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x-speedX,transform.position.y+speedY);
        transform.Rotate(0,0,rotationSpeed);
    }
    
    public void SetRandom(float startSpeed,  Sprite image, bool generateLP =false)
    {
        this.generateLP = generateLP;
        this.startSpeed = startSpeed;
        GetComponent<SpriteRenderer>().sprite = image;
        RandomRotationSpeed();
        RandomSpeed();
        int destroyTimer = Mathf.RoundToInt( 1f/speedX);
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
        speedX = Random.Range(startSpeed/2, startSpeed);
        speedY = Random.Range(0, 0.001f/speedX);
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        Player player = other.GetComponent<Player>();
        if (player != null)
        {

            if (PlayerPrefs.GetInt("HiScore") < SceneController.scoreValue )
            {
                PlayerPrefs.SetInt("HiScore", SceneController.scoreValue );
            }

            player.Ship_Attack();
            HitObj(0,true); 
        }
        
        EndOffArea Scene_Area = other.GetComponent<EndOffArea>();
        if (Scene_Area != null)
        {
            Destroy(gameObject);
        }

    } 
    
    public void HitObj(int hit,bool destroyObj = false)
    {
        GameObject blowUp = sceneController.blowUp;
        blowUp.transform.position = transform.position;
        blowUp.SetActive(true);
        this.gameObject.SetActive(false);
        Invoke("DestroyObject",0.2f);
    }
    
    public void DestroyObject()
    {

       sceneController.blowUp.SetActive(false);
       if (generateLP)
       {
            GetComponent<SpriteRenderer>().sprite = sceneController.imagesLP[0];
            this.gameObject.SetActive(true);
            generateLP = false;
       }
       else
       {
           Destroy(this.gameObject);    
       }
    }
    
}
