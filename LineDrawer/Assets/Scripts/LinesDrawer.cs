using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesDrawer : MonoBehaviour
{
    [SerializeField]
    private Line linePrefab;
    [SerializeField]
    private LayerMask cantDrawLayer;

    [Space]
    [SerializeField]
    private Gradient lineColor;
    [SerializeField]
    private float linePointsMinDistance;
    [SerializeField]
    private float lineWidth;

    private Line CurrentLine { get; set; }
    private Camera MainCamera { get; set; }

    private int cantDrawLayerIndex { get; set; }

    private void Start()
    {
        MainCamera = Camera.main;
        cantDrawLayerIndex = LayerMask.NameToLayer("CantDraw");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeginDraw();
        }

        if (Input.GetMouseButton(0) && CurrentLine != null)
        {
            Draw();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndDraw();
        }
    }

    private void BeginDraw()
    {
        CurrentLine = Instantiate(linePrefab, transform);
        CurrentLine.SetPhysicsActive(false);
        CurrentLine.SetColor(lineColor);
        CurrentLine.SetPointsMinDistance(linePointsMinDistance);
        CurrentLine.SetLineWidth(lineWidth);
    }

    private void Draw()
    {
        Vector2 mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawLayer);

        if(hit)
        {
            EndDraw();
        }
        else
        {
            CurrentLine.AddPoint(mousePosition);
        }
    }

    private void EndDraw()
    {
        if (CurrentLine != null)
        {
            if (CurrentLine.PointsCount < 2)
            {
                Destroy(CurrentLine.gameObject);
            }
            else
            {
                CurrentLine.gameObject.layer = cantDrawLayerIndex;
                CurrentLine.SetPhysicsActive(true);
                CurrentLine = null;
            }
        }
    }
}
