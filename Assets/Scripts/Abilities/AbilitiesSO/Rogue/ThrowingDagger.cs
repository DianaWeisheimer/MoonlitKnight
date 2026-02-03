using UnityEngine;

public class ThrowingDagger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var mark = new MarkedForDeathEffect(10f, null, other.GetComponent<Character>(), 20f);
            //var bleed = new BleedEffect(duration: 5f, tickInterval: 1f, target: other.GetComponent<Character>(), damage: 10f);
            other.GetComponent<Character>().statusEffects.ApplyEffect(mark);
            Destroy(gameObject);
        }

    }
}
