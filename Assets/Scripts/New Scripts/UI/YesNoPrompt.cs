using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YesNoPrompt : MonoBehaviour
{   
    [SerializeField]
    TextMeshProUGUI promptText;
    Action onYes = null;

    public void CreatePrompt(string message, Action onYes)
    {
        //Se establece la acci�n
        this.onYes = onYes;
        //Muestra el mensaje relacionado a la acci�n
        promptText.text = message;
    }

    public void Answer(bool yes)
    {
        //Ejecuta la acci�n si se selecciona "s�"
        if (yes && onYes != null)
        {
            onYes();
        }

        //Reseteamos la acci�n
        onYes = null;

        //Desactiva el panel del prompt
        gameObject.SetActive(false);
    }
}
