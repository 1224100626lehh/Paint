using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Draw : MonoBehaviour
{
    //public: variables de entrada

    public InputActionReference drawInput;
    public Transform drawPositionSource;
    public float lineWidth = 0.03f;
    public Material lineMaterial;
    public float distanceThreshold = 0.05f;

    // private : elementos helpers
    private List<Vector3> currentLinePositions = new List<Vector3>();
    private XRController controller;
    private bool isDrawing = false;
    private LineRenderer currentLine;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<XRController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener el valor del trigger para verificar si esta sibujando
        float triggerValue = drawInput.action.ReadValue<float>();
        bool isPressed = triggerValue > 0.1f;

        // Si no esta dibujando y presiono el boton

        if(!isDrawing && isPressed)
        {
            // Inicia pintando la linea
            StartDrawing();
        }
        else if(isDrawing && isPressed)// Si esta dibujando y no presiona el boton
        {
            //  Detener la linea
            StopDrawing();
        }
        else if(isDrawing && isPressed)// Si esta dibujando y presionando el boton
        {
            // Actualizar la Linea
            UpdateDrawing();
        }
    }

    public void SetLineMaterial(Material mat)
    {
        lineMaterial = mat;
    }
    void StartDrawing()
    {
        // Pasar la variable isDrawing a true
        isDrawing = true;
        // Crear una Linea 
        // Generar un objeto GameObject
        GameObject lineGameObject = new GameObject("Line");
        currentLine = lineGameObject.AddComponent<LineRenderer>();

        //Forzar las coordenadas globales
        currentLine.useWorldSpace = true;

       // Configurar el grosor de la linea
       currentLine.startWidth = lineWidth;
       currentLine.endWidth = lineWidth;

       //Actualiza la linea
        UpdateLine();

    }

    void UpdateLine()
    {
        currentLinePositions.Add( drawPositionSource.position );
        currentLine.positionCount = currentLinePositions.Count;
        currentLine.SetPositions(currentLinePositions.ToArray());

        currentLine.material = lineMaterial;
        currentLine.startWidth = lineWidth;
    }

    void StopDrawing()
    {
        isDrawing=false;
        currentLinePositions.Clear();
        currentLine = null;
    }

    void UpdateDrawing()
    {
        //Check if we have a line
        if (currentLine || currentLinePositions.Count == 0 )
        return;
            Vector3 lastSetPosition = currentLinePositions [currentLinePositions.Count - 1];
            if(Vector3.Distance (lastSetPosition, drawPositionSource.position) > distanceThreshold)
        {
            UpdateLine();
        }
        
    }
}
