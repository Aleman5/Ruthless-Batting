using UnityEngine;
using UnityEngine.UI;

public class VersionName : MonoBehaviour {

    void Awake()
    {
        GetComponent<Text>().text = "v" + Application.version;
    }
}
