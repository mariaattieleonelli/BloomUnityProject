using UnityEngine;

[System.Serializable]
public class Timestamp
{
    public int year;
    public Season season;
    public int day;
    public int hour;
    public int minute;

    //Constructor para setup de la clase de Timestamp
    public Timestamp(int year, Season season, int day, int hour, int min)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.minute = min;
        this.hour = hour;
    }

    //Creamos una instancia de un time stamp existente
    public Timestamp(Timestamp timeStamp)
    {
        this.year = timeStamp.year;
        this.season = timeStamp.season;
        this.day = timeStamp.day;
        this.minute = timeStamp.minute;
        this.hour = timeStamp.hour;
    }

    //Incremente el tiempo en 1 minuto
    public void UpdateClock()
    {
        minute++;

        //Incremento de horas
        if(minute >= 60)
        {
            //Si ya pasaron 60 "minutos", se resetea ese conteo y se añade una hora
            minute = 0;
            hour++;
        }

        //Incremento de días
        if(hour >= 24)
        {
            //Si ya pasaron 24 "horas", se resetea ese conteo y se añade un día
            hour = 0;
            day++;
        }

        //Incremento de meses. Cada mes es una estación
        if(day > 30)
        {
            //Si pasaron 30 "días", regresamos el día a 1 y pasamos al siguiente mes
            day = 1;
            //Si es invierno el siguiente mes debe ser primavera, pero en el enum no se marca así
            if(season == Season.WINTER)
            {
                season = Season.SPRING;
                //Aquí inicia un nuevo año, tras invierno
                year++;
            }
            else
            {
                season++;
            }
        }
    }

    //Función que nos permite saber qué día de la semana es
    public DayOfTheWeek GetDayOfTheWeek()
    {
        //Convertimos el total de tiempo pasado a días
        int daysPassed = YearsToDays(year) * SeasonsToDays(season) + day;

        //Tomamos el sobrante de dividir los días que han pasado entre 7
        int dayIndex = daysPassed % 7;

        //Convertimos a día de la semana
        return (DayOfTheWeek)dayIndex;
    }

    //Convertimos horas en minutos
    public static int HoursToMinutes(int hour)
    {
        //60 minutos equivalen a una hora
        return hour * 60;
    }

    //Convertimos días a horas
    public static int DaysToHours(int days)
    {
        //24 horas equivalen a un día
        return days * 24;
    }

    //Convertimos meses a días
    public static int SeasonsToDays(Season season)
    {
        int seasonIndex = (int)season;
        return seasonIndex * 30;
    }

    //Convertimos años a días
    public static int YearsToDays(int years)
    {
        return years * 4 * 30;
    }

    //Función que determina la diferencia de horas entre dos timestamps
    public static int CompareTimestamps(Timestamp timestamp1, Timestamp timestamp2)
    {
        //Convertimos timestamp a horas
        int timestamp1Hours = DaysToHours(YearsToDays(timestamp1.year)) + DaysToHours(SeasonsToDays(timestamp1.season)) + DaysToHours(timestamp1.day) + timestamp1.hour;
        int timestamp2Hours = DaysToHours(YearsToDays(timestamp2.year)) + DaysToHours(SeasonsToDays(timestamp2.season)) + DaysToHours(timestamp2.day) + timestamp2.hour;
        int hoursDifference = timestamp2Hours - timestamp1Hours;
        return Mathf.Abs(hoursDifference);
    }

    public enum Season
    {
        SPRING,
        SUMMER,
        FALL,
        WINTER
    }

    public enum DayOfTheWeek
    {
        //Empezamos el enum por sábado para que el día 1 de la semana sea domingo
        SABADO,
        DOMINGO,
        LUNES,
        MARTES,
        MIERCOLES,
        JUEVES,
        VIERNES
    }
}
