using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSSounds : MonoBehaviour
{
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip runningSound;
    
    private AudioSource _source;
    private ScFPSController _scFPSController;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _scFPSController = GetComponent<ScFPSController>();
    }

    private void Update()
    {
        // If the player moves then we play the sound
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            // Depending on the running or not play appropriate sound
            switch (_scFPSController.isRunning)
                {
                    case true:
                        if (!_source.isPlaying || _source.clip == walkingSound)
                        {
                            _source.clip = runningSound;
                            _source.Play();
                        }
                        break;
                    case false:
                        if (!_source.isPlaying || _source.clip == runningSound)
                        {
                            _source.clip = walkingSound;
                            _source.Play(); 
                        }
                        break;
                }
        }
        else
        {
            _source.Stop();
        }
    }
}
