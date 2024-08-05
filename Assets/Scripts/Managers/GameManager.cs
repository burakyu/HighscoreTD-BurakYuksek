using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : BaseSingleton<GameManager>
{
    [SerializeField] private GameSettings gameSettings;

    public GameSettings GameSettings => gameSettings;
    
    void Start()
    {
#if UNITY_IOS || UNITY_ANDROID
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
#endif
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2)) Time.timeScale = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) Time.timeScale = 3;
    }

    private void OnEnable()
    {
        EventManager.MainTowerLosed.AddListener(GameLosed);
        EventManager.RestartGame.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        EventManager.MainTowerLosed.RemoveListener(GameLosed);
        EventManager.RestartGame.RemoveListener(RestartGame);
    }

    private void GameLosed()
    {
        Time.timeScale = 0;
    }

    private void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
