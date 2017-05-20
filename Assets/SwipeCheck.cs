using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCheck : MonoBehaviour
{
    PanelWindowManager PWM;
    public TouchGesture.GestureSettings GestureSetting;
    private TouchGesture touch;
    void Start()
    {
        PWM = GetComponent<PanelWindowManager>();
        touch = new TouchGesture(this.GestureSetting);
        StartCoroutine(touch.CheckHorizontalSwipes(
            onLeftSwipe: () => { PWM.SwitchScreen(true); },
            onRightSwipe: () => { PWM.SwitchScreen(false); }
            ));
    }
}
