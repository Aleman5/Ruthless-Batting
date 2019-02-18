using UnityEngine;
using System.IO;

[System.Serializable]
public class BodiesHolder : MonoBehaviour
{
    static BodiesHolder instance;

    [SerializeField] int maxBodies;
    [SerializeField] Sprite[] enemyTypes;
    [SerializeField] Vector2 minValues;
    [SerializeField] Vector2 maxValues;

    void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/rbSave.bp"))
        {
            int bodies = SaveLoad.saveGame.data.enemyBodies;

            for (int i = 0; i < bodies; i++)
            {
                GameObject obj = new GameObject();

                GameObject o = Instantiate(obj, transform);

                o.transform.position = new Vector3(Random.Range(minValues.x, maxValues.x), 0.0f, Random.Range(minValues.y, maxValues.y));
                o.transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);

                o.AddComponent<SpriteRenderer>();
                SpriteRenderer sr = o.GetComponent<SpriteRenderer>();
                sr.sprite = enemyTypes[Random.Range(0, enemyTypes.Length)];
                sr.sortingLayerName = "BehindWall";
                sr.sortingOrder = 0;
            }

            /*SpriteRenderer[] srs = SaveLoad.saveGame.data.enemyBodies; // Here is the problem.

            if (srs[0] != null)
                for (int i = 0; i < srs.Length; i++)
                {
                    if (srs[i].sortingLayerName != "Default")
                        Instantiate(srs[i], this.transform);
                    else
                        break; // Break because the next information will be always diffrent.
                }*/
        }
    }

    void AddToListener(Health health)
    {
        health.OnDeath().AddListener(CheckLimit);
	}

    void CheckLimit()
    {
        if (transform.childCount > maxBodies)
            Destroy(transform.GetChild(0));
    }

    public int GetBodies()
    {
        return transform.childCount;

        /*SpriteRenderer[] bodies = GetComponentsInChildren<SpriteRenderer>();
        Debug.Log(bodies.Length);
        if (bodies.Length > 0)
            return bodies;
        return null;*/
    }

    public int GetMaxBodies()
    {
        return maxBodies;
    }

    static public BodiesHolder Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<BodiesHolder>();
                if (!instance)
                {
                    GameObject go = new GameObject("EnemyBodiesHolder");
                    instance = go.AddComponent<BodiesHolder>();
                }
            }
            return instance;
        }
    }
}
