using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jext;
using UnityEngine.UI;

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
        if (highScores == null)
            highScores = new HighScores();
    }

    #region Timer
    public void StartTimer()
    {
        timer = StartCoroutine(Timer());
    }

    private float time;
    private Coroutine timer;
    [SerializeField]
    private Text timerText;
    private IEnumerator Timer()
    {
        time = 0;
        while(true)
        {
            timerText.text = ConvScore(time);
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

    [SerializeField, Tooltip("5 Lang")]
    private List<Text> scores = new List<Text>();
    [SerializeField]
    private GameObject scoreHolder;
    [SerializeField]
    private Image scoreBoard;
    [SerializeField]
    private float fadeSpeed;
    private int place;
    public IEnumerator ShowScores(float newScore)
    {
        place = AddScore(newScore);
        yield return StartCoroutine(scoreBoard.FadeToBlack(fadeSpeed, Methods.FadeType.FadeOut));
        scoreHolder.gameObject.SetActive(true);

        for (int score = 0; score < 5; score++)
            scores[score].text = place == score ? "Your score: " + "" + ConvScore(highScores.scores[score]);
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

    private string ConvScore(float score)
    {
        return (Mathf.Round(score * 100f) / 100f).ToString();
    }
}
