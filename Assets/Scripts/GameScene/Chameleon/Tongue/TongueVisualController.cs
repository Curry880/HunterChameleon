using UnityEngine;

public class TongueVisualController : MonoBehaviour
{
    [SerializeField]
    private AudioClip shootSound;

    private AudioSource audioSource;
    private SpriteRenderer tongueSpriteRenderer;

    public void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }

    public void Init()
    {
        audioSource = GetComponent<AudioSource>();
        tongueSpriteRenderer = GetComponent<SpriteRenderer>();
        SetColor();
    }

    public void SetColor()
    {
        tongueSpriteRenderer.color = new Color32(
            (byte)ParameterManager.tongueColorRed,
            (byte)ParameterManager.tongueColorGreen,
            (byte)ParameterManager.tongueColorBlue,
            (byte)ParameterManager.tongueColorAlpha
        );
    }
}
