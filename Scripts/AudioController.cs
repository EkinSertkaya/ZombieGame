using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    AudioSource mainCameraAudioSource;
    AudioSource gameplayAudioSource;

    Slider volumeSlider;

    private void Start()
    {
        mainCameraAudioSource = Camera.main.gameObject.GetComponent<AudioSource>();
        gameplayAudioSource = GameObject.Find("Gameplay Audio Source").GetComponent<AudioSource>();
        volumeSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        if(gameObject.name == "Music Volume Slider")
        {
            mainCameraAudioSource.volume = volumeSlider.value;
        }
        else if(gameObject.name == "Effects Volume Slider")
        {
            gameplayAudioSource.volume = volumeSlider.value;
        }
    }
}
