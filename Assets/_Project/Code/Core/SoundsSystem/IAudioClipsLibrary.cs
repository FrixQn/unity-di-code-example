using UnityEngine;

public interface IAudioClipsLibrary
{
    AudioClip FindClip(string name, bool throwIfNotFound = false);
}
