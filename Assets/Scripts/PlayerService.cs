using System.Linq;
using UnityEngine;

public class PlayerService : MonoBehaviour
{
    public static PlayerService Instance { get; private set; }

    private PlayerController[] _players;

    public PlayerController ClosestPlayer(Vector2 currentPosition)
    {
        return _players.OrderBy(controller => Vector2.Distance(currentPosition, controller.transform.position)).First();
    }

    public float DistanceToClosestPlayer(Vector2 currentPosition)
    {
        return _players.Min(controller => Vector2.Distance(currentPosition, controller.transform.position));
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _players = FindObjectsOfType<PlayerController>();
        if (_players.Length == 0)
            Debug.LogError("No players were found in the scene.");
    }
}
