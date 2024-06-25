using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    #region MonoBehaviour

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneLoader.LoadGameOverScene();
    }

    #endregion
}
