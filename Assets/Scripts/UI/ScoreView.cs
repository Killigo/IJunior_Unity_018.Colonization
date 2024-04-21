using TMPro;
using UnityEngine;

[RequireComponent(typeof(ScoreCounter))]

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    private ScoreCounter _scoreCounter;

    private void Awake()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

    private void OnEnable()
    {
        _scoreCounter.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score.text = score.ToString();
    }
}

