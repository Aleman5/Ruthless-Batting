using UnityEngine;

public class GrenadeLauncher : MonoBehaviour, IWeapon
{
    // Hacer que la animacion de la granada dure el tiempo en llegar hasta el punto objetivo (a discusión).

    [SerializeField] GameObject granade;
    [SerializeField] float cooldown;

    float actualTime;
    int upgGrenadeLevel;

    void Start()
    {
        actualTime = cooldown;
    }

    void Update()
    {
        if (InputManager.Instance.GetGranadeButton() && actualTime <= 0.0f)
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
        Vector3 diff = dest - transform.position;
        diff.y = 0;

        gr.SetData(diff.normalized, dest);
    }

    public void SetStats(int level)
    {
        upgGrenadeLevel = level;

        cooldown -= cooldown * 0.05f * upgGrenadeLevel;
    }

    public int GetUpgradeValue() { return upgGrenadeLevel; }
}
