using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animation animations;
    GameManager gameManager;

    void Start()
    {
        animations = GetComponent<Animation>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnPlayButtonPressed()
    {
        PlayAnimation("MainMenuFadeOutAnimation");
        gameManager.StartGame();
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    private void PlayAnimation(string animationName) {    
        foreach(AnimationState state in animations) {
            if (state.name == animationName) {
                animations.Play(state.name);      
                break;
            }
        }
    }
}
