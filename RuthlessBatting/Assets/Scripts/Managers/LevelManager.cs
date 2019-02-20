using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    static LevelManager instance;

    [Header("Scripts")]
    [SerializeField] Health PlayerHealthScript;
    [SerializeField] WaveSpawner waveSpawnerScript;
    [SerializeField] PauseController pauseScript;

    [Header("Canvas")]
    [SerializeField] RectTransform winScreen;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject restartText;

    [Header("Waves Checkpoints")]
    [SerializeField] int[] checkpoints;

    [Header("Scene")]
    [SerializeField] SceneEnum actualScene;

    [HideInInspector] UnityEvent onSaving = new UnityEvent();

    bool gameWon = false;
    bool alive = true;

    void Start()
    {
        waveSpawnerScript.OnLevelComplete.AddListener(IsWin);
        PlayerHealthScript.OnDeath().AddListener(Restart);
        pauseScript.OnPause.AddListener(PauseGame);
        pauseScript.OnResume.AddListener(ContinueGame);
        pauseScript.OnReturn.AddListener(ReturnMenu);
        waveSpawnerScript.OnCountdown.AddListener(Save);
    }

    void IsWin()
    {
        winScreen.gameObject.SetActive(true);
        gameWon = true;

        //Time.timeScale = 0;
    }

    void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ContinueGame()
    {
        if(winScreen.gameObject.activeInHierarchy)
            winScreen.gameObject.SetActive(false);

        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    void ReturnMenu()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(GoToMenu());
    }

    void Restart()
    {
        restartText.SetActive(true);
        Time.timeScale = 0.6f;
        alive = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public void Save()
    {
        foreach (int checkpoint in checkpoints)
            if (checkpoint == waveSpawnerScript.GetActualWaveIndex())
            {
                OnSaving.Invoke();
                SaveLoad.Save();
            }
    }

    void Update()
    {
        if (gameWon && InputManager.Instance.GetActionButton())
        {
            Time.timeScale = 1f;
            GoToNextLevel();
        }
            

        if (!alive) // Player is dead.
        {
            if(InputManager.Instance.GetRestartButton())
            {
                Time.timeScale = 1f;

                UpperFloorObjects.EmptyList();
                SceneLoaderManager.Instance.ReloadScene(SceneManager.GetActiveScene());
            }
            if (InputManager.Instance.GetPauseButton())
            {
                Time.timeScale = 1f;
                ReturnMenu();
            }
                
        }
        else // Player is alive.
        {
            if(InputManager.Instance.GetPauseButton())
                pauseCanvas.SetActive(!pauseCanvas.activeSelf);
        }

        // Para Testing nomás.
        if (Input.GetKey(KeyCode.J))
        {
            SaveLoad.Save();
            SceneLoaderManager.Instance.LoadNextScene(actualScene);
        }
    }

    void GoToNextLevel()
    {
        SaveLoad.Save();
        SceneLoaderManager.Instance.LoadNextScene(actualScene);
    }

    IEnumerator GoToMenu()
    {
        //float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(0.05f);//fadeTime;
        SceneLoaderManager.Instance.ReturnMenu();
    }

    public int[] GetCheckpoints()
    {
        return checkpoints;
    }

    public UnityEvent OnSaving
    {
        get { return onSaving; }
    }

    static public LevelManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<LevelManager>();
                if (!instance)
                {
                    GameObject go = new GameObject("Manager");
                    instance = go.AddComponent<LevelManager>();
                }
            }
            return instance;
        }
    }
}
