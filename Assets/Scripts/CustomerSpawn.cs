using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] private Customer customerPrefab;
    [SerializeField] private Customer magicCustomerPrefab;

    public Customer SpawnCustomer()
    {
        return Instantiate(customerPrefab, transform.position, Quaternion.identity);
    }

    public Customer SpawnMagicCustomer()
    {
        return Instantiate(magicCustomerPrefab, transform.position, Quaternion.identity);
    }
}
