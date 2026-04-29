using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Music Tracks")]
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip[] _combatMusic;
    [SerializeField] private AudioClip _bossMusic;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _turnEndClash;
    [SerializeField] private AudioClip _selectCard;

    private const string MUSIC_VOL_KEY = "MusicVolume";
    private const string SFX_VOL_KEY = "SFXVolume";

    private int _currentCombatTrack = 0;

    void Start()
    {
        float musicVol = PlayerPrefs.GetFloat(MUSIC_VOL_KEY, 0.15f);
        float sfxVol = PlayerPrefs.GetFloat(SFX_VOL_KEY, 0.15f);

        _musicSource.volume = musicVol;
        _sfxSource.volume = sfxVol;

        PlayMenuMusic();
    }
    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //--------- MUSIC ----------

    public void PlayMenuMusic() => PlayMusic(_menuMusic);
    public void PlayBossMusic() => PlayMusic(_bossMusic);

    public void PlayCombatMusic()
    {
        if (_combatMusic.Length == 0) return;

        AudioClip track = _combatMusic[_currentCombatTrack];
        _currentCombatTrack = (_currentCombatTrack + 1) % _combatMusic.Length;

        PlayMusic(track);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (_musicSource.clip == clip && _musicSource.isPlaying) return;

        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    //-------------- SOUND EFFECTS ----------

    public void PlayButtonClick() => _sfxSource.PlayOneShot(_buttonClick);
    public void PlayTurnEndClash() => _sfxSource.PlayOneShot(_turnEndClash);
    public void PlaySelectCard() => _sfxSource.PlayOneShot(_selectCard);

    //----------- MUSIC ENDED CALLBACK -----------

    void Update()
    {
        //When combat music ends, play next track
        if (!_musicSource.isPlaying && _musicSource.clip != null && 
            System.Array.Exists(_combatMusic, track => track == _musicSource.clip))
        {
            PlayCombatMusic();
        }
    }

    // ---------- AUDIO SLIDERS ---------
    public void SetMusicVolume(float volume)
    {
        _musicSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOL_KEY, volume);
    }

    public void SetSFXVolume(float volume)
    {
        _sfxSource.volume = volume;
        PlayerPrefs.SetFloat(SFX_VOL_KEY, volume);
    }
}