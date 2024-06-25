using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle _paddle;
    [SerializeField] List<AudioClip> _ballSounds;

    private Vector2 _initialDistanceToPaddle;
    private bool _isAttachedToPaddle = true;

    private Vector2 _velocity;
    private Vector2 _previousPosition;
    private readonly float _launchSpeed = 5f;
    private readonly float _velocityLimit = 15f;
    private readonly float _randomCorrectionLimit = 0.5f;
    private Rigidbody2D _ballRigidbody;

    private AudioSource _ballAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _ballAudioSource = GetComponent<AudioSource>();
        _ballRigidbody = GetComponent<Rigidbody2D>();
        _previousPosition = transform.position;
        _initialDistanceToPaddle = transform.position - _paddle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAttachedToPaddle)
        {
            UpdateVelocity();
            if (Input.GetMouseButtonDown(0))
            {
                LaunchBall();
                _isAttachedToPaddle = false;
            }
            else
            {
                LockBallToPaddle();
            }
        }
        else
        {
            Debug.Log($"{_ballRigidbody.velocity}\t{_ballRigidbody.velocity.magnitude}");
        }

    }

    private void UpdateVelocity()
    {
        Vector2 currentPosition = transform.position;
        _velocity = (currentPosition - _previousPosition) / Time.deltaTime;
        _previousPosition = currentPosition;
    }

    private void LockBallToPaddle()
    {
        transform.position = (Vector2)_paddle.transform.position + _initialDistanceToPaddle;
    }

    private void LaunchBall()
    {
        _ballRigidbody.velocity = new Vector2(Mathf.Clamp(_velocity.x, -_velocityLimit, _velocityLimit), _launchSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isAttachedToPaddle)
        {
            PlayAnAudio(collision);
            RandomSpeedAlter();
        }
    }

    private void PlayAnAudio(Collision2D collision)
    {
        var audio = collision.gameObject.tag switch
        {
            "Wall" => _ballSounds.First(s => s.name.Equals("SFX_Click_2")),
            "Breakable block" => _ballSounds.First(s => s.name.Equals("SFX_Click")),
            "Unbreakable block" => _ballSounds.First(s => s.name.Equals("SFX_Clunk")),
            "Paddle" => _ballSounds.First(s => s.name.Equals("SFX_Bounce")),
            _ => _ballSounds.First(s => s.name.Equals("SFX_Click_2"))
        };
        _ballAudioSource.PlayOneShot(audio);
    }

    private void RandomSpeedAlter()
    {
        if (Mathf.Abs(_ballRigidbody.velocity.x) < 2 || Mathf.Abs(_ballRigidbody.velocity.y) < 2)
        {
            var horizontalVelocityChange =  Random.Range(-_randomCorrectionLimit, _randomCorrectionLimit);
            if (Mathf.Abs(_ballRigidbody.velocity.x + horizontalVelocityChange) > _ballRigidbody.velocity.magnitude)
            {
                horizontalVelocityChange = (_ballRigidbody.velocity.magnitude - Mathf.Abs(_ballRigidbody.velocity.x)) * Mathf.Sign(_ballRigidbody.velocity.x);
            }
            var newVerticalVelocity = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(_ballRigidbody.velocity.magnitude > _launchSpeed ? _ballRigidbody.velocity.magnitude : _launchSpeed , 2) - 
                Mathf.Pow(_ballRigidbody.velocity.x + horizontalVelocityChange, 2)));

            var verticalVelocityChange = Mathf.Sign(_ballRigidbody.velocity.y) * newVerticalVelocity - _ballRigidbody.velocity.y;

            _ballRigidbody.velocity += new Vector2(horizontalVelocityChange, verticalVelocityChange);
        }
    }
}
