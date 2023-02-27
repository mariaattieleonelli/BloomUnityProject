using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; private set; }

    //Controla el avance de minutos en el juego, equivalente a 1 segundo real
    public float timeScale = 1.0f;

    public Transform sunTransform;

    [Header("Day and night cycle")]
    [SerializeField]
    Timestamp timeStamp;

    //Lista de objetos a los que hay que informarles de los cambios en el tiempo
    List<ITimeTracker> listeners;

    private void Awake()
    {
        //si hay otra instancia, destruir la extra
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    //Inicializamos el time stamp
    private void Start()
    {
        timeStamp = new Timestamp(0, Timestamp.Season.SPRING, 1, 6, 0);
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1/timeScale);
            Tick();
        }  
    }

    //Equivalente a un segundo del tiempo dentro del juego
    public void Tick()
    {
        timeStamp.UpdateClock();

        //Informamos a los listeners sobre el nuevo estado del timeStamp
        foreach(ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timeStamp);
        }

        UpdateSunMovement();

    }

    //Ciclo de d�a y noche
    void UpdateSunMovement()
    {
        //Convertimos el tiempo actual a minutos
        int timeInMinutes = Timestamp.HoursToMinutes(timeStamp.hour) + timeStamp.minute;

        //Movimiento del sol; 15 grados cada hora, 0.25 en un minuto
        //A media noche 00:00, el �ngulo del sol es de -90
        float sunAngle = 0.25f * timeInMinutes - 90;

        //Aplicamos el �ngulo calculado al transform de la luz direccional (sol)
        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }

    //Manejo de los listeners

    //A�ade objetos a la lista de objetos que quieren saber sobre timeStamp
    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }
}
