using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AlienArmMotion))]
public class AlienArmAttack : MonoBehaviour
{
    [SerializeField] private AlienArmMotion _alienArm;
    [SerializeField] private OneTimeDamageDealer _damageDealer;
    [Space]
    [SerializeField] private float _windUpDelay;
    [SerializeField] private float _stayDuration;
    [SerializeField] private float _animationDuration;

    public void StartAttacking(Transform target)
    {
        _alienArm.Target = target;

        StartCoroutine(Attack(target));
    }

    private IEnumerator Attack(Transform target)
    {
        yield return new WaitForSeconds(_windUpDelay);
        
        var originalPosition = _alienArm.OriginalTilePosition;
        
        yield return StartCoroutine(_alienArm.ModifyArm(target.position, _animationDuration, false));
        yield return new WaitForSeconds(_stayDuration);
        
        _damageDealer.Clear();
        
        yield return StartCoroutine(_alienArm.ModifyArm(originalPosition, _animationDuration, true));

        _damageDealer.Clear();
        _alienArm.Target = null;
    }

    private void OnValidate()
    {
        if (_alienArm == null)
            _alienArm = GetComponent<AlienArmMotion>();
    }
}