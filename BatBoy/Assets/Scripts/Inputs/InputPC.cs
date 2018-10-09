using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPC : IInput
{
    public float GetVerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }
    public float GetHorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }
    public bool GetFireButton()
    {
        return Input.GetButtonDown("Fire1");
    }
    public bool GetDashButton()
    {
        return Input.GetButtonDown("Dash");
    }
    public bool GetInteractButton()
    {
        return Input.GetButtonDown("Interact");
    }
    public bool GetActionButton()
    {
        return Input.GetButtonDown("Action");
    }
    public bool GetPauseButton()
    {
        return Input.GetButtonDown("Cancel");
    }
    public bool GetRestartButton()
    {
        return Input.GetButtonDown("Restart");
    }
}
