using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PostFX;

public class Settings : MonoBehaviour
{
    public Sprite[] checkboxSprites;
    public TiltShift tiltShift;
    public Image blurCheckbox;

    public void OnBlurSwitch()
    {
        tiltShift.enabled = !tiltShift.enabled;

        if (tiltShift.enabled)
        {
            blurCheckbox.sprite = checkboxSprites[0];
        } 
        else
        {
            blurCheckbox.sprite = checkboxSprites[1];
        }
    }
}
