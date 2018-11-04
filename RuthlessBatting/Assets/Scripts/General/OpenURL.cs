using UnityEngine;

public class OpenURL : MonoBehaviour
{
    [SerializeField] string URL;

    public void OpenTheURL()
    {
        Application.OpenURL(URL);
    }
}
