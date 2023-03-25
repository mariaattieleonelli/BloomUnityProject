using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    //Id de la tierra en donde se plantó la planta
    int landId;

    //Trae la información de a qué planta crecerá lo que se plante
    SeedData seedToGrow;

    //Objetos que indican las etapas de crecimiento de la planta
    public GameObject seed;
    public GameObject wiltedPlant;
    private GameObject sprout;
    private GameObject harvestable;

    int growth;
    int maxGrowth;

    //Establecemos la "vida" de una planta, para cuando se seca
    int plantMaxHealth = Timestamp.HoursToMinutes(48);
    int plantHealth;

    public CropState cropState;

    //Inicializamos el game object de la planta cuando el jugador planta una semilla
    public void Plant(int landId, SeedData seedToGrow)
    {
        LoadCrop(landId, seedToGrow, CropState.SEED, 0, 0);
        LandManager.instance.RegisterCrop(landId, seedToGrow, cropState, growth, plantHealth);
    }

    public void LoadCrop(int landPlotId, SeedData seedToGrow, CropBehaviour.CropState cropState, int growth, int plantHealth)
    {
        this.landId = landPlotId;

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

        //Se establece el crecimiento y salud
        this.growth = growth;
        this.plantHealth = plantHealth;

        //Ponemos el estado inicial del cultivo
        SwitchState(cropState);
    }

    //Cuando se tenga el estado de watered, la planta debe crecer
    public void Grow()
    {
        //Aumentamos los "puntos" de crecimiento de la planta
        growth++;

        //Cuando crece significa que la regamos, así que aumenta su salud
        if(plantHealth < plantMaxHealth)
        {
            plantHealth++;
        }

        //El retoño saldrá cuando se tenga la mitad del tiempo de crecimiento total de la planta
        if(growth >= maxGrowth/2 && cropState == CropState.SEED)
        {
            SwitchState(CropState.SPROUT);
        }

        if (growth >= maxGrowth && cropState == CropState.SPROUT)
        {
            SwitchState(CropState.HARVESTABLE);
        }

        //Informamos los cambios en el crecimiento al land manager
        LandManager.instance.OnCropStateChange(landId, cropState, growth, plantHealth);
    }

    //Cuando no se riega va perdiendo vida
    public void Wither()
    {
        plantHealth--;
        //Si la vida es menor a 0, significa que murió
        if(plantHealth < 0 && cropState != CropState.SEED)
        {
            SwitchState(CropState.WILTED);
        }

        //Actualizamos los cambios en la muerte al land manager
        LandManager.instance.OnCropStateChange(landId, cropState, growth, plantHealth);
    }

    //Manejamos los cambios de estado de crecimiento
    void SwitchState(CropState stateToSwitch)
    {
        //Reseteamos todos los game objects a estado de inactivos
        seed.SetActive(false);
        wiltedPlant.SetActive(false);
        sprout.SetActive(false);
        harvestable.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.SEED:
                seed.SetActive(true);
                break;
            case CropState.SPROUT:
                sprout.SetActive(true);
                //Se le asigna máxima vida a la planta
                plantHealth = plantMaxHealth;
                break;
            case CropState.HARVESTABLE:
                harvestable.SetActive(true);
                
                //Quitamos el padre de la planta que nació, es decir, el prefab de crop que maneja los estados
                harvestable.transform.parent = null;
                RemoveCrop();
                break;
            case CropState.WILTED:
                wiltedPlant.SetActive(true);
                break;
        }

        //Ponemos el estado de la planta como el estado al que estamos cambiando
        cropState = stateToSwitch;
    }

    //Destruye el cultivo y des registra de la lista de cultivos
    public void RemoveCrop()
    {
        //Cuando se cultiva la planta, se elimina su información de la farm data
        LandManager.instance.DeregisterCrop(landId);
        //Destruimos el padre para quedarnos solamente con la planta
        Destroy(gameObject);
    }

    public enum CropState
    {
        SEED, SPROUT, HARVESTABLE, WILTED
    }
}
