using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private GameObject Boss;
    [SerializeField] private Camera camera;
    protected bool gameOverOn = false;
    protected bool LevelOn = false;
    public bool bossOn = false;
    public float bossTimerOn = 120f;
    protected float bossTimer = 0f;
  
    public GameObject blowUp;
    [SerializeField] private GarbageController originalGarabage;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject textLevel;
    [SerializeField] public Sprite[] imagesLP;
    [SerializeField] private Sprite[] imagesGarbage1;
    [SerializeField] private Sprite[] imagesGarbage2;
    [SerializeField] private Sprite[] imagesBoss1;
    [SerializeField] private Sprite[] imagesBoss2;
    [SerializeField] private Sprite[] imagesBoss3;
    
    private List<GarbageController> garbage = new List<GarbageController>();
    public int level;
    
    public int minMaxX_For_Garbage = 4;
    
    private static SceneController instance;
    public static int scoreValue = 0;
    
    //score
    [SerializeField] private GameObject score;
    Text scoretext;
 
    private SceneController()
    {}
 
    public static SceneController getInstance()
    {
        if (instance == null)
            instance = new SceneController();
        return instance;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        level = Mathf.Max(StaticClass.level, 1);
        scoreValue = StaticClass.StartScore;

        // text level
        TextMesh leveltext = textLevel.GetComponent<TextMesh>() as TextMesh;
        leveltext.text = "LEVEL " + level;
        Invoke("DisactiveText",2f);
        
        scoretext  = score.GetComponent<Text>() as Text;
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
      
        garbage = garbage.Where(c => c != null).ToList();
        if (LevelOn && !gameOverOn && (garbage == null || garbage.Count < level))
        {
            float[] parms = new float[0];
            StartCoroutine("CreateGarbage",parms);
        }

        bossTimer += Time.deltaTime;
        
        if (bossTimer > bossTimerOn && !bossOn)
        {
            bossOn = true;
            BossController controller = Boss.GetComponent<BossController>();
            SpriteRenderer spriteRenderer = Boss.GetComponent<SpriteRenderer>();
            
            TextMesh leveltext = textLevel.GetComponent<TextMesh>() as TextMesh;
            
            if (level == 1 )
            {
                leveltext.text = "Dangerous asteroid \r\n coming to Earth.";
                controller.images = imagesBoss3;
            } else if ( level == 2)
            {
                leveltext.text = "Next asteroid \r\n are coming";
                controller.images = imagesBoss3;
                    
            } else  if (level == 3)
            {
                leveltext.text = "Another one.";
                controller.images = imagesBoss2;
            } else  if (level == 4)
            {
                leveltext.text = "The last one.";
                controller.images = imagesBoss1;
            } 
            textLevel.SetActive(true);
            Invoke("DisactiveText",5f);
                
            spriteRenderer.sprite = controller.images[0];
            Boss.SetActive(true);
            
          
            
        }

    }
    
    
    public void CreateGarbage(float[] parms)
    {
 
        GarbageController garbageGen;
        garbageGen = Instantiate(originalGarabage) as GarbageController;
        

        float positionY = Random.Range(-minMaxX_For_Garbage, minMaxX_For_Garbage);
        float positionX = camera.aspect * camera.orthographicSize;
        
        int typeGarbage = 1;
        if (level > 1)
        {
            typeGarbage = 2;
        }

        if ( parms.Length > 1 )
        {
            positionY = parms[1];
        }

        if ( parms.Length > 0 )
        {
            positionX  = parms[0];
        }
        if ( parms.Length > 2 )
        {
            typeGarbage = Mathf.FloorToInt(parms[2]);
        }

        garbageGen.transform.position = new Vector3((float)positionX, (float)positionY, 0); 
        int id;
            
        float startSpeed = 0.1f + scoreValue * 0.0005f;
        if ( typeGarbage == 0)
        {
            id = Random.Range(0, imagesLP.Length);
            garbageGen.SetRandom(startSpeed, imagesLP[id]);
        }
        else if ( typeGarbage == 1)
        {
            id = Random.Range(0, imagesGarbage1.Length);
            garbageGen.SetRandom(startSpeed, imagesGarbage1[id]);
        }
        else if ( typeGarbage == 2)
        {
            id = Random.Range(0, imagesGarbage2.Length);
            garbageGen.SetRandom(startSpeed, imagesGarbage2[id],true);
        }

        garbage.Add(garbageGen);
    }

    
    public void GameOvertTrue()
    {
        gameOverOn = true;
        gameOver.SetActive(true);
    }
    
    public void UpdateScore(int sc)
    {
        scoreValue = scoreValue + sc;
        scoretext.text = "Score: " + scoreValue;
        if (PlayerPrefs.GetInt("HiScore") < scoreValue)
        {
            PlayerPrefs.SetInt("HiScore", scoreValue);
        }
    }
    
        
    private void DisactiveText()
    {
        LevelOn = true;
        textLevel.SetActive(false);
    }
    
    public void LevelPass()
    {
        LevelOn = false;
        
        TextMesh leveltext = textLevel.GetComponent<TextMesh>() as TextMesh;
        leveltext.text = "LEVEL COMPLeTE!!!";
        textLevel.SetActive(true);
        Invoke("NextLevel",1f);
    }
    
    private void NextLevel()
    {
        if (level == 4)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            StaticClass.level = level + 1;
            int maxLevel = PlayerPrefs.GetInt("LEVEL", 1);
            if (maxLevel  < StaticClass.level)
            {
                PlayerPrefs.SetInt("LEVEL", StaticClass.level);
            }
            StaticClass.StartScore = scoreValue;
            SceneManager.LoadScene("GameScene");
        }
    }
}
