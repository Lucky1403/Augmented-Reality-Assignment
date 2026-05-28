using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private VideoLoader videoLoader;
    private int currentVideoIndex = 0;
    private int totalVideos = 2;
    private float activeAlpha = 1.0f;
    private float inactiveAlpha = 0.3f;

    // Start is called before the first frame update
    private void Start()
    {
        leftButton.onClick.AddListener(OnLeftPressed);
        rightButton.onClick.AddListener(OnRightPressed);

        UpdateButtonAlpha();
    }

    private void OnRightPressed()
    {
        if(currentVideoIndex < totalVideos - 1)
        {
            currentVideoIndex++;
            videoLoader.SwitchToVideo(currentVideoIndex);
            UpdateButtonAlpha();
        }
    }

    private void OnLeftPressed()
    {
        if(currentVideoIndex > 0)
        {
            currentVideoIndex--;
            videoLoader.SwitchToVideo(currentVideoIndex);
            UpdateButtonAlpha();
        }
    }
    
    private void UpdateButtonAlpha()
    {
        SetButtonAlpha(leftButton, currentVideoIndex == 0 ? inactiveAlpha : activeAlpha);
        SetButtonAlpha(rightButton, currentVideoIndex == totalVideos - 1 ? inactiveAlpha : activeAlpha);
    }

    private void SetButtonAlpha(Button btn, float alpha)
    {
        Image img = btn.GetComponent<Image>();
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}
