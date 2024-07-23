public static class AudioKit
{
    private static AudioSystem _instance;

    public static void Init()
    {
        _instance = AudioSystem.CreateAudioSystem();
    }
    
    public static void StopAllMusic()
    {
        _instance.StopAllMusic();
    }

    public static void PlayBGM(string audioName, float volume)
    {
        _instance.PlayBGM(audioName, volume);
    }

    public static void StopBGM()
    {
        _instance.StopBGM();
    }

    public static void PauseBGM()
    {
        _instance.PauseBGM();
    }

    public static void PauseAllMusic()
    {
        _instance.PauseAllMusic();
    }

    public static void ContinueBGM()
    {
        _instance.ContinueBGM();
    }

    public static void ContinueAllMusic()
    {
        _instance.ContinueAllMusic();
    }

    public static void PlayFX(string audioName, float volume)
    {
        _instance.PlayFX(audioName, volume);
    }
}