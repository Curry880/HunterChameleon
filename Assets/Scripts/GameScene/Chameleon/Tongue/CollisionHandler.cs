using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private Score score;//シングルトン化
    [SerializeField]
    private PoolManager judgePool;
    [SerializeField]
    private Sprite[] judges = new Sprite[4];
    [SerializeField]
    private AudioClip hitSound, achieveSound;

    private AudioSource audioSource;
    private Vector3 shotPosition;

    [System.NonSerialized]
    public int hitNum;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fly") || collision.CompareTag("Apple"))
        {
            HandleCollision(collision);
        }
    }

    private void HandleCollision(Collider2D collision)
    {
        hitNum++;

        Vector3 collidedPosition = collision.transform.position;
        collidedPosition.z = 1.0f;

        GameObject judge = ShowJudge(collidedPosition);

        if (collision.CompareTag("Fly"))
        {
            HandleFlyCollision(judge, collidedPosition);
            score.AddScore(300);
        }
        else if (collision.CompareTag("Apple"))
        {
            HandleAppleCollision(judge);
            score.AddScore(500);
        }

        collision.GetComponent<Destroyer>().pool.ReleaseToPool(collision.gameObject);
    }

    private void HandleFlyCollision(GameObject judge, Vector3 collidedPosition)
    {
        float distance = Vector3.Distance(shotPosition, collidedPosition);

        if (distance < 1.0f)
        {
            judge.GetComponent<SpriteRenderer>().sprite = judges[1];
            audioSource.PlayOneShot(achieveSound);
        }
        else if (distance < 5.0f)
        {
            judge.GetComponent<SpriteRenderer>().sprite = judges[2];
        }
        else
        {
            judge.GetComponent<SpriteRenderer>().sprite = judges[3];
        }

        audioSource.PlayOneShot(hitSound);
    }

    private void HandleAppleCollision(GameObject judge)
    {
        judge.GetComponent<SpriteRenderer>().sprite = judges[0];
        audioSource.PlayOneShot(hitSound);
        audioSource.PlayOneShot(achieveSound);
    }

    private GameObject ShowJudge(Vector3 position)
    {
        GameObject judge = judgePool.GetFromPool();
        judge.transform.position = position;
        Destroyer destroyer = judge.GetComponent<Destroyer>();
        destroyer.pool = judgePool;
        destroyer.StartDestroyTimer1sec();
        return judge;
    }
}
