using UnityEngine;

public class MultiplierTextSettings : MonoBehaviour
{
    [SerializeField] float speed;

    const string sortingOrderName = "Wall ChangeUp";
    const int sortingOrder = 2;

    void Awake()
    {
        MeshRenderer mR = GetComponent<MeshRenderer>();
        mR.sortingLayerName = sortingOrderName;
        mR.sortingOrder = sortingOrder;
    }

    void Update()
    {
        Vector3 mov = new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
        transform.Translate(mov);
    }
}
