using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;

    private void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.z = Target.position.z;
        transform.position = transformPosition;
    }
}
