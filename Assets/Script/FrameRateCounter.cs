using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display = default;

    public enum DisplayMode { FPS, MS };

    [SerializeField]
    DisplayMode displayMode = DisplayMode.FPS;

    [SerializeField, Range(0.1f, 2.0f)]
    float sampleDuration = 1.0f;

    int frames;

    float duration;
    float bestDuration = float.MaxValue;
    float worstDuration = 0.0f;

    private void Update()
    {
        var frameDuration = Time.unscaledDeltaTime;
        frames++;
        duration += frameDuration;

        if (frameDuration < bestDuration)
        {
            bestDuration = frameDuration;
        }
        if (frameDuration > worstDuration)
        {
            worstDuration = frameDuration;
        }

        if (duration >= sampleDuration)
        {
            if (displayMode == DisplayMode.FPS)
            {
                display.SetText(
                    "FPS\n{0:0}\n{1:0}\n{2:0}",
                    1.0f / bestDuration,
                    frames / duration,
                    1.0f / worstDuration
                );
            }
            else
            {
                display.SetText(
                    "FPS\n{0:1}\n{1:1}\n{2:1}",
                    1000.0f * bestDuration,
                    1000.0f * duration / frames,
                    1000.0f * worstDuration
                );
            }

            frames = 0;
            duration = 0.0f;
            bestDuration = float.MaxValue;
            worstDuration = 0.0f;
        }
    }

}
