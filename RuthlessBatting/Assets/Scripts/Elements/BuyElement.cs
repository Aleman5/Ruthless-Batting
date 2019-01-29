using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class BuyElement : MonoBehaviour
{
    [SerializeField] Transform objective;
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

        if (File.Exists(Application.persistentDataPath + "/rbSave.bp"))
            levelOfTheElement = SaveLoad.saveGame.data.playerUpgrades[(int)buyable.GetBuyable()];
        else
            levelOfTheElement = 0;

        text = GetComponentInChildren<TextMeshPro>();
        //text.text = buyable.GetBuyable() + " - $" + priceOfTheElement;
        text.text = "" + priceOfTheElement;
        moneyHolder = objective.GetComponent<MoneyHolder>();

        switch (SceneManager.GetActiveScene().name)
        {
            case "Game3D":
                actualLevelScene = 1;
                break;
            case "Level2":
                actualLevelScene = 2;
                break;
        }
    }

    void Update()
    {
        Vector3 dist = objective.position - transform.position;

        if (dist.magnitude < distanceToInteract)
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
                    //enabled = false;
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
        // Talk with Mati if is better incrementing the price based on the original price or based in the actual price.
        // Example 1: 100 + 100 % 10 = 110 + 100 % 10 = 120 % 10 = 130;
        // Example 2: 100 + 100 % 10 = 110 + 110 % 10 = 121 % 10 = 133;

        priceOfTheElement += (int)(priceOfTheElement * extraPercentagePerLevelUp);
        text.text = ""+priceOfTheElement;
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
