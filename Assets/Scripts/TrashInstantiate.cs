using UnityEngine;

public class TrashInstantiate : MonoBehaviour
{
    public GameObject[] trash;

    private float xPos;
    private float zPos;

    private bool finishInstantiate;
    private int trashCount;
    private int maxTrash = 9;
    private int minTrash = 3;
    private int trashToInstantiate;

    void Start()
    {
        trashToInstantiate = Random.Range(minTrash, maxTrash);
        InvokeRepeating("InstantiateTrash", 0, 0.5f);
    }

    void Update()
    {
        if (finishInstantiate)
        {
            CancelInvoke("InstantiateTrash");
        }
    }

    private void InstantiateTrash()
    {
        if(trashCount < trashToInstantiate)
        {
            xPos = Random.Range(1.7f, -17.2f);
            zPos = Random.Range(-3, 9);
            Instantiate(trash[Random.Range(0, trash.Length)], new Vector3(xPos, -0.07f, zPos), Quaternion.identity);
            trashCount++;
        }
        else
        {
            finishInstantiate = true;
        }
    }
}
