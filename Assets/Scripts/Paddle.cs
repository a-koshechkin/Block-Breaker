using UnityEngine;

public class Paddle : MonoBehaviour
{
    private float _fieldWidthInUnits;
    private float _pixelsToUnitsRatio;
    private readonly float _paddleSize = 2f;
    private float _minX;
    private float _maxX;
    // Start is called before the first frame update
    void Start()
    {
        _fieldWidthInUnits = Camera.main.orthographicSize * Camera.main.aspect * 2;
        _pixelsToUnitsRatio = _fieldWidthInUnits / Screen.width;
        _minX = _paddleSize / 2;
        _maxX = _fieldWidthInUnits - _paddleSize / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(Input.mousePosition.x * _pixelsToUnitsRatio, _minX, _maxX), transform.position.y);
    }
}
