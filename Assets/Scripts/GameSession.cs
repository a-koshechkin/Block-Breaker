using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    #region Fields

    [Range(_minGameSpeed, _maxGameSpeed)][SerializeField] private float _gameSpeed = 1;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] bool _isAutoplayEnabled = false;

    private const float _minGameSpeed = 0.25f;
    private const float _maxGameSpeed = 4f;

    private int _currentScore = 0;
    private readonly int _pointsPerBlock = 50;

    #endregion

    #region Properties

    public bool IsAutoplayEnabled => _isAutoplayEnabled;

    #endregion

    #region MonoBehaviour

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

    void Start()
    {
        _scoreText.text = _currentScore.ToString();
    }

    void Update()
    {
        Time.timeScale = _gameSpeed;
    }

    #endregion

    #region Methods

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

    #endregion
}
