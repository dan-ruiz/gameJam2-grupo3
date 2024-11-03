using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPool : MonoBehaviour
{
    [SerializeField] private GameObject chocolateCandyPref;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private List<GameObject> chocolateCandyList;

    // Patron Singleton
    private static CandyPool instance;
    public static CandyPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AddCandiesToPool(poolSize);
    }


    private void AddCandiesToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject candy = Instantiate(chocolateCandyPref, transform);
            candy.GetComponent<Candy>().SetPool(this);
            candy.SetActive(false);
            chocolateCandyList.Add(candy);
        }
    }

    // Funcion que permite instanciar un candy desde otro script 
    public GameObject RequestCandy()
    {
        for (int i = 0; i < chocolateCandyList.Count; i++)
        {
            if (!chocolateCandyList[i].activeSelf)
            {

                return chocolateCandyList[i];
            }
        }
        AddCandiesToPool(1);
        chocolateCandyList[chocolateCandyList.Count - 1].SetActive(true);
        return chocolateCandyList[chocolateCandyList.Count - 1];
    }

    public void ReturnCandyToPool(GameObject candy)
    {
        candy.SetActive(false);
    }
}
