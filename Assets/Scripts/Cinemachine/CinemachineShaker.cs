using Cinemachine;
using UnityEngine;

public class CinemachineShaker : MonoBehaviour
{
    public static CinemachineShaker Instance { get; private set; }

    public CinemachineVirtualCamera cam;

    public float timer;
    public float timerTotal;
    public float startIntensity;

    private bool smooth;
    private CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        Instance = this;
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (smooth)
            {
                noise.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, 1 - (timer / timerTotal));
            }

            if (timer <= 0f)
            {
                noise.m_AmplitudeGain = 0f;
                Instance.enabled = false;
            }
        }
    }

    public void Shake(float intensity, float time)
    {
        noise.m_AmplitudeGain = intensity;
        timer = time;
        smooth = false;
    }

    public void ShakeSmooth(float intensity, float time)
    {
        noise.m_AmplitudeGain = intensity;
        startIntensity = intensity;
        timer = time;
        timerTotal = time;
        smooth = true;
    }
}
