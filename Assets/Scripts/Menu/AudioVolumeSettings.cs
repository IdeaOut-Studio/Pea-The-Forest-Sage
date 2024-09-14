using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeSettings : MonoBehaviour
{
    [Header("Main Menu UI")]
    [SerializeField] private AudioMixer audMixer;
    [SerializeField] private Slider sfxLevelSlider;
    [SerializeField] private Slider bgmLevelSlider;

    private string saveKeyVolumeBGM = "VolumeBGM";
    private string saveKeyVolumeSFX = "VolumeSFX";

    private void Start()
    {
        if (PlayerPrefs.HasKey(saveKeyVolumeSFX))
        {
            LoadVolumeBGM();
        }
        else
        {
            BgmSoundLevel();
        }

        if (PlayerPrefs.HasKey(saveKeyVolumeSFX)){
            LoadVolumeSFX();
        }
        else
        {
            SfxSoundLevel();
        }

    }

    public void BgmSoundLevel()
    {
        float _bgmLvl = bgmLevelSlider.value;
        audMixer.SetFloat("BGM", Mathf.Log10(_bgmLvl) * 20);
        PlayerPrefs.SetFloat(saveKeyVolumeBGM, _bgmLvl);
    }

    public void SfxSoundLevel()
    {
        float _sfxLvl = sfxLevelSlider.value;
        audMixer.SetFloat("SFX", Mathf.Log10(_sfxLvl) * 20);
        PlayerPrefs.SetFloat(saveKeyVolumeSFX, _sfxLvl);
    }

    private void LoadVolumeBGM()
    {
        bgmLevelSlider.value = PlayerPrefs.GetFloat(saveKeyVolumeBGM);
        BgmSoundLevel();
    }
    private void LoadVolumeSFX()
    {
        sfxLevelSlider.value = PlayerPrefs.GetFloat(saveKeyVolumeSFX);
        SfxSoundLevel();
    }
}
