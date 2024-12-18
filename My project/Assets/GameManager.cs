using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public bool isGamePaused = false;

    public event System.Action<bool> OnPauseStateChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            // call attached class that bool changed
            OnPauseStateChanged?.Invoke(isGamePaused);

            Time.timeScale = isGamePaused ? 0f : 1f;
        }
    }

}
