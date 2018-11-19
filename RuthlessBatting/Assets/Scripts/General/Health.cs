using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float amount;
    [HideInInspector][SerializeField] UnityEvent onDeath;
    [HideInInspector][SerializeField] UnityEvent onHit;

    private void Awake()
    {
        if (!CompareTag("Player"))
        {
            MoneyManager.Instance.AddToListeners(this);

        }
    }

    public float Amount
    {
        get { return amount; }
        set
        {
            float prevHealth = amount;
            amount = value;
            if (amount <= 0)
            {
                amount = 0;
                OnDeath.Invoke();
            }
            else if (prevHealth > amount)
                OnHit.Invoke();
        }
    }

    public UnityEvent OnDeath
    {
        get { return onDeath; }
    }
    public UnityEvent OnHit
    {
        get { return onHit; }
    }
}
