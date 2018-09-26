using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInput{
    float GetVerticalAxis();
    float GetHorizontalAxis();
    bool GetFireButton();
    bool GetInteractButton();
    bool GetNextWaveButton();
    bool GetPauseButton();
}
