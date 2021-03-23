using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private EdgeCollider2D edgeCollider;
    [SerializeField]
    private Rigidbody2D rigidBody;

    private List<Vector2> PointList { get; } = new List<Vector2>();
    public int PointsCount => PointList.Count;

    private float PointsMinDistance { get; set; } = 0.1f;
    private float CircleColliderRadius { get; set; }
    
    public Vector2 LastPoint => PointList.LastOrDefault();

    public void AddPoint(Vector2 newPoint)
    {
        if(PointsCount >= 1 && Vector2.Distance(LastPoint, newPoint) < PointsMinDistance)
        {
            return;
        }

        PointList.Add(newPoint);

        CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = CircleColliderRadius;

        lineRenderer.positionCount = PointsCount;
        lineRenderer.SetPosition(PointsCount - 1, newPoint);
        if(PointsCount > 1)
        {
            edgeCollider.SetPoints(PointList);
        }
    }

    public void SetPhysicsActive(bool active)
    {
        rigidBody.isKinematic = !active;
    }

    public void SetColor(Gradient colorGradient)
    {
        lineRenderer.colorGradient = colorGradient;
    }

    public void SetPointsMinDistance(float distance)
    {
        PointsMinDistance = distance;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        edgeCollider.edgeRadius = width / 2f;
        CircleColliderRadius = width / 2f;
    }
}
