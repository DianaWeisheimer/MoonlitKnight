using UnityEngine;
using System.Collections.Generic;
public class FruitTree : MonoBehaviour
{
    [SerializeField] private ParticleSystem _leaves;
    [SerializeField] private GameObject _fruit;
    [SerializeField] private List<Transform> _fruitPosition;
    [SerializeField] private float _fruitDropChance;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            TakeHit();
        }
    }

    private void TakeHit()
    {
        if(_leaves != null)
        {
            _leaves.Play();
        }

        if(_fruit != null && _fruitPosition.Count > 0)
        {
            float dropChance = Random.Range(0, 100);

            if(dropChance > _fruitDropChance)
            {
                DropFruit();
            }
        }
    }

    private void DropFruit()
    {
        int fruitPos = Random.Range(0, _fruitPosition.Count);

        Instantiate(_fruit, _fruitPosition[fruitPos]);
    }
}
