using UnityEngine;
using UnityEngine.UI;

public class highScore : MonoBehaviour
{
    public Text newScore;
    private int score;

    private void Start()
    {
        ZPlayerPrefs.Initialize("CD#XL6yQkpZri3/", "CrG16@gA20");
        score = ZPlayerPrefs.GetInt("HighScore", 0);
        if (score < globalControl.Instance.currentWave)
        {
            score = globalControl.Instance.currentWave;
            ZPlayerPrefs.SetInt("HighScore", score);
        }
        newScore.text = "Highest Roud: " + score;
        ZPlayerPrefs.Save();
    }


    public void resetScore()
    {
        ZPlayerPrefs.DeleteAll();
        newScore.text = "Highest Roud: 0";
    }

}
