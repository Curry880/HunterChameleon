using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] // Button コンポーネントを必須化
public class SceneTransitionButton : MonoBehaviour
{
    [SerializeField] private SceneAsset sceneAsset; // 遷移先シーン
    [SerializeField] private AudioClip se;
    [SerializeField] private bool playSE = true;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
    }

    public void LoadScene()
    {
        // SEを鳴らすかどうかをチェック
        if (playSE)
        {
            SeManager.Instance.PlaySE(se);
        }

        // シーン遷移を実行
        SceneManager.LoadScene(sceneAsset.name);
    }
}