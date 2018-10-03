using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInput{
    float GetVerticalAxis();
    float GetHorizontalAxis();
    bool GetFireButton();
    bool GetDashButton();
    bool GetInteractButton();
    bool GetActionButton();
    bool GetPauseButton();
}
