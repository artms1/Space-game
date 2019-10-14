using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private Camera camera;
    protected bool gameOverOn = false;
  
    public GameObject blowUp;
    [SerializeField] private GarbageController originalCard;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private Sprite[] images;
    GarbageController garbage;
    
    public int minMaxX_For_Garbage = 4;
    

    // Start is called before the first frame update
    void Start()
    {
        ScoreScript.scoreValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (garbage == null && !gameOverOn)
        {
            garbage = Instantiate(originalCard) as GarbageController;
            float positionY = Random.Range(-minMaxX_For_Garbage, minMaxX_For_Garbage);
            
            var boxCollider = camera.GetComponent<BoxCollider2D>() as BoxCollider2D;
 
            float width = camera.aspect * camera.orthographicSize;
           // boxCollider.size.y = 2f * camera.orthographicSize;
            
            garbage.transform.position = new Vector3(width, positionY, 0); 
            int id = Random.Range(0, images.Length);
            
            float startSpeed = 0.1f + ScoreScript.scoreValue * 0.0005f;
            garbage.SetRandom(startSpeed, images[id]);
        }
    }
    
    public void GameOvertTrue()
    {
        gameOverOn = true;
        GameOver.SetActive(true);
    }
    
 
}
