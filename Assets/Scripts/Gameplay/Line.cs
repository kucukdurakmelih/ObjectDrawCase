using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    public void Draw(Vector2 pos)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, pos);
        EventManager.CheckGrid?.Invoke(pos);

    }

    public void CheckLine()
    {
        EventManager.CheckDrawing?.Invoke();
        Destroy(lineRenderer.gameObject);
        
    }
}
