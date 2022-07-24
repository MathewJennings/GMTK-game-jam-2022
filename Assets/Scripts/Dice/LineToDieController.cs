using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToDieController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform startTransform;
    private Transform endTransform;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    public void setUpLine(Transform startTransform, Transform endTransform)
    {
        lineRenderer.positionCount = 4; // Start, end, and two more points to draw an arrow head
        this.startTransform = startTransform;
        this.endTransform = endTransform;
    }

    public void removeLine()
    {
        lineRenderer.positionCount = 0;
    }

    private void FixedUpdate()
    {
        if (lineRenderer.positionCount == 0)
        {
            return;
        }
        Vector3 startPosition = startTransform.position;
        float currentVelocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        Vector3 endPosition = endTransform.position;
        if (Physics.Linecast(startPosition, endTransform.position, out RaycastHit hit, LayerMask.GetMask("Dice")))
        {
            endPosition = hit.point;
        }
        Vector3 startToEnd = endPosition - startPosition;
        Vector3 point0 = startPosition;
        Vector3 point1 = startPosition + startToEnd*0.95f;
        Vector3 point2 = startPosition + startToEnd * 0.951f;
        Vector3 point3 = endPosition;
        lineRenderer.SetPosition(0, point0);
        lineRenderer.SetPosition(1, point1);
        lineRenderer.SetPosition(2, point2);
        lineRenderer.SetPosition(3, point3);
    }
}
