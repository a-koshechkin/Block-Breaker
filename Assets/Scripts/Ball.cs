using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static Constants;

public class Ball : MonoBehaviour
{
    #region Fields

    [SerializeField] List<AudioClip> _ballSounds;

    private Vector2 _initialDistanceToPaddle;
    private bool _isAttachedToPaddle = true;

    private Vector2 _velocity;
    private Vector2 _previousPosition;
    private readonly float _launchSpeed = 10f;
    private readonly float _velocityUpperLimit = 15f;
    private readonly float _axisVelocityLowerLimit = 2f;
    private readonly float _randomCorrectionLimit = 0.5f;
    private Rigidbody2D _ballRigidbody;

    private AudioSource _ballAudioSource;
    private Paddle _paddle;

    #endregion

    #region MonoBehaviour

    void Start()
    {
        _ballAudioSource = GetComponent<AudioSource>();
        _ballRigidbody = GetComponent<Rigidbody2D>();

        _paddle = FindObjectOfType<Paddle>();

        _initialDistanceToPaddle = transform.position - _paddle.transform.position;
        UpdateLockedPosition();
    }

    void Update()
    {
        if (_isAttachedToPaddle)
        {
            UpdateAttachedState();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isAttachedToPaddle)
        {
            PlayAnAudio(collision);
            RandomSpeedAlter();
        }
    }

    #endregion MonoBehaviour

    #region Methods

    private void UpdateAttachedState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LaunchBall();
        }
        else
        {
            UpdateLockedPosition();
        }
    }

    private void UpdateLockedPosition()
    {
        transform.position = (Vector2)_paddle.transform.position + _initialDistanceToPaddle;
        _previousPosition = transform.position;
    }

    private void LaunchBall()
    {
        _isAttachedToPaddle = false;
        _velocity = ((Vector2)transform.position - _previousPosition) / Time.deltaTime;
        _ballRigidbody.velocity = new Vector2(Mathf.Clamp(_velocity.x, -_velocityUpperLimit, _velocityUpperLimit), _launchSpeed);
    }

    private void PlayAnAudio(Collision2D collision)
    {
        var entity = Tags.First(t => collision.gameObject.CompareTag(t.Value)).Key;
        var audio = _ballSounds.First(s => s.name.Equals(BallSounds[entity]));
        _ballAudioSource.PlayOneShot(audio);
    }

    private void RandomSpeedAlter()
    {
        if (Mathf.Abs(_ballRigidbody.velocity.x) < _axisVelocityLowerLimit || Mathf.Abs(_ballRigidbody.velocity.y) < _axisVelocityLowerLimit)
        {
            var horizontalVelocityChange = Random.Range(-_randomCorrectionLimit, _randomCorrectionLimit);
            if (Mathf.Abs(_ballRigidbody.velocity.x + horizontalVelocityChange) > _ballRigidbody.velocity.magnitude)
            {
                horizontalVelocityChange = (_ballRigidbody.velocity.magnitude - Mathf.Abs(_ballRigidbody.velocity.x)) * Mathf.Sign(_ballRigidbody.velocity.x);
            }
            var newVerticalVelocity = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(_ballRigidbody.velocity.magnitude > _launchSpeed ? _ballRigidbody.velocity.magnitude : _launchSpeed, 2) -
                Mathf.Pow(_ballRigidbody.velocity.x + horizontalVelocityChange, 2)));

            var verticalVelocityChange = Mathf.Sign(_ballRigidbody.velocity.y) * newVerticalVelocity - _ballRigidbody.velocity.y;

            _ballRigidbody.velocity += new Vector2(horizontalVelocityChange, verticalVelocityChange);
        }
    }
    #endregion
}
