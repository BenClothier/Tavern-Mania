using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;

    public void SpawnCustomer()
    {
        Instantiate(customerPrefab, transform.position, Quaternion.identity);
    }
}
