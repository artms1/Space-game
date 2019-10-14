using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    public SpriteRenderer rinc;
    public float KoeficientScreenorthographicSize = 0.1f;
    float m_FieldOfView;
    
    void Start()
    {
       // Camera.main.orthographicSize = Screen.height*KoeficientScreenorthographicSize;
        m_FieldOfView = 10.0f;
    }

    void Update()
    {
        //Update the camera's field of view to be the variable returning from the Slider
        Camera.main.fieldOfView = m_FieldOfView;
    }

 
}
