using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    private int _numberOfBreakableBlocks;
    private SceneLoader _sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBreakableBlock()
    {
        _numberOfBreakableBlocks++;
    }

    public void RemoveBreakableBlock()
    {
        _numberOfBreakableBlocks--;
        if ( _numberOfBreakableBlocks <= 0)
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(0.25f);
        _sceneLoader.LoadNextScene();
    }
}
