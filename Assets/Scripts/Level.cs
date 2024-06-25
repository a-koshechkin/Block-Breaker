using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    #region Fields

    private int _numberOfBreakableBlocks;

    #endregion

    #region Methods

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
        SceneLoader.LoadNextScene();
    }

    #endregion
}
