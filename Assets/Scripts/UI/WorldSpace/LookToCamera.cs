using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    [SerializeField] private bool useLookAt;
    [SerializeField] private bool canRotate = false;
    [SerializeField] private bool useGivenRotation;
    [SerializeField] private Quaternion givenRotation;

    private Quaternion _initialRot;

    private void Awake()
    {
        _initialRot = transform.rotation;
    }

    private void LateUpdate()
    {
        if(!canRotate) return;
        
        if (useLookAt)
        {
            transform.LookAt(Camera.main.transform.position);
        }
        else if (useGivenRotation)
        {
            transform.rotation = givenRotation;
        }
        else
        {
            var dirToLookAtTarget = Vector3.zero;
            dirToLookAtTarget = transform.position - Camera.main.transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dirToLookAtTarget);
            lookRot.x = _initialRot.x;
            lookRot.z = 0f;
            transform.rotation = lookRot;
        }
    }
}
