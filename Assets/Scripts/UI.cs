using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    int score;
    [SerializeField] Text scoreOutput;

    void Start()
    {
        score = 0;
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreOutput.text = score.ToString();
    }
}
