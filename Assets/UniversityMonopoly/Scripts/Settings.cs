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

        switch (tiltShift.enabled)
        {
            case true:
                blurCheckbox.sprite = checkboxSprites[0];
                break;
            case false:
                blurCheckbox.sprite = checkboxSprites[1];
                break;
        }
    }
}
