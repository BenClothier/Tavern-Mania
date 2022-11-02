using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    void Start() // change to OnGameStart listener
    {
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
}
