using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private bool isMuted = false;

    [SerializeField] private AudioSource GameMusic;  

    private void Awake()
    {
        DontDestroyOnLoad(this);

        GameMusic = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (!isMuted)
        {
            GameMusic.Play();
        }
    }

    private void Update() // Simple mute function for now.
    {
        CheckMute();
    }

    private void CheckMute()
    {
        if (isMuted)
        {
            GameMusic.volume = 0;
        }
        else
        {
            GameMusic.volume = 1;
        }
    }
    public void MuteAndUnMute()
    {
        if (isMuted)
        {
            isMuted = false;
        }else if (!isMuted)
        {
            isMuted = true;
        }
    }
}
