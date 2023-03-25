using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsAppear : MonoBehaviour
{
    public GameObject shopObjects;
    public GameObject trashObjects;
    public GameObject treeObjects;
    public GameObject panelBlur;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            shopObjects.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            trashObjects.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            treeObjects.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            panelBlur.SetActive(false);
        }

    }
}
