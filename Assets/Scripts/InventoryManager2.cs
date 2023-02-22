using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager2 : MonoBehaviour
{
    public static InventoryManager2 instance { get; private set; }

    //Espacios de inventario para herramientas y herramienta en mano
    public ItemData[] tools = new ItemData[2];
    public ItemData equipedTool = null;

    //Espacios de inventario para items e item en mano
    public ItemData[] items = new ItemData[16];
    public ItemData[] equipedItem = null;

    private void Awake()
    {
        //si hay otra instancia, destruir la extra
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
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
