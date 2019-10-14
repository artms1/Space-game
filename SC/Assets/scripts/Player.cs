using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player: MonoBehaviour
{
 
    public float speed = 6f;
    public float shildOnTimeForTimer = 6f;
    protected bool shildOn = true;

    private Rigidbody2D rb;
    [SerializeField] private GameObject[] particals;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject Shield;
    [SerializeField] private SceneController SceneController;
    
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");
        bool fireOn = CrossPlatformInputManager.GetButton("Fire");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        rb.velocity = movement * speed;

        List<bool> visibl = new List<bool>();
        for (int i = 0; i < particals.Length; i++)
        {
            visibl.Add(false);
        }

        if (moveHorizontal > 0)
        {
            visibl[0] = true;
        }

        if (moveHorizontal < 0)
        {
            visibl[1] = true;
        }

        if (moveVertical > 0)
        {
            visibl[3] = true;
        }

        if (moveVertical < 0)
        {
            visibl[2] = true;
        }

        for (int i = 0; i < particals.Length; i++)
        {
            particals[i].SetActive(visibl[i]);
        }

        if (fireOn)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position+new Vector3(2f,0,0), Vector2.right);
            // If it hits something...
            if (hit.collider != null)
            {
                GarbageController garbage = hit.collider.gameObject.GetComponent<GarbageController>();
                if (garbage != null)
                {
                    garbage.DestroyObj();
                    ScoreScript.scoreValue += 1;
                }
            }
        }

        this.fire.SetActive(fireOn);
    }

    public void Ship_Attack ()
    {
        if (shildOn)
        {
            shildOn = false;
            Shield.SetActive(false);
            Invoke("Shield_On",shildOnTimeForTimer);
        }
        else
        {
            SceneController.blowUp.transform.position = transform.position;
            SceneController.blowUp.SetActive(true);
            this.gameObject.SetActive(false);
            Invoke("DistroyOff",0.5f);
        }
    }
    
    void Shield_On ()
    {
        shildOn = true;
        Shield.SetActive(shildOn);
    }

    void DistroyOff ()
    {
        SceneController.blowUp.SetActive(false);
        SceneController.GameOvertTrue();
        Destroy(this.gameObject);
    }
    
}
