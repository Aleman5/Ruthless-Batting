using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
    static InputManager instance;
    IInput input;

    void Awake()
    {
        if (Instance == this)
        {
            //DontDestroyOnLoad(gameObject);

            input = new InputPC();
        }
    }

    public float GetVerticalAxis()   { return input.GetVerticalAxis();   }
    public float GetHorizontalAxis() { return input.GetHorizontalAxis(); }
    public bool GetFireButton()      { return input.GetFireButton();     }
    public bool GetDashButton()      { return input.GetDashButton();     }
    public bool GetInteractButton()  { return input.GetInteractButton(); }
    public bool GetPauseButton()     { return input.GetPauseButton();    }
    public bool GetActionButton()    { return input.GetActionButton();   }
    public bool GetRestartButton()   { return input.GetRestartButton();  }

    static public InputManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<InputManager>();
                if (!instance)
                {
                    GameObject go = new GameObject("Manager");
                    instance = go.AddComponent<InputManager>();
                }
            }
            return instance;
        }
    }
}
