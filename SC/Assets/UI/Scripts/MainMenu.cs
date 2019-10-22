using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text HiScore;
    [SerializeField] private Text textMusicOn;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject PanelLevels;
    [SerializeField] private GameObject[] buttons;
    private int maxLevel;
    
     // Start is called before the first frame update
    public void Start ()
    {
        maxLevel = PlayerPrefs.GetInt("LEVEL", 1);
        
        StaticClass.StartScore = 0;
        if (HiScore != null)
        {
            HiScore.text = "Hi score: " + PlayerPrefs.GetInt("HiScore", 0);
        }

        for (int i = maxLevel; i < buttons.Length; i++)
        {
            Image spr = buttons[i].GetComponent<Image>();
            spr.color = Color.grey;
            ;
        }

        MusicOnVolume();
        
    }
    // Start is called before the first frame update
    public void PlayGame (int startLevel)
    {
        if (maxLevel >= startLevel)
        {
            StaticClass.level = startLevel;
            SceneManager.LoadScene("GameScene");
        }
    }
    
    // Start is called before the first frame update
    public void OpenMenuLevels (bool flagOpenMenu)
    {
        PanelLevels.SetActive(flagOpenMenu);
    }
    
    // Start is called before the first frame update
    public void MusicOn ()
    {
        int musicOn = PlayerPrefs.GetInt("MusicOn", 1);
        musicOn = musicOn == 1 ? 0 : 1;
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
            if (textMusicOn != null)
            {
                textMusicOn.text = "Music: on";
            }
        }
        else
        {
            AudioSource.volume = 0f;
            if (textMusicOn != null)
            {
                textMusicOn.text = "Music: off";
            }
        }
    }

    }
