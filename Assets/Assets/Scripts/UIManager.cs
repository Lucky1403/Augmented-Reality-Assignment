// UIManager.cs
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

    private void Start()
    {
        leftButton.onClick.AddListener(OnLeftPressed);
        rightButton.onClick.AddListener(OnRightPressed);

        UpdateButtonAlpha();
    }

    private void OnRightPressed()
    {
        if (currentVideoIndex < totalVideos - 1)
        {
            currentVideoIndex++;
            videoLoader.SwitchToVideo(currentVideoIndex);
            UpdateButtonAlpha();
            Debug.Log("Right pressed, video index: " + currentVideoIndex);
        }
    }

    private void OnLeftPressed()
    {
        if (currentVideoIndex > 0)
        {
            currentVideoIndex--;
            videoLoader.SwitchToVideo(currentVideoIndex);
            UpdateButtonAlpha();
            Debug.Log("Left pressed, video index: " + currentVideoIndex);
        }
    }

    private void UpdateButtonAlpha()
    {
        SetButtonAlpha(leftButton,
            currentVideoIndex == 0 ? inactiveAlpha : activeAlpha);
        SetButtonAlpha(rightButton,
            currentVideoIndex == totalVideos - 1 ? inactiveAlpha : activeAlpha);
    }

    private void SetButtonAlpha(Button btn, float alpha)
    {
        Image img = btn.GetComponent<Image>();
        if (img != null)
        {
            Color c = img.color;
            c.a = alpha;
            img.color = c;
        }
    }
}