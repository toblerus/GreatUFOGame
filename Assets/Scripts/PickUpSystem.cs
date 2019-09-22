using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    public static PickUpSystem Instance { get; private set; }

    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _maxPickUps;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<BasePickUp> _pickUpPrefabs;

    private readonly List<BasePickUp> _spawnedPickUps = new List<BasePickUp>();
    private readonly List<Transform> _usedSpawnPoints = new List<Transform>();

    public void DespawnPickup(Transform spawnPoint)
    {
        if (spawnPoint == null)
            return;

        _usedSpawnPoints.Remove(spawnPoint);
    }
    
    private IEnumerator SpawnPickUps()
    {
        while (true)
        {
            if (_spawnedPickUps.Count(pickUp => pickUp.IsActive) >= _maxPickUps)
            {
                yield return new WaitForSeconds(_spawnDelay);
            }
            else
            {
                yield return new WaitForSeconds(_spawnDelay);

                var inactivePickUp = _spawnedPickUps.FirstOrDefault(pickUp => !pickUp.IsActive);
                if (inactivePickUp == null)
                {
                    var randomPrefab = _pickUpPrefabs[Random.Range(0, _pickUpPrefabs.Count)];
                    inactivePickUp = Instantiate(randomPrefab);
                    _spawnedPickUps.Add(inactivePickUp);
                }

                var unusedSpawnPoints = _spawnPoints.Where(point => !_usedSpawnPoints.Contains(point)).ToList();
                var selectedSpawn = unusedSpawnPoints.OrderByDescending(point => PlayerService.Instance.DistanceToClosestPlayer(point.position)).First();

                inactivePickUp.Spawn(selectedSpawn, null);
                _usedSpawnPoints.Add(selectedSpawn);
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        StartCoroutine(SpawnPickUps());
    }
}