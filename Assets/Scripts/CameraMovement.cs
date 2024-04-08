using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x,transform.position.y,transform.position.z), ref _currentVelocity, smoothTime);
        Vector3 targetDirection = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(0, targetDirection.y, targetDirection.z), Vector3.up);

    }
}
