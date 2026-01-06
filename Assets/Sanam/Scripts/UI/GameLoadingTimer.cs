using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadingTimer : MonoBehaviour
{
    public Image ProgressImage;
    public TextMeshProUGUI ProgressText;

    private float totalTime = 5f; // Total time for loading in seconds
    private float currentTime = 0f;

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        if (_isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                _isTimerRunning = false;
                gameObject.SetActive(false);
            }
            else
            {
                UpdateShowingDetails();
            }
        }
    }

    private void UpdateShowingDetails()
    {
        ProgressImage.fillAmount = Mathf.Max(currentTime/totalTime, 0);
        ProgressText.text = $"{currentTime:F0}";
    }

    private bool _isTimerRunning;
    public void StartTimer(float time)
    {
        totalTime = time;
        currentTime = time;
        _isTimerRunning = true;
        UpdateShowingDetails();
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _isTimerRunning = false;
        gameObject.SetActive(false);
    }
}
