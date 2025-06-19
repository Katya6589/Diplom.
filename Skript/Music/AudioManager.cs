using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip cubeClick;
    public AudioClip menuClick;

    private bool isMusicOn = true;
    private bool isSfxOn = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource.loop = true;
            musicSource.clip = backgroundMusic;
            if (isMusicOn)
                musicSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayCubeClick()
    {
        if (isSfxOn && cubeClick != null)
            sfxSource.PlayOneShot(cubeClick);
    }

    public void PlayMenuClick()
    {
        if (isSfxOn && menuClick != null)
            sfxSource.PlayOneShot(menuClick);
    }

    // === Только эти две функции для кнопок ===

    /// <summary>
    /// Включить/выключить фоновую музыку одной кнопкой
    /// </summary>
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        if (isMusicOn)
            musicSource.Play();
        else
            musicSource.Pause();
    }

    /// <summary>
    /// Включить/выключить все звуки (эффекты) одной кнопкой
    /// </summary>
    public void ToggleSfx()
    {
        isSfxOn = !isSfxOn;
    }

    public void PauseAllAudio()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Pause();
        if (sfxSource != null && sfxSource.isPlaying)
            sfxSource.Pause();
    }

    public void ResumeAllAudio()
    {
        if (isMusicOn && musicSource != null && !musicSource.isPlaying)
            musicSource.Play();
        // Обычно эффекты не надо запускать, они проигрываются только по действию
    }


    public bool IsMusicOn() => isMusicOn;
    public bool IsSfxOn() => isSfxOn;
}
