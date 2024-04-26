using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterfaceFunctionality : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject play;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject video;
    [SerializeField] new GameObject audio;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject game;


    private void Start()
    {
        canvas.SetActive(true);
        play.SetActive(false);
        settings.SetActive(false);
        video.SetActive(false);
        audio.SetActive(false);
        controls.SetActive(false);
        game.SetActive(false);
    }

    private void Update()
    {
        DoNotDestroy();
        PauseGame();
    }

    private void DoNotDestroy()
    {
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(gameObject);
    }

    //Se puede simplificar de aquí

    public void EnablePlay()
    {
        play.SetActive(true);
        settings.SetActive(false);
        video.SetActive(false);
        audio.SetActive(false);
        controls.SetActive(false);
        game.SetActive(false);
    }

    public void EnableSettings()
    {
        play.SetActive(false);
        settings.SetActive(true);
        video.SetActive(false);
        audio.SetActive(false);
        controls.SetActive(false);
        game.SetActive(false);
    }

    public void EnableSettingsVideo()
    {
        if (settings.activeSelf == true)
        {
            video.SetActive(true);
            audio.SetActive(false);
            controls.SetActive(false);
            game.SetActive(false);
        }
    }

    public void EnableSettingsAudio()
    {
        if (settings.activeSelf == true)
        {
            video.SetActive(false);
            audio.SetActive(true);
            controls.SetActive(false);
            game.SetActive(false);
        }
    }

    public void EnableSettingsControls()
    {
        if (settings.activeSelf == true)
        {
            video.SetActive(false);
            audio.SetActive(false);
            controls.SetActive(true);
            game.SetActive(false);
        }
    }

    public void EnableSettingsGame()
    {
        if (settings.activeSelf == true)
        {
            video.SetActive(false);
            audio.SetActive(false);
            controls.SetActive(false);
            game.SetActive(true);
        }
    }

    // Hasta aquí, en una función???

    public void LoadNewGame()
    {
        canvas.SetActive(false);
        SceneManager.LoadScene("GBX_SceneBlocking");
    }

    //Traducir al new input system si es necesario
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(!canvas.activeSelf);
            if (canvas.activeSelf == true)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }

    public void GameQuit()
    {
        Debug.Log("Has Salido del Juego");
        Application.Quit();
    }
}
