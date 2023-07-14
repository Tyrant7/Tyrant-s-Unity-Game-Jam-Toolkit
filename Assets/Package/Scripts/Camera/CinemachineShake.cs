using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// A basic camera shake script which can be added to a CinemachineVirtualCamera.
/// Max one instance may exist at a time.
/// </summary>
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineBasicMultiChannelPerlin cmMultiChannelPerlin;

    private float shakeTimer;
    private float shakeTimerTotal;

    private float startingIntensity;
    private float startingFrequency;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            CinemachineVirtualCamera cmVirtualCam = GetComponent<CinemachineVirtualCamera>();
            if (cmVirtualCam != null)
            {
                cmMultiChannelPerlin = cmVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Shakes the camera with a provided intensity, frequency, and duration.
    /// </summary>
    /// <param name="intensity">The intensity of the shake.</param>
    /// <param name="frequency">The frequency of the shake.</param>
    /// <param name="duration">The duration of the shake.</param>
    public void ShakeCamera(float intensity, float frequency, float duration)
    {
        cmMultiChannelPerlin.m_AmplitudeGain = intensity;
        cmMultiChannelPerlin.m_FrequencyGain = frequency;

        startingIntensity = intensity;
        startingFrequency = frequency;

        shakeTimerTotal = duration;
        shakeTimer = duration;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            cmMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(0f, startingIntensity, shakeTimer / shakeTimerTotal);
            cmMultiChannelPerlin.m_FrequencyGain = Mathf.Lerp(1f, startingFrequency, shakeTimer / shakeTimerTotal);
        }
        else
        {
            // If there is a bug, just comment these two lines out
            // I haven't tested them :P
            cmMultiChannelPerlin.m_AmplitudeGain = 0;
            cmMultiChannelPerlin.m_FrequencyGain = 1;
        }
    }
}
