using UnityEngine;
using System.Collections;

public sealed class MusicSource : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource source;

    public static bool MusicEnabled { get; set; } = true;

    private IEnumerator Start()
    {
        if (!MusicEnabled) yield break;

        while (true)
        {
            var randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
            source.PlayOneShot(randomClip);
            yield return new WaitForSeconds(randomClip.length);
        }
    }
}
