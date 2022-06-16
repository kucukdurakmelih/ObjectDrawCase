using System;
using Unity.Mathematics;
using UnityEngine;


public class DrawManager : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private Line linePrefab;

    private Line currentLine;
    [SerializeField] private float minPointDistance;
    private Vector2 prevDrawPoint;
    private void Start()
    {
        mainCamera = Camera.main;
    }


    private void Update()
    {
        DrawLine();
    }

    private void DrawLine()
    {
        var mousePos = Input.mousePosition;
        var mouseWorldPoint = (Vector2) mainCamera.ScreenToWorldPoint(mousePos);
        
        if (Input.GetMouseButtonDown(0))
        {
            var newLine = Instantiate(linePrefab, mouseWorldPoint, quaternion.identity);
            currentLine = newLine;
            prevDrawPoint = mouseWorldPoint;
            currentLine.Draw(mouseWorldPoint);

        }
        
        else if (Input.GetMouseButtonUp(0))
        {
           currentLine.CheckLine();
        }
        
        else if (Input.GetMouseButton(0))
        {
            var distance = Vector2.Distance(prevDrawPoint, mouseWorldPoint);
        
            if (distance >= minPointDistance)
            {
                prevDrawPoint = mouseWorldPoint;
                currentLine.Draw(mouseWorldPoint);
            }
        }
    }
}