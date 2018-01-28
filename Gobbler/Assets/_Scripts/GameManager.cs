using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jext;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    [SerializeField]
    private List<string> highScoreSavePath = new List<string>();
    public static HighScores highScores;
    private string path;

    private void Awake()
    {
        instance = this;
        path = Methods.GenerateFilePath(highScoreSavePath);
        highScores = Methods.Load<HighScores>(path);
    }

    #region Timer
    public void StartTimer()
    {
        timer = StartCoroutine(Timer());
    }

    private float time;
    private Coroutine timer;
    private IEnumerator Timer()
    {
        time = 0;
        while(true)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    public int EndTimer()
    {
        StopCoroutine(timer);
        return AddScore(time);
    }
    #endregion

    private int AddScore(float newScore)
    {
        for (int score = 0; score < 5; score++)
            if(highScores.scores[score] < newScore)
            {
                highScores.scores[score] = newScore;
                return score;
            }
        SaveScores();
        return 6;
    }

    private void SaveScores()
    {
        Methods.Save(highScores, path);
    }

    public class HighScores
    {
        public float[] scores = new float[5];

        public HighScores()
        {
            for (int score = 0; score < scores.Length; score++)
                scores[score] = 0;
        }
    }
}
