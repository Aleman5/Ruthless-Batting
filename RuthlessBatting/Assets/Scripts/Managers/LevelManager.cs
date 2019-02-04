using System.Collections;
using UnityEngine;
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

    bool gameWon = false;
    bool alive = true;

    void Awake()
    {
        if (Instance == this)
        {
            //DontDestroyOnLoad(gameObject);
        }

        //SceneEnum sE = (SceneEnum)System.Enum.Parse(typeof(SceneEnum), SceneManager.GetActiveScene().name);

        //switch (sE)
        //{
        //    case SceneEnum.Menu:
        //        AudioManager.Instance.RunAudio(Audios.menu);
        //        break;
        //    case SceneEnum.StoryboardN1:
        //        AudioManager.Instance.RunAudio(Audios.menu);
        //        break;
        //    case SceneEnum.Game3D:
        //        AudioManager.Instance.RunAudio(Audios.nivel1);
        //        break;
        //    case SceneEnum.Level2:
        //        AudioManager.Instance.RunAudio(Audios.nivel2);
        //        break;
        //}
    }

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
        Time.timeScale = 0f;
        alive = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public void Save()
    {
        foreach (int checkpoint in checkpoints)
            if (checkpoint == waveSpawnerScript.GetActualWaveIndex())
                SaveLoad.Save();
    }

    void Update()
    {
        if (gameWon && InputManager.Instance.GetActionButton())
        {
            Time.timeScale = 1f;
            ChangeLevel2();
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
            SceneLoaderManager.Instance.LoadNextScene(SceneEnum.Game3D);
    }

    void ChangeLevel2()
    {
        SceneLoaderManager.Instance.LoadNextScene(SceneEnum.Game3D);
    }

    IEnumerator GoToMenu()
    {
        //float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(0.05f);//fadeTime;
        SceneLoaderManager.Instance.ReturnMenu();
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
