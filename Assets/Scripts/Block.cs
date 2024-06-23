using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip _soundOnDestroy;

    private Level _level;

    private void Start()
    {
        _level = FindObjectOfType<Level>();
        _level.AddBreakableBlock();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBlock();
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(_soundOnDestroy, Camera.main.transform.position, 0.05f);
        _level.RemoveBreakableBlock();
        Destroy(gameObject);
    }
}
