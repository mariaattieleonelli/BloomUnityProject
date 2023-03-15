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
        //Se establece la acción
        this.onYes = onYes;
        //Muestra el mensaje relacionado a la acción
        promptText.text = message;
    }

    public void Answer(bool yes)
    {
        //Ejecuta la acción si se selecciona "sí"
        if (yes && onYes != null)
        {
            onYes();
        }

        //Reseteamos la acción
        onYes = null;

        //Desactiva el panel del prompt
        gameObject.SetActive(false);
    }
}
