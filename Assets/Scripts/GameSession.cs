using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [Range(_minGameSpeed, _maxGameSpeed)][SerializeField] private float _gameSpeed = 1;
    [SerializeField] TextMeshProUGUI _scoreText;

    private const float _minGameSpeed = 0.25f;
    private const float _maxGameSpeed = 4f;

    private int _currentScore = 0;
    private int _pointsPerBlock = 50;

    private void Awake()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = _currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = _gameSpeed;
    }

    public void IncreaseScore()
    {
        _currentScore += _pointsPerBlock;
        _scoreText.text = _currentScore.ToString();
    }

    public void ResetTheGame()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
