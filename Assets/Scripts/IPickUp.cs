using UnityEngine;

public interface IPickUp
{
    bool IsCollector(GameObject collider);
    bool OnCollection(GameObject collider);
}