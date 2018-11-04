using UnityEngine;
using UnityEngine.UI;

public class VersionName : MonoBehaviour {

    void Awake()
    {
        GetComponent<Text>().text = "ALPHA VERSION " + Application.version;
    }
}
