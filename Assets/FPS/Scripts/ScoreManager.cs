using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMesh Pro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int Score { get; private set; } = 0;
    public TMP_Text scoreText; // Assign in inspector

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddPoint()
    {
        Score++;
        if (scoreText != null)
            scoreText.text = $"Score: {Score}";
    }
}