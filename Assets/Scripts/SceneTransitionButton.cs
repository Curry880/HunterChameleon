using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] // Button �R���|�[�l���g��K�{��
public class SceneTransitionButton : MonoBehaviour
{
    [SerializeField] private SceneAsset sceneAsset; // �J�ڐ�V�[��
    [SerializeField] private bool playSE = true;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
    }

    //========UI���g���ꍇ�폜===========
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tongue"))
        {
            LoadScene();
        }
    }
    //===================================

    public void LoadScene()
    {
        // SE��炷���ǂ������`�F�b�N
        if (playSE)
        {
            SeManager.Instance.PlaySE();
        }

        // �V�[���J�ڂ����s
        SceneManager.LoadScene(sceneAsset.name);
    }
}