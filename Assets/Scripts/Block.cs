using System.Collections.Generic;
using UnityEngine;

using static Constants;

public class Block : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioClip _soundOnDestroy;
    [SerializeField] private GameObject _vfxOnDestroy;
    [SerializeField] private List<Sprite> _hitSprites;

    private int _currentHits = 0;
    private int _maxHits;

    #endregion

    #region MonoBehaviour
    private void Start()
    {
        if (IsBreakableBlock())
        {
            FindObjectOfType<Level>().AddBreakableBlock();
            _maxHits = _hitSprites.Count + 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsBreakableBlock())
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

    #endregion

    #region Methods

    private bool IsBreakableBlock()
    {
        return CompareTag(Tags[Entities.BreakableBlock]);
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
        Destroy(Instantiate(_vfxOnDestroy, transform.position, transform.rotation), 2f);
    }

    #endregion
}
