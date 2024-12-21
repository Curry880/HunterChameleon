using UnityEngine;

public class SeManager : MonoBehaviour
{
    private AudioSource audioSource;

    public static SeManager Instance { get; private set; }

    void Awake()
    {
        // �V���O���g���̏�����
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // AudioSource �R���|�[�l���g�̎擾
        audioSource = GetComponent<AudioSource>();
    }

    // �w�肳�ꂽ���ʉ����Đ�
    public void PlaySE(AudioClip clip)
    {
        // AudioClip �� null �̏ꍇ�͌x����\��
        if (clip == null)
        {
            Debug.LogWarning("Attempted to play a null AudioClip.");
            return;
        }
        audioSource.PlayOneShot(clip);
    }
}