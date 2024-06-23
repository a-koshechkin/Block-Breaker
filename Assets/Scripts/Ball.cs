using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle _paddle;
    private Vector2 _initialDistanceToPaddle;
    private bool _isAttachedToPaddle;
    private Vector2 _velocity;
    private Vector2 _previousPosition;
    private readonly float _launchSpeed = 15f;
    private readonly float _velocityLimit = 15f;
    [SerializeField] List<AudioClip> _ballSounds;
    private AudioSource _ballAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _ballAudioSource = GetComponent<AudioSource>();
        _previousPosition = transform.position;
        _isAttachedToPaddle = true;
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
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Clamp(_velocity.x, -_velocityLimit, _velocityLimit), _launchSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isAttachedToPaddle)
        {
            var audio = collision.gameObject.tag switch
            {
                "Wall" => _ballSounds.First(s => s.name.Equals("SFX_Click_2")),
                "Block" => _ballSounds.First(s => s.name.Equals("SFX_Click")),
                "Paddle" => _ballSounds.First(s => s.name.Equals("SFX_Bounce")),
                _ => _ballSounds.First(s => s.name.Equals("SFX_Clunk"))
            };
            _ballAudioSource.PlayOneShot(audio);
        }
    }
}
