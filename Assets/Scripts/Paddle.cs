using UnityEngine;

public class Paddle : MonoBehaviour
{
    private GameSession _gameSession;
    private Ball _ball;
    private float _fieldWidthInUnits;
    private float _pixelsToUnitsRatio;
    private readonly float _paddleSize = 2f;
    private float _minX;
    private float _maxX;
    // Start is called before the first frame update
    void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
        _ball = FindObjectOfType<Ball>();
        _fieldWidthInUnits = Camera.main.orthographicSize * Camera.main.aspect * 2;
        _pixelsToUnitsRatio = _fieldWidthInUnits / Screen.width;
        _minX = _paddleSize / 2;
        _maxX = _fieldWidthInUnits - _paddleSize / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(GetNewXPosition(), transform.position.y);
    }

    private float GetNewXPosition()
    {
        if (_gameSession.IsAutoplayEnabled)
        {
            return Mathf.Clamp(_ball.transform.position.x, _minX, _maxX);
        }
        else
        {
            return Mathf.Clamp(Input.mousePosition.x * _pixelsToUnitsRatio, _minX, _maxX);
        }
    }
}
