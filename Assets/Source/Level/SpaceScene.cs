using System.Collections;
using UnityEngine;

public sealed class SpaceScene : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        LevelManager.LoadDeferredLevel();
    }
}