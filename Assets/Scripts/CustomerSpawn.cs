using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private GameObject magicCustomerPrefab;

    public void SpawnCustomer()
    {
        Instantiate(customerPrefab, transform.position, Quaternion.identity);
    }

    public void SpawnMagicCustomer()
    {
        Instantiate(magicCustomerPrefab, transform.position, Quaternion.identity);
    }
}
