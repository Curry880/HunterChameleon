using System.Collections;
using UnityEngine;

public class TongueController : MonoBehaviour
{
    [SerializeField]
    private AudioClip shootSound;

    private AudioSource audioSource;
    SpriteRenderer tongueSpriteRenderer;
    private int tongueSpeed;
    private bool isShooting;
    private float scaleY;
    private TongueState tongueState;

    private float InitialScaleY;

    private enum TongueState
    {
        Extending = 1, // 伸びている状態
        Retracting = -1 // 縮んでいる状態
    }

    private void Awake()
    {
        InitialScaleY = transform.localScale.y;
        audioSource = GetComponent<AudioSource>();
        tongueSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Init();
    }

    public IEnumerator Shoot(Vector3 triggeredPosition)
    {
        if (isShooting) { yield break; }

        PrepareShot(triggeredPosition);
        audioSource.PlayOneShot(shootSound);

        float distance = Vector3.Distance(transform.position, triggeredPosition);
          
        while (UpdateTongueScale(distance))    
        {
            yield return null;
        }
    }

    private void PrepareShot(Vector3 triggeredPosition)
    {
        isShooting = true;
        scaleY = InitialScaleY;
        tongueState = TongueState.Extending;

        float angle = GetAngle(transform.position, triggeredPosition);
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private bool UpdateTongueScale(float distance)
    {
        // スケールを更新
        scaleY += (int)tongueState * tongueSpeed * Time.deltaTime;
        Vector3 localScale;

        // 到達判定
        if (tongueState == TongueState.Extending && scaleY >= distance)
        {
            tongueState = TongueState.Retracting;
            scaleY = distance; // 距離を超えないようにする
        }
        else if (tongueState == TongueState.Retracting && scaleY <= InitialScaleY)
        {
            isShooting = false;
            scaleY = InitialScaleY; // 最小スケールを超えないようにする
            localScale = transform.localScale;
            localScale.y = scaleY;
            transform.localScale = localScale;
            return false;
        }

        // スケール更新
        localScale = transform.localScale;
        localScale.y = scaleY;
        transform.localScale = localScale;
        return true;
    }

    private float GetAngle(Vector2 from, Vector2 to)
    {
        float dx = to.x - from.x;
        float dy = to.y - from.y;
        return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    }

    public void Init()
    {
        tongueSpriteRenderer.color = new Color32(
            (byte)ParameterManager.tongueColorRed, 
            (byte)ParameterManager.tongueColorGreen, 
            (byte)ParameterManager.tongueColorBlue,
            (byte)ParameterManager.tongueColorAlpha
        );
        tongueSpeed = ParameterManager.tongueSpeed * 10;
        isShooting = false;
        scaleY = InitialScaleY;
    }
}
