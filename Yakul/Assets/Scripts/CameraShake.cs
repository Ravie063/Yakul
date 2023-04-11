using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    static CinemachineVirtualCamera VirtualCam;
    float shakeTimer;
    private void Awake()
    {
        Instance = this;
        VirtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCam(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin CamNoise =
            VirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CamNoise.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin CamNoise =
           VirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                CamNoise.m_AmplitudeGain = 0f;
            }
        }
    }
}
