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
        return Input.GetButton("Fire1");
    }
    public bool GetInteractButton()
    {
        return Input.GetButtonDown("Interact");
    }
    public bool GetNextWaveButton()
    {
        return Input.GetButtonDown("NextWave");
    }
    public bool GetPauseButton()
    {
        return Input.GetButtonDown("Cancel");
    }
}
