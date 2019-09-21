using UnityEngine;

public interface IPickUp
{
    bool IsCollector(GameObject collidedObject);
    void OnCollection(GameObject collidedObject);
}