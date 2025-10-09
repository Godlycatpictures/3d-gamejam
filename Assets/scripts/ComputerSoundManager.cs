using UnityEngine;

public class ComputerSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public void EnableComputerSounds()
    {
        audioSource.mute = false;
    }
}
