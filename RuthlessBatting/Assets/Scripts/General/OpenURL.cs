using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    [SerializeField] string URL;

    public void OpenTheURL()
    {
        Application.OpenURL(URL);
    }
}
