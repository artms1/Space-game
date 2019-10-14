using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text HiScore;
    public Text textMusicOn;
    [SerializeField] private Camera camera;
    
     // Start is called before the first frame update
    public void Start ()
    {
        HiScore.text = "Hi score: " + PlayerPrefs.GetInt("HiScore",  0);
        MusicOnVolume();
    }
    // Start is called before the first frame update
    public void PlayGame ()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    // Start is called before the first frame update
    public void MusicOn ()
    {
        int musicOn = PlayerPrefs.GetInt("MusicOn", 1);
        if (musicOn == 1)
        {
            musicOn = 0;
        }
        else
        {
            musicOn = 1;
        }
        PlayerPrefs.SetInt("MusicOn", musicOn);
        MusicOnVolume();
    }

    // Update is called once per frame
    public void QuitGame ()
    {
        Application.Quit();
    }
    
    public void LoadMMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void MusicOnVolume ()
    {
        int musicOn = PlayerPrefs.GetInt("MusicOn", 1);
        var AudioSource = camera.GetComponent<AudioSource>() as AudioSource;
        if (musicOn == 1)
        {
            AudioSource.volume = 1f;
            textMusicOn.text = "Music: on";
        }
        else
        {
            AudioSource.volume = 0f;
            textMusicOn.text = "Music: off";
        }
    }
    }
