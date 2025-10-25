using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    private float TimeValue = 120;
    private int ScoreValue;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI ScoreText;
    private void FixedUpdate()
    {
        TimeValue -= Time.deltaTime;
        UpdateUI();
    }
    public void UpdateUI()
    {
        TimeText.text = "Time: "+((int)TimeValue).ToString();
        ScoreText.text = "Score: "+ ScoreValue.ToString();
    }
    public void AddScoreUI(int score)
    {
        ScoreValue += score;
        UpdateUI();
    }
    public void UpdateUI(int Time,int Score)
    {
        TimeValue = Time;
        ScoreValue = Score;
        UpdateUI();
    }
}
