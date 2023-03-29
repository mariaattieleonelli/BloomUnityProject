using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance { get; private set; }

    public GameObject tutorialEndPanel;
    public GameObject tutorialPanel;

    public int grownAloe;
    public int grownChamomille;
    public int grownCactus;

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

    void Start()
    {
        //Si es una nueva partida, activamos el tutorial
        if (PlayerStats.isNewGame == false)
        {
            tutorialPanel.SetActive(false);
        }   
    }

    private void Update()
    {
        if (grownAloe == 1 & grownChamomille == 1 & grownCactus == 1)
        {
            ChallengeComplete();
        }
    }

    //Cuando ya se plantaron las especies solicitadas
    private void ChallengeComplete()
    {
        tutorialEndPanel.SetActive(true);
    }
}
