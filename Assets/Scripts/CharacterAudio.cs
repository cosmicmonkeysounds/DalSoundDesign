using UnityEngine;
using UnityEditor;

public class CharacterAudio : MonoBehaviour
{
    [Header ("Audio Sources")]
    [SerializeField] AudioSource footstepsAudioSource = null;
    [SerializeField] AudioSource jumpingAudioSource = null;
    [SerializeField] AudioSource characterVoiceSource = null;


    [Header("Audio Clips")]
    [SerializeField] AudioClip[] softSteps = null;
    [SerializeField] AudioClip[] hardSteps = null;
    [SerializeField] AudioClip[] softFootLandings = null;
    [SerializeField] AudioClip[] hardFootLandings = null;
    [SerializeField] AudioClip[] softFootJumps = null;
    [SerializeField] AudioClip[] hardFootJumps = null;
    [SerializeField] AudioClip[] landingVoiceEmotes = null;
    [SerializeField] AudioClip[] jumpVoiceEmotes = null;

 


    [Header ("Variation Parameters")]

    [SerializeField, Tooltip ("Min/Max values represent the offset variation in pitch as a multiplicative number: 0.5 means an octave down, 2 means an octave up.")] 
    private Vector2 footstepPitchVariationRange = new Vector2 (0.0f, 0.0f);


    [Header("Steps")]
    [SerializeField] float stepsTimeGap = 1f;

    private float stepsTimer;

    public void PlaySteps(GroundType groundType, float speedNormalized)
    {
        if (groundType == GroundType.None)
            return;

        stepsTimer += Time.fixedDeltaTime * speedNormalized;

        if (stepsTimer >= stepsTimeGap)
        {
            var steps = groundType == GroundType.Hard ? hardSteps : softSteps;

            if (steps.Length == 0) return;

            int index = Random.Range(0, steps.Length);

            footstepsAudioSource.pitch = Random.Range (footstepPitchVariationRange.x, footstepPitchVariationRange.y);//.Remap (-1200.0f, 1200.0f, 0.5f, 2.0f);
            footstepsAudioSource.PlayOneShot(steps[index]);

            stepsTimer = 0;
        }
    }

    public void PlayJump (GroundType groundType)
    {
        AudioClip[] footstepContainer = groundType == GroundType.Hard ? hardFootJumps : softFootJumps;

        if (footstepContainer.Length == 0) return;

        jumpingAudioSource.PlayOneShot (footstepContainer [Random.Range (0, footstepContainer.Length)]);

        characterVoiceSource.PlayOneShot (jumpVoiceEmotes [Random.Range (0, jumpVoiceEmotes.Length)]);
    }

    public void PlayLanding(GroundType groundType)
    {
        AudioClip[] footstepContainer = groundType == GroundType.Hard ? hardFootLandings : softFootLandings;

        if (footstepContainer.Length == 0) return;

        jumpingAudioSource.PlayOneShot (footstepContainer [Random.Range (0, footstepContainer.Length)]);

        characterVoiceSource.PlayOneShot (landingVoiceEmotes [Random.Range (0, landingVoiceEmotes.Length)]);
    }
}
