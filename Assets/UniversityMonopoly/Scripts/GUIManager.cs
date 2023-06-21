using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public CanvasGroup mainMenuCanvas;
    public CanvasGroup settingsCanvas;

    public void ShowSettings()
    {
        HideCanvas(mainMenuCanvas);
        ShowCanvas(settingsCanvas);
    }

    public void ShowMainMenu()
    {
        HideCanvas(settingsCanvas);
        ShowCanvas(mainMenuCanvas);
    }

    void HideCanvas(CanvasGroup canvas)
    {
        canvas.alpha = 0f;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }

    void ShowCanvas(CanvasGroup canvas)
    {
        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }
}
