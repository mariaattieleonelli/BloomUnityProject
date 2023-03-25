using UnityEngine;

public class PopUpUI : MonoBehaviour
{
    public GameObject waterFillUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            waterFillUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            waterFillUI.SetActive(false);
        }
    }
}
