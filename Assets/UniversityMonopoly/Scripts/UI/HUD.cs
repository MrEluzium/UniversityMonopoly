using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI diceText;
    public TextMeshProUGUI mainText;
    public Animator playerspanelAnimator;

    public void ShowPlayersPanel()
    {
        playerspanelAnimator.Play("PlayersPanelShow");
    }

    public void SetDiceText(string newText)
    {
        diceText.text = newText;
    }

    public void SetMainText(string newText)
    {
        mainText.text = newText;
    }
}
