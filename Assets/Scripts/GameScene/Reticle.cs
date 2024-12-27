using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Reticle : MonoBehaviour
{
    [SerializeField]
    private TongueController tongue;

    [SerializeField] TextMeshProUGUI debugText;

    [System.NonSerialized]
    public int triggerNum;

    private Vector2 preMousePosition;
    private bool useMouse = true;

    private int gamepadSensitivity;
    private int mouseSensitivity;
    private List<SpriteRenderer> reticleSpriteRenderers = new List<SpriteRenderer>();

    private void Awake()
    {
        Debug.Log(reticleSpriteRenderers);
        Debug.Log(GetComponent<SpriteRenderer>());
        reticleSpriteRenderers.Add(GetComponent<SpriteRenderer>());
        foreach (Transform child in transform)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                reticleSpriteRenderers.Add(spriteRenderer);
            }
        }
    }

    void Start()
    {
        this.Init();
        preMousePosition = (Mouse.current).position.ReadValue();
    }

    

    public void SetCursorVisibility(bool isVisible)
    {
        useMouse = isVisible;
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void Update()
    {

        if (Mouse.current != null)
        {
            var mouseInput = Mouse.current;
            var mouseLeftButton = mouseInput.leftButton;
            var mousePosition = mouseInput.position.ReadValue();

            

            if (useMouse)
            {
                if (preMousePosition != mousePosition)
                {
                    var cursorPosition = Camera.main.ScreenToWorldPoint(mouseInput.position.ReadValue());
                    this.transform.position = new Vector3(Mathf.Clamp(cursorPosition.x, -9.0f, 9.0f), Mathf.Clamp(cursorPosition.y, -3.5f, 5.0f), 1.0f);
                }
            }
            else
            {
                var cursorPosition = this.transform.position
                        + new Vector3(mouseInput.delta.ReadValue().x * 0.002f * mouseSensitivity, mouseInput.delta.ReadValue().y * 0.002f * mouseSensitivity, 0f);
                this.transform.position = new Vector3(Mathf.Clamp(cursorPosition.x, -9.0f, 9.0f), Mathf.Clamp(cursorPosition.y, -3.5f, 5.0f), 1.0f);
                
            }

            if (mouseLeftButton.wasPressedThisFrame)
            {
                this.trigger();
            }

            preMousePosition = mouseInput.position.ReadValue();
        }

        UpdateDebugText();
    }

    private void ProcessGamepadInput()
    {
        if (Gamepad.current == null) {  return; }
        var gamepadInput = Gamepad.current;
        var gamepadSouthButton = gamepadInput.buttonSouth;
        var gamepadDpad = gamepadInput.dpad.ReadValue();
        var gamepadLeftStick = gamepadInput.leftStick.ReadValue();

        if (gamepadDpad != Vector2.zero)
        {
            var currentPosition = this.transform.position;
            this.transform.position = new Vector3(Mathf.Clamp(currentPosition.x + gamepadDpad.x * gamepadSensitivity * 2 * Time.deltaTime, -9.0f, 9.0f), Mathf.Clamp(currentPosition.y + gamepadDpad.y * gamepadSensitivity * 2 * Time.deltaTime, -3.5f, 5.0f), 1.0f);
        }

        if (gamepadLeftStick != Vector2.zero)
        {
            var currentPosition = this.transform.localPosition;
            this.transform.localPosition = new Vector3(Mathf.Clamp(currentPosition.x + gamepadLeftStick.x * gamepadSensitivity * 2 * Time.deltaTime, -9.0f, 9.0f), Mathf.Clamp(currentPosition.y + gamepadLeftStick.y * gamepadSensitivity * 2 * Time.deltaTime, -3.5f, 5.0f), 1.0f);
        }

        if (gamepadSouthButton.wasPressedThisFrame)
        {
            this.trigger();
        }
    }

    // デバッグテキストの更新
    private void UpdateDebugText()
    {
        if (debugText != null)
        {
            debugText.text = Cursor.lockState.ToString();
        }
    }

    private void trigger ()
    {
        if (GameManager.isPlaying)
        {
            triggerNum++;
        }
        Vector3 temp = transform.position;
        temp.z = 0;
        tongue.Shoot(temp);
    }

    public void Init ()
    {
        gamepadSensitivity = ParameterManager.gamepadSensitivity;
        mouseSensitivity = ParameterManager.mouseSensitivity;
        foreach(SpriteRenderer reticleSpriteRenderer in reticleSpriteRenderers)
        {
            reticleSpriteRenderer.color = new Color32(
                (byte)ParameterManager.tongueColorRed,
                (byte)ParameterManager.tongueColorGreen,
                (byte)ParameterManager.tongueColorBlue,
                (byte)ParameterManager.tongueColorAlpha
            );
        }
    }
}
