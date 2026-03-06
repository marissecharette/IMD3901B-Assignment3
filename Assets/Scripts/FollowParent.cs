using UnityEngine;

public class FollowParent : MonoBehaviour
{
    public Transform parentTransform;

    // Position
    private Vector3 localPos;
    // Offset
    public Vector3 localOffset = Vector3.zero;

    private bool isFollowing = false;

    void Update()
    {
        if (!isFollowing || parentTransform == null) return;

        // Compute new world position based on stored local offset
        transform.position = parentTransform.TransformPoint(localPos + localOffset);
    }

    // Call this when picking up
    public void StartFollowing(Transform parent)
    {
        parentTransform = parent;
        localPos = parentTransform.InverseTransformPoint(transform.position);
        isFollowing = true;
    }

    // Call this when throwing so the 
    public void StopFollowing()
    {
        isFollowing = false;
        parentTransform = null;
    }
}