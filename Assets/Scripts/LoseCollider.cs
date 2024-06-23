using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _sceneLoader.LoadGameOverScene();
    }
}
