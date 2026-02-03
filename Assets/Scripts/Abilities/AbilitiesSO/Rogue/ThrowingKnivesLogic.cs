using UnityEngine;

[System.Serializable]
public class ThrowingKnivesLogic : AbilityLogic
{
    private AbilitySensor sensor;
    private GameObject dagger;

    public ThrowingKnivesLogic(Character user, GameObject _dagger) : base(user)
    {
        dagger = _dagger;
        sensor = user.GetComponent<AbilitySensor>();
    }

    public override void Activate(float charge)
    {
        var targets = sensor.Scan();

        foreach (var target in targets)
        {
            Vector3 spawnPos = user.transform.position;
            spawnPos.y += 1.75f;

            var d = GameObject.Instantiate(dagger, spawnPos, Quaternion.identity);
            d.transform.LookAt(target.transform.position);
            d.GetComponent<Rigidbody>().AddForce((d.transform.forward).normalized * 30f, ForceMode.Impulse);
            // Apply damage, play effects, etc.
        }
    }

    public override bool CheckRequirements()
    {
        return true; // Add weapon check, proximity, etc.
    }
}
