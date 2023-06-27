using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI diceText;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI firstButtonText;
    public TextMeshProUGUI secondButtonText;

    public Animator playerspanelAnimator;
    public Animator buttonsAnimator;

    public void ShowPlayersPanel()
    {
        playerspanelAnimator.Play("PlayersPanelShow");
    }

    public void ShowButtons()
    {
        buttonsAnimator.Play("ActionButtonsShow");
    }

    public void HideButtons()
    {
        buttonsAnimator.Play("ActionButtonsHide");
    }

    public void DisableFirstButton()
    {
        firstButtonText.gameObject.transform.parent.GetComponent<Button>().interactable = false;
    }

    public void DisableSecondButton()
    {
        secondButtonText.gameObject.transform.parent.GetComponent<Button>().interactable = false;
    }

    public void EnableFirstButton()
    {
        firstButtonText.gameObject.transform.parent.GetComponent<Button>().interactable = true;
    }

    public void EnableSecondButton()
    {
        secondButtonText.gameObject.transform.parent.GetComponent<Button>().interactable = true;
    }

    public void SetDiceText(string newText)
    {
        diceText.text = newText;
    }

    public void SetMainText(string newText)
    {
        mainText.text = newText;
    }

    public void SetFirstButtonText(string newText)
    {
        firstButtonText.text = newText;
    }

    public void SetSecondButtonText(string newText)
    {
        secondButtonText.text = newText;
    }
}
