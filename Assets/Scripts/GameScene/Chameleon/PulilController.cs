using UnityEngine;

public class PulilController : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;

    void LateUpdate()
    {
        float angle = GetAngle(transform.position, targetObject.transform.position);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle + 90);
    }
    private float GetAngle(Vector2 from, Vector2 to)
    {
        float dx = to.x - from.x;
        float dy = to.y - from.y;
        return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    }
}

