using UnityEngine;

public class Score : MonoBehaviour
{

    private float score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        score = 0;
    }


    public void addScore(float score)
    {
        score += score;
    }

    public float getScore()
    {
        return score;
    }

}
