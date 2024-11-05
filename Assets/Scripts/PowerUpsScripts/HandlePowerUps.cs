using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePowerUps : MonoBehaviour
{
    [SerializeField] private List<GameObject> yellowCandyList = new List<GameObject>();
    [SerializeField] private List<GameObject> blueCandyList = new List<GameObject>();
    [SerializeField] private List<GameObject> redCandyList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            PowerUps powerUp = collision.GetComponent<PowerUps>();
            if (powerUp != null)
            {
                AddCandiesToList(powerUp.yellowCandies, yellowCandyList);
                AddCandiesToList(powerUp.blueCandies, blueCandyList);
                AddCandiesToList(powerUp.redCandies, redCandyList);

                // Destruir el power-up después de recogerlo
                Destroy(collision.gameObject);
            }
        }
    }

    private void AddCandiesToList(List<GameObject> sourceList, List<GameObject> targetList)
    {
        foreach (var candy in sourceList)
        {
            targetList.Add(candy);
        }
    }
}
