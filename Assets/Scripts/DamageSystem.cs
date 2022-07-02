using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] private int dmgDie;
    [SerializeField] private int dmgMult;

    // TODO: Weapon damage system
    // weapons damage D4-D12 etc
    // weapons count 1D-2D etc
    // calculate random value from parameters, pass that as damage 
    // Fists : 1D4
    // Sword : 1D8 / 1D10
    private void OnTriggerExit(Collider other)
    {
        var health = other.GetComponent<IDamageable>();
        if (health != null)
        {
            //var damage = Random.Range();
            //health.Damage(damage);
        }


    }
}
