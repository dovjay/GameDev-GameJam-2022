using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField] AudioClip characterClip;
    [SerializeField] float speedMultiplier = 0.5f;

    bool pitchDown = false;
    bool pitchUp = false;
    float pitchTarget;

    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (pitchDown) TurndownPitch();
        if (pitchUp) TurnupPitch();
    }

    public void PlayCharacterClip() {
        audioSource.clip = characterClip;
        audioSource.Play();
    }

    public void TurnPitch(string direction, float target) {
        pitchTarget = target;

        switch (direction) {
            case "down":
                pitchDown = true;
                TurndownPitch();
            break;
            case "up":
                pitchUp = true;
                TurnupPitch();
            break;
        }
    }

    private void TurndownPitch() {
        if (audioSource.pitch > pitchTarget) {
            audioSource.pitch -= Time.deltaTime * speedMultiplier;
            return;
        }

        audioSource.pitch = pitchTarget;
        pitchDown = false;
    }

    private void TurnupPitch() {
        if (audioSource.pitch < pitchTarget) {
            audioSource.pitch += Time.deltaTime * speedMultiplier;
            return;
        }

        audioSource.pitch = pitchTarget;
        pitchUp = false;
    }
}