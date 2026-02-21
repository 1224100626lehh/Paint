using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material newMaterial;

    private void OnTriggerEnter(Collider collider)
    {
        //Cambiar el color del pincel
        collider.GetComponent<Renderer>().material = newMaterial;

        //Cambiar la linea al color seleccionado

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
