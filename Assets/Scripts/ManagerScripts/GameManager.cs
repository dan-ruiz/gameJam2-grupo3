using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;

    // Audio
    public AudioClip gameMusic;

    // Variables para acceder a las imagenes/botones
    public GameObject inactiveSound;
    public GameObject activeSound;


    void Start()
    {
        PlayMusic();
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Funciones al dar click en botones

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void BackHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Reset()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameActive = true;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        isGameActive = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        isGameActive = true;
    }


    // Funcioens para Audio 

    public void PauseMusic()
    {
        // Pausar musica de fondo
        AudioManager.Instance.StopMusic();
        inactiveSound.SetActive(false);
        activeSound.SetActive(true);
    }

    public void PlayMusic()
    {
        // Reproducir musica de fondo
        AudioManager.Instance.PlayMusic(gameMusic);
        activeSound.SetActive(false);
        inactiveSound.SetActive(true);
    }
}
