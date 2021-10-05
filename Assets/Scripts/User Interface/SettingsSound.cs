using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSound : MonoBehaviour
{
    private SaveSettings _sv = new SaveSettings();
    private string _path;

    [SerializeField] private AudioSource[] _musicSource;
    [SerializeField] private AudioSource[] _soundsSource;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    private void Start()
    {
        _path = Path.Combine(Application.dataPath, "SaveSettings.json");

        if (File.Exists(_path))
        {
            _sv = JsonUtility.FromJson<SaveSettings>(File.ReadAllText(_path));

            foreach (AudioSource sourses in _musicSource) sourses.volume = _sv.musicVolume;
            _musicSlider.value = _sv.musicVolume;

            foreach (AudioSource sourses in _soundsSource) sourses.volume = _sv.soundVolume;
            _soundSlider.value = _sv.soundVolume;
        }
        else
        {
            _sv.musicVolume = _musicSlider.value;
            _sv.soundVolume = _soundSlider.value;
        }
    }

    public void Save()
    {
        if (File.Exists(_path))
        {
            File.Delete(_path);
            File.WriteAllText(_path, JsonUtility.ToJson(_sv));
        }
        else
            File.WriteAllText(_path, JsonUtility.ToJson(_sv));
    }

    public void MusicValueChange()
    {
        foreach (AudioSource sourses in _musicSource) sourses.volume = _musicSlider.value;
        _sv.musicVolume = _musicSlider.value;
    }

    public void SoundValueChange()
    {
        foreach (AudioSource sourses in _soundsSource) sourses.volume = _soundSlider.value;
        _sv.soundVolume = _soundSlider.value;
    }
}

[Serializable]
public class SaveSettings
{
    public float musicVolume;
    public float soundVolume;
}
