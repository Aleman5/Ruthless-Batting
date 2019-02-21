using UnityEngine;
using UnityEngine.Events;

public class Bat : MonoBehaviour, IWeapon
{
	enum Direction
	{
		UP,
		DOWN,
		RIGHT,
		LEFT
	}

    [SerializeField] PlayerMovement3D playerMovement;
    [SerializeField] BoxCollider batBoxCollider;
    [SerializeField] float horizontalAttackRange;
    [HideInInspector][SerializeField] UnityEvent onAttack;

    float cooldown;
    float attackRate;
    float origAttackRate;
    int upgBatLevel;

    float timeToDisappearHitBox;
    int direction;

    void Awake()
    {
        cooldown = 0.0f;
        origAttackRate = attackRate = 0.255f;

        timeToDisappearHitBox = 0.18f;

        batBoxCollider.enabled = false;
    }

    void Update()
    {
        if (InputManager.Instance.GetFireButton() && Time.time > cooldown)
        {
            Attack();
        }
    }

    void Attack()
    {
        cooldown = Time.time + attackRate;

        //playerMovement.enabled = false;

        batBoxCollider.enabled = true;

        ActualDirection = Utilities.GetDirection(transform, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        //Utilities.SetBoxPreparations(transform, ActualDirection);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mousePosition.y = 0;
        transform.rotation = Quaternion.LookRotation(mousePosition, Vector3.up);

        AudioManager.Instance.RunAudio(Audios.bath_al_aire);

        onAttack.Invoke();

        Invoke("DesactivateBox", timeToDisappearHitBox);
    }

	void DesactivateBox()
    {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        playerMovement.enabled = true;

        batBoxCollider.enabled = false;
    }

    public void SetStats(int level)
    {
        upgBatLevel = level;

        attackRate = origAttackRate - 0.015f * upgBatLevel;
    }

    public int GetUpgradeValue() { return upgBatLevel; }

    public UnityEvent OnAttack
    {
        get { return onAttack; }
    }

    public int ActualDirection
    {
        get { return direction; }
        set { direction = value; }
    }
}