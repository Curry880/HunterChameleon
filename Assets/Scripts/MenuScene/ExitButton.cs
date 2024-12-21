using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private void Start()
    {
        // WebGL環境ではボタンを無効化する
        DisableForWebGL();
    }

    public void OnClicked()
    {
        // エディタまたは非WebGL環境でアプリを終了する処理を呼び出す
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
