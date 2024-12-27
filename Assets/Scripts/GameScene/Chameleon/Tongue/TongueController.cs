using UnityEngine;

public class TongueController : MonoBehaviour
{
    private TonguePhysicsController physicsController;
    private TongueVisualController visualController;

    private void Awake()
    {
        physicsController = GetComponent<TonguePhysicsController>();
        visualController = GetComponent<TongueVisualController>();
    }

    void Start()
    {
        Init();
    }

    public void Shoot(Vector3 triggeredPosition)
    {
        StartCoroutine(physicsController.Shoot(triggeredPosition));
        visualController.PlayShootSound();
    }

    public void Init()
    {
        visualController.Init();
        physicsController.Init();
    }
}
