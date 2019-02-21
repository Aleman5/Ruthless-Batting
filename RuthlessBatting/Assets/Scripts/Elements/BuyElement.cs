using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class BuyElement : MonoBehaviour
{
    [SerializeField] Transform objective;
    [SerializeField] LayerMask obstacules;
    [SerializeField] float distanceToInteract;
    [Range(0,500)]
    [SerializeField] int priceOfTheElement;
    [Range(0,1)]
    [SerializeField] float extraPercentagePerLevelUp;

    [HideInInspector][SerializeField] UnityEvent onInteract;
    [HideInInspector][SerializeField] UnityEvent onNotEnoughMoney;
    [HideInInspector][SerializeField] UnityEvent onRange;
    [HideInInspector][SerializeField] UnityEvent onQuit;

    IBuyable buyable;
    TextMeshPro text;
    MoneyHolder moneyHolder;

    int levelOfTheElement;
    int actualLevelScene;

    bool isOnRange = false;

    void Awake()
    {
        buyable = GetComponent<IBuyable>();
        text = GetComponentInChildren<TextMeshPro>();
        moneyHolder = objective.GetComponent<MoneyHolder>();

        if (File.Exists(Application.persistentDataPath + "/rbSave.bp"))
        {
            levelOfTheElement = SaveLoad.saveGame.data.playerUpgrades[(int)buyable.GetBuyable()];
            for (int i = 0; i < levelOfTheElement; i++)
                LevelUpThePrice();
        }
        else
            levelOfTheElement = 0;

        text.text = "" + priceOfTheElement;

        switch((SceneEnum)System.Enum.Parse(typeof(SceneEnum), SceneManager.GetActiveScene().name))
        {
            case SceneEnum.Level1:
                actualLevelScene = 1;
                break;
            case SceneEnum.Level2:
                actualLevelScene = 2;
                break;
            case SceneEnum.Level3:
                actualLevelScene = 3;
                break;
        }
    }

    void Update()
    {
        Vector3 dist;

        if (objective)
            dist = objective.position - transform.position;
        else
            dist = new Vector3(distanceToInteract, distanceToInteract);

        if (dist.magnitude < distanceToInteract && Physics.Raycast(transform.position, dist, dist.magnitude, obstacules))
        {
            if (!isOnRange)
            {
                isOnRange = true;
                onRange.Invoke();
            }

            if (InputManager.Instance.GetInteractButton())
            {
                if(levelOfTheElement < 3  * actualLevelScene && moneyHolder.ActualMoney >= priceOfTheElement)
                {
                    buyable.Buy(objective, ++levelOfTheElement);
                    moneyHolder.ActualMoney = -priceOfTheElement;
                    LevelUpThePrice();
                    AudioManager.Instance.RunAudio(Audios.habilidad_comprada);
                    onInteract.Invoke();
                }
                else
                {
                    AudioManager.Instance.RunAudio(Audios.habilidad_cancelada);
                    OnNotEnoughMoney.Invoke();
                }
            }
        }
        else if (isOnRange)
        {
            isOnRange = false;
            onQuit.Invoke();
        }
    }

    void LevelUpThePrice()
    {
        priceOfTheElement += (int)(priceOfTheElement * extraPercentagePerLevelUp);

        if (levelOfTheElement < 3 * actualLevelScene)
            text.text = "" + priceOfTheElement;
        else
            text.text = "";
    }

    public bool IsOnRange()
    {
        return isOnRange;
    }

    public int GetLevel()
    {
        return levelOfTheElement;
    }

    public UnityEvent OnInteract
    {
        get { return onInteract; }
    }
    public UnityEvent OnNotEnoughMoney
    {
        get { return onNotEnoughMoney; }
    }
    public UnityEvent OnRange
    {
        get { return onRange; }
    }
    public UnityEvent OnQuit
    {
        get { return onQuit; }
    }
}
