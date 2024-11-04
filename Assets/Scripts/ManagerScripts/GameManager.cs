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
    [SerializeField] private GameObject gameOverMenu;
    public bool isGameActive;
    public bool isPaused;

    // Audio
    public AudioClip gameMusic;
    public AudioClip clickButton;

    // Variables para acceder a las imagenes/botones
    public GameObject inactiveSound;
    public GameObject activeSound;


    void Start()
    {
        PlayMusic(false);
        isGameActive = true;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        PauseWithP();
    }

    // Funciones al dar click en botones

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        isGameActive = false;
    }

    public void BackHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        AudioManager.Instance.PlaySFX(clickButton);
    }

    public void Reset()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameActive = true;
        AudioManager.Instance.PlaySFX(clickButton);
        isPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        isGameActive = false;
        AudioManager.Instance.PlaySFX(clickButton);
        isPaused = true;
    }

    public void PauseWithP()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        isGameActive = true;
        AudioManager.Instance.PlaySFX(clickButton);
        isPaused = false;
    }


    // Funciones para Audio 

    public void PauseMusic()
    {
        // Pausar musica de fondo
        AudioManager.Instance.StopMusic();
        // Sonido de efecto al presionar boton
        AudioManager.Instance.PlaySFX(clickButton);

        inactiveSound.SetActive(false);
        activeSound.SetActive(true);
    }

    public void PlayMusic()
    {
        // Reproducir musica de fondo
        AudioManager.Instance.PlayMusic(gameMusic);
        // Sonido de efecto al presionar boton
        AudioManager.Instance.PlaySFX(clickButton);

        activeSound.SetActive(false);
        inactiveSound.SetActive(true);
    }

    // Este metodo se creo solo para evitar el sonido de Click al ejecutar el juego
    public void PlayMusic(bool playClickSound = true)
    {
        // Reproducir musica de fondo
        AudioManager.Instance.PlayMusic(gameMusic);

        // Sonido de efecto al presionar boton, solo si playClickSound es true
        if (playClickSound)
        {
            AudioManager.Instance.PlaySFX(clickButton);
        }

        activeSound.SetActive(false);
        inactiveSound.SetActive(true);
    }
}
