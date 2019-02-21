using UnityEngine;

public class GrenadeLauncher : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject granade;
    [SerializeField] float cooldown;

    float origCd;
    float actualTime;
    int upgGrenadeLevel;

    void Start()
    {
        origCd = actualTime = cooldown;
    }

    void Update()
    {
        if (InputManager.Instance.GetGranadeButton() && actualTime < 0.0f)
        {
            actualTime = cooldown;
            InstantiateGranade();
        }

        actualTime -= Time.deltaTime;
    }

    void InstantiateGranade()
    {
        GameObject g = Instantiate(granade, transform.position, transform.rotation);

        Grenade gr = g.GetComponent<Grenade>();

        Vector3 dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dest.y = transform.position.y;
        Vector3 diff = dest - transform.position;
        diff.y = 0;

        gr.SetData(diff.normalized, dest);
    }

    public void SetStats(int level)
    {
        upgGrenadeLevel = level;

        cooldown = origCd - 0.1f * upgGrenadeLevel;
    }

    public int GetUpgradeValue() { return upgGrenadeLevel; }
}
