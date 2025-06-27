using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SoundsLibrary), menuName = "Project/SoundsController/SoundsLibrary")]
public class SoundsLibrary : ScriptableObject, IAudioClipsLibrary
{
    [Serializable]
    private struct ClipDefinition
    {
        [field: SerializeField] public string Name {get; private set;}
        [field: SerializeField] public AudioClip Clip { get; private set; }
    }

    [SerializeField] private ClipDefinition[] _clips;

    private static int NameToHash(in string name)
    {
        return name.ToLower().GetHashCode();
    }

    public AudioClip FindClip(string name, bool throwIfNotFound)
    {
        foreach(var clipDefinition in _clips)
        {
            if (NameToHash(name) == NameToHash(clipDefinition.Name))
            {
                return clipDefinition.Clip;
            }
        }

        if (throwIfNotFound)
            throw new Exception($"Can't find a clip with name: {name}");

        return null;
    }

    
}
