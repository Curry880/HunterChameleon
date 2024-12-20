using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] // Button �R���|�[�l���g��K�{��
public class PauseButton : MonoBehaviour
{
    private Button pauseButton;

    private bool isPausing;

    [SerializeField]
    private Sprite[] buttonSprites = new Sprite[2];

    [SerializeField]
    private GameObject parameterPanel;

    [SerializeField]
    private Score score;

    [SerializeField]
    private TargetManager targetManager;
    [SerializeField]
    private Tongue tongue;
    [SerializeField]
    private Reticle reticle;

    void Awake()
    {
        isPausing = true;
        pauseButton = GetComponent<Button>();
        pauseButton.onClick.AddListener(OnClicked);
    }

    void OnClicked () 
    {
        if(isPausing)
        {
            Init();
            TimeKeeper.isPlaying = true;
            isPausing = false;
            this.gameObject.GetComponent<Image>().sprite = buttonSprites[1];
            parameterPanel.SetActive(false);
            targetManager.StartSpawn();

            reticle.UseCursor(false);
        }
        else
        {
            TimeKeeper.isPlaying = false;
            isPausing = true;
            this.gameObject.GetComponent<Image>().sprite = buttonSprites[0];
            parameterPanel.SetActive(true);
            targetManager.StopSpawn();

            reticle.UseCursor(true);
        }
    }

    //=============UI���g���ꍇ�͍폜===============
    public void OnClickSprite()
    {
        if (isPausing)
        {
            Init();
            TimeKeeper.isPlaying = true;
            isPausing = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprites[1];
            parameterPanel.SetActive(false);
            targetManager.StartSpawn();

            reticle.UseCursor(false);
        }
        else
        {
            TimeKeeper.isPlaying = false;
            isPausing = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprites[0];
            parameterPanel.SetActive(true);
            targetManager.StopSpawn();

            reticle.UseCursor(true);
        }
    }
    //================================================

    private void Init ()
    {
        score.Init();
        targetManager.Init();
        tongue.Init();
        reticle.Init();
    }
}
