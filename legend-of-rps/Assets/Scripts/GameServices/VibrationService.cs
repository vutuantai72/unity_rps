using MoreMountains.NiceVibrations;
using UnityEngine;

public class VibrationService : UnityActiveSingleton<VibrationService>
{
    private const float MinCapForVibration = 1f;
    private float lastTimePlayHaptics;
    private float lastTimePlayHapticsMedium;
    private float lastTimePlayHapticsHeavy;

    private void Start()
    {
        this.gameObject.AddComponent<GlobalData>();
        MMVibrationManager.SetHapticsActive(true);
    }

    /// <summary>
    /// Play Haptics In Game
    /// </summary>
    public void PlayHaptics()
    {
        float now = Time.realtimeSinceStartup;
        if (now - lastTimePlayHaptics < MinCapForVibration || !GameService.@object.isTurnOnVibration.Value)
        {
            //not allow vibration too much at a single time
            return;
        }

        lastTimePlayHaptics = now;

        if (Application.platform == RuntimePlatform.Android)
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);
        }
        else
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact, false, true, this);
        }
    }

    /// <summary>
    /// Play Haptics In Game
    /// </summary>
    public void PlayHapticsHeavyImpact()
    {
        float now = Time.realtimeSinceStartup;
        if (now - lastTimePlayHapticsHeavy < MinCapForVibration || !GameService.@object.isTurnOnVibration.Value)
        {
            return;
        }

        lastTimePlayHapticsHeavy = now;
        if (Application.platform == RuntimePlatform.Android)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact, false, true, this);
        }
        else
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact, false, true, this);
        }
    }

    /// <summary>
    /// Play Haptics In Game
    /// </summary>
    public void PlayHapticsMedium()
    {
        float now = Time.realtimeSinceStartup;
        if (now - lastTimePlayHapticsMedium < MinCapForVibration || !GameService.@object.isTurnOnVibration.Value)
        {
            return;
        }

        lastTimePlayHapticsMedium = now;
        if (Application.platform == RuntimePlatform.Android)
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);
        }
        else
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);
        }
    }

    /// <summary>
    /// Play Soft Haptics In Game
    /// </summary>
    public void PlaySoftHaptics()
    {
        if (!GameService.@object.isTurnOnVibration.Value)
        {
            return;
        }

        PlayHaptics();
    }
}
