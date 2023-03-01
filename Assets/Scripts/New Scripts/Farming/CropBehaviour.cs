using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    //Trae la información de a qué planta crecerá lo que se plante
    SeedData seedToGrow;

    //Objetos que indican las etapas de crecimiento de la planta
    public GameObject seed;
    private GameObject sprout;
    private GameObject harvestable;

    int growth;
    int maxGrowth;

    public CropState cropState;

    //Inicializamos el game object de la planta cuando el jugador planta una semilla
    public void Plant(SeedData seedToGrow)
    {
        //Guarda la información de las semillas
        this.seedToGrow = seedToGrow;

        //Instanciamos los game objects para SPROUT y HARVESTABLE
        sprout = Instantiate(seedToGrow.sprout, transform);

        //Accesamos a la planta que crece de la semilla que se plante
        ItemData objectToGrow = seedToGrow.objectToGrow;

        //Instanciamos la planta que ya se puede cosechar
        harvestable = Instantiate(objectToGrow.gameModel, transform);

        //Convertimos los días a crecer de la planta a minutos, ya que se actualiza cada minuto el campo
        int hoursToGrow = Timestamp.DaysToHours(seedToGrow.daysToGrow);
        maxGrowth = Timestamp.HoursToMinutes(hoursToGrow);

        //Ponemos el estado inicial como el de seed
        SwitchState(CropState.SEED);
    }

    //Cuando se tenga el estado de watered, la planta debe crecer
    public void Grow()
    {
        //Aumentamos los "puntos" de crecimiento de la planta
        growth++;

        //El retoño saldrá cuando se tenga la mitad del tiempo de crecimiento total de la planta
        if(growth >= maxGrowth/2 && cropState == CropState.SEED)
        {
            SwitchState(CropState.SPROUT);
        }

        if (growth >= maxGrowth && cropState == CropState.SPROUT)
        {
            SwitchState(CropState.HARVESTABLE);
        }
    }

    //Manejamos los cambios de estado de crecimiento
    void SwitchState(CropState stateToSwitch)
    {
        //Reseteamos todos los game objects a estado de inactivos
        seed.SetActive(false);
        sprout.SetActive(false);
        harvestable.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.SEED:
                seed.SetActive(true);
                break;
            case CropState.SPROUT:
                sprout.SetActive(true);
                break;
            case CropState.HARVESTABLE:
                harvestable.SetActive(true);
                //Quitamos el padre de la planta que nació, es decir, el prefab de crop que maneja los estados
                harvestable.transform.parent = null;
                //Destruimos el padre para quedarnos solamente con la planta
                Destroy(gameObject);
                break;
        }

        //Ponemos el estado de la planta como el estado al que estamos cambiando
        cropState = stateToSwitch;
    }

    public enum CropState
    {
        SEED, SPROUT, HARVESTABLE
    }
}
