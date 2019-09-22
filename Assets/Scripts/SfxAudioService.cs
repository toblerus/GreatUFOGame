using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class SfxAudioService : MonoBehaviour
{
    [Serializable]
    private class SfxSetting
    {
        public SfxClip SfxClip;
        public AudioClip AudioClip;
    }
    
    public static SfxAudioService Instance;

    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private SfxSetting[] _sfxSettings;
    
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    public void PlayOneShotForClip(SfxClip clip)
    {
        if(_sfxAudioSource.isPlaying)
            _sfxAudioSource.Stop();
        
        var audioClip = _sfxSettings.First(setting => setting.SfxClip == clip).AudioClip;
        _sfxAudioSource.PlayOneShot(audioClip);
        
        Application.Quit();
    }
    
    public void PlayOneShotForClipWithVolumeScale(SfxClip clip, float volumeScale)
    {
        if(_sfxAudioSource.isPlaying)
            _sfxAudioSource.Stop();
        
        var audioClip = _sfxSettings.First(setting => setting.SfxClip == clip).AudioClip;
        _sfxAudioSource.PlayOneShot(audioClip, volumeScale);
    }
}
