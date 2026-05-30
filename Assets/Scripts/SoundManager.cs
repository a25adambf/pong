using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Clips (auto-generated if empty)")]
    [SerializeField] AudioClip paddleHitClip;
    [SerializeField] AudioClip goalClip;
    [SerializeField] AudioClip gameStartClip;
    [SerializeField] AudioClip wallBounceClip;
    [SerializeField] AudioClip buttonClickClip;

    [Header("Volume")]
    [Range(0f, 1f)]
    [SerializeField] float masterVolume = 0.5f;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Generate procedural audio clips if none were assigned
        if (paddleHitClip == null)
            paddleHitClip = GenerateTone(0.3f, 440f, 0.1f);
        if (goalClip == null)
            goalClip = GenerateTone(0.5f, 220f, 0.2f);
        if (gameStartClip == null)
            gameStartClip = GenerateTone(0.4f, 660f, 0.15f);
        if (wallBounceClip == null)
            wallBounceClip = GenerateTone(0.2f, 330f, 0.08f);
        if (buttonClickClip == null)
            buttonClickClip = GenerateTone(0.15f, 880f, 0.05f);
    }

    public void PlayPaddleHit()
    {
        PlaySound(paddleHitClip);
    }

    public void PlayGoal()
    {
        PlaySound(goalClip);
    }

    public void PlayGameStart()
    {
        PlaySound(gameStartClip);
    }

    public void PlayWallBounce()
    {
        PlaySound(wallBounceClip);
    }

    public void PlayButtonClick()
    {
        PlaySound(buttonClickClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, masterVolume);
        }
    }

    /// <summary>
    /// Generates a procedural AudioClip with a sine wave and envelope.
    /// </summary>
    private AudioClip GenerateTone(float duration, float frequency, float fadeOutStart)
    {
        int sampleRate = 44100;
        int sampleCount = Mathf.CeilToInt(duration * sampleRate);
        float[] samples = new float[sampleCount];

        for (int i = 0; i < sampleCount; i++)
        {
            float t = (float)i / sampleRate;
            float normalizedTime = (float)i / sampleCount;

            // Sine wave
            float sample = Mathf.Sin(2f * Mathf.PI * frequency * t);

            // Fade out envelope
            float envelope = 1f;
            if (normalizedTime > fadeOutStart)
            {
                envelope = 1f - ((normalizedTime - fadeOutStart) / (1f - fadeOutStart));
            }

            // Start fade in (avoid click)
            if (normalizedTime < 0.02f)
            {
                envelope *= normalizedTime / 0.02f;
            }

            samples[i] = sample * envelope * 0.5f;
        }

        AudioClip clip = AudioClip.Create("ProceduralTone", sampleCount, 1, sampleRate, false);
        clip.SetData(samples, 0);
        return clip;
    }
}