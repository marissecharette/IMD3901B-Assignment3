using UnityEngine;
using Unity.Netcode;

public class FollowParent : NetworkBehaviour
{
    public Transform parentTransform;

    // Position
    private Vector3 localPos;
    // Offset
    public Vector3 localOffset = Vector3.zero;

    private bool isFollowing = false;

    void Update()
    {
        if (!isFollowing || parentTransform == null)
        {
            return;
        }

        // New position based on local offset
        transform.position = parentTransform.TransformPoint(localPos + localOffset);
    }

    // This is called when the item is picked up, so it follows the parent's position
    public void StartFollowing(Transform parent)
    {
        parentTransform = parent;
        localPos = parentTransform.InverseTransformPoint(transform.position);
        isFollowing = true;
    }

    // Call this when throwing so the item stops following the parent
    public void StopFollowing()
    {
        isFollowing = false;
        parentTransform = null;
    }
}