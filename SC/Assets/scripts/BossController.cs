using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{   
    [SerializeField] private SceneController sceneController;
    private float time;
    public Sprite[] images;
    public int health = 0;
    private float yPosition = 20f;
    
    void Start()
    {
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        float rad = time % (2 * Mathf.PI);
        if (sceneController.bossOn && yPosition >0)
        {
            yPosition -= Time.deltaTime;
            if (health == 0)
            {
                health = images.Length*10;
            }
        }
        Vector3 startVector = new Vector3(8f, yPosition, -1);
        float radius = 5f/Mathf.Max(health,1f);
        transform.position = startVector + new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
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

            player.Ship_Attack(10);
            HitObj(0,true); 
        }
        
    } 
    
    public void HitObj(int hit,bool destroyObj = false)
    {
        GameObject blowUp = sceneController.blowUp;
        blowUp.transform.position = transform.position;
        blowUp.SetActive(true);
        this.gameObject.SetActive(false);
        Invoke("DestroyObject",0.2f);
        
        int count = Random.Range(1, sceneController.level);
        for (int i = 0; i < count; i++)
        {
            float y = Random.Range(0, 2f);
            sceneController.CreateGarbage(new float[3] {transform.position.x, transform.position.y - 1f + y, 2});   
        }
    }
    
    public void DestroyObject()
    {

        sceneController.blowUp.SetActive(false);
        if (health>0)
        {
            health -= 1;
            GetComponent<SpriteRenderer>().sprite = images[Mathf.FloorToInt(health/10)];
            this.gameObject.SetActive(true);
        }
        else
        {
            sceneController.LevelPass();
            Destroy(this.gameObject);    
        }
    }
}
