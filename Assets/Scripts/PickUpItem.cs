using UnityEngine;
using System;
using System.Collections.Generic;

public class PickUpItem : MonoBehaviour
{
    [SerializeField, Tooltip("The speed that object rotates at.")]
    private float _rotationSpeed = 500.0f;
    public static int s_objectsCollected = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // grab the currennt rotatation, increment it, and re-apply it.
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y += (_rotationSpeed * Time.deltaTime);
        transform.eulerAngles = newRotation;
    }

    public void onPickedUp(GameObject whoPickedUp)
    {
        // show the collection count in the console window.
        s_objectsCollected++;
        Debug.Log( s_objectsCollected + "items picked up." );

        // destroy this object.
        Destroy(gameObject);
    }
}