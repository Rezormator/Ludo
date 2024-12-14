using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip button, diceRolling, piece, win;

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlayButtonSound()
    {
        source.PlayOneShot(button);
    }
}
