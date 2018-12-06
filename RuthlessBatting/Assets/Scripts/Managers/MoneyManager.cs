using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    static MoneyManager instance;

    [Header("Multiplier Text")]
    
    [SerializeField] Transform multiplier;
    [SerializeField] Gradient colorGradient;

    [Header("Font")]
    [SerializeField] TMP_FontAsset font; 

    [Header("Scripts")]
    [SerializeField] ShakerController shakerScript;
    [SerializeField] ZoomWhenKilling zoomKillScript;
    [SerializeField] MoneyHolder moneyHolderScript;

    [Header("Variables")]
    [SerializeField] int minMoneyToGive;
    [SerializeField] int maxMoneyToGive;
    [SerializeField] float timeToTurnBack; // Camera zoom in starts 0.3 seconds before time reaches 0


    Transform player;

    float timeLeft = 0.0f;
    float moneyMultiplier = 1.0f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            if(timeLeft < 0.3f && !zoomKillScript.IsGoingBack)
                zoomKillScript.IsGoingBack = true;
        }
        else if (moneyMultiplier > 1.0f)
        {
            moneyMultiplier = 1.0f;
        }
    }

    public void AddToListeners(Health health)
    {
        if (shakerScript)
            health.OnDeath.AddListener(shakerScript.Shake);

        if (zoomKillScript)
            health.OnDeath.AddListener(zoomKillScript.ReduceSize);

        if (moneyHolderScript)
            health.OnDeath.AddListener(AddMoney);
    }

    void AddMoney()
    {
        int moneyToAdd = (int)(Random.Range(minMoneyToGive, maxMoneyToGive) * moneyMultiplier);
        moneyHolderScript.ActualMoney = moneyToAdd;

        Vector3 extraPos = new Vector3(Random.Range(-0.7f, 0.7f), 0.0f, 1.0f);
        Transform go = Instantiate(multiplier, player.position + extraPos, player.rotation);
        TextMeshPro tm = go.GetComponentInChildren<TextMeshPro>();
        tm.font = font;
        tm.fontSize = 7;
        tm.text = "x" + moneyMultiplier;
        tm.color = colorGradient.Evaluate(moneyMultiplier - 1.0f);

        timeLeft = timeToTurnBack;
        moneyMultiplier += 0.2f;
    }

    static public MoneyManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<MoneyManager>();
                if (!instance)
                {
                    GameObject go = new GameObject("Manager");
                    instance = go.AddComponent<MoneyManager>();
                }
            }
            return instance;
        }
    }
}
