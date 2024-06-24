using System;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip _soundOnDestroy;
    [SerializeField] private GameObject _vfxOnDestroy;
    [SerializeField] private List<Sprite> _hitSprites;

    private int _currentHits = 0;
    private int _maxHits;

    private void Start()
    {
        if (CompareTag("Breakable Block"))
        {
            FindObjectOfType<Level>().AddBreakableBlock();
            _maxHits = _hitSprites.Count + 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Breakable Block"))
        {
            _currentHits++;
            if (_currentHits >= _maxHits)
            {
                FindObjectOfType<GameSession>().IncreaseScore();
                DestroyBlock();
            }
            else
            {
                ChangeHitSprite();
            }
        }
    }

    private void ChangeHitSprite()
    {
        GetComponent<SpriteRenderer>().sprite = _hitSprites[_currentHits - 1];
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(_soundOnDestroy, Camera.main.transform.position, 0.05f);
        TriggerVfx();
        FindObjectOfType<Level>().RemoveBreakableBlock();
        Destroy(gameObject);
    }

    private void TriggerVfx()
    {
        var vfx = Instantiate(_vfxOnDestroy, transform.position, transform.rotation);
        Destroy(vfx, 2f);
    }
}
