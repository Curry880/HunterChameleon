using UnityEngine;

public class SeManager : MonoBehaviour
{
    private AudioSource audioSource;

    public static SeManager Instance { get; private set; }

    void Awake()
    {
        // シングルトンの初期化
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // AudioSource コンポーネントの取得
        audioSource = GetComponent<AudioSource>();
    }

    // 指定された効果音を再生
    public void PlaySE(AudioClip clip)
    {
        // AudioClip が null の場合は警告を表示
        if (clip == null)
        {
            Debug.LogWarning("Attempted to play a null AudioClip.");
            return;
        }
        audioSource.PlayOneShot(clip);
    }
}