using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private void Start()
    {
        // WebGL���ł̓{�^���𖳌�������
        DisableForWebGL();
    }

    public void OnClicked()
    {
        // �G�f�B�^�܂��͔�WebGL���ŃA�v�����I�����鏈�����Ăяo��
        HandleExit();
    }

    private void DisableForWebGL()
    {
        #if UNITY_WEBGL
            gameObject.SetActive(false);
        #endif
    }

    private void HandleExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif !UNITY_WEBGL
            Application.Quit();
        #endif
    }
}
