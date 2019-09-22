using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    [SerializeField] private List<AlienArmAttack> _alienArms;
    [SerializeField] private Transform _armTarget;
    [SerializeField] private float _attackDelay;

    private void Start()
    {
        StartCoroutine(AttackWithArms());
    }

    private IEnumerator AttackWithArms()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackDelay);
            
            foreach (var alienArm in _alienArms)
            {
                alienArm.StartAttacking(_armTarget);
            }
        }
    }
}