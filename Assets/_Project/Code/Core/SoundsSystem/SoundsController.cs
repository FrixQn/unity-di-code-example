using UnityEngine;
using VContainer;


public class SoundsController : MonoBehaviour, ISoundsController
{
    
    public IAudioClipsLibrary SoundsLibrary { get; private set; }

    public IAudioClipsLibrary MusicLibrary => throw new System.NotImplementedException();

    [Inject]
    private void Inject(SoundsLibrary soundsLibrary)
    {
        SoundsLibrary = soundsLibrary;
    }

    public void PlayMusic(string name)
    {
        throw new System.NotImplementedException();
    }

    public void PlaySound(string name)
    {
        var clip = SoundsLibrary.FindClip(name, true);
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
}
