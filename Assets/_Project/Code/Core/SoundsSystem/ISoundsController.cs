public interface ISoundsController
{
    public IAudioClipsLibrary SoundsLibrary { get; }
    public IAudioClipsLibrary MusicLibrary { get; }
    void PlaySound(string name);
    void PlayMusic(string name);
}
