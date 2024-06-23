using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [Range(0.25f, 4f)] [SerializeField] private float _gameSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = _gameSpeed;
    }
}
