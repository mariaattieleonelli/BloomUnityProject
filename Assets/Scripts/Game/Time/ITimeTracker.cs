using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Vamos a usar esta interfaz para que objetos que requieran interactuar con el paso del tiempo
//lo hagan de esta manera

public interface ITimeTracker
{
    void ClockUpdate(Timestamp timeStamp)
    {

    }
}
