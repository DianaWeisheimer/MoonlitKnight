using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public Gradient ambientSkyColor;
    public Gradient ambientEquatorColor;
    public Gradient directionalColor;
    public Gradient fogColor;
    public Gradient grassVectorColor;
    public Gradient grassDarkerColor;
    public Gradient cloudColor;
    public Gradient skyStar;
    public Renderer cloud;
    public Renderer star;
    public List<Renderer> grass;
    public Light directionalLight;
    public float timeDelay;
    [Range(0,24)]public float timeOfDay;

    private void Update()
    {
        timeOfDay += Time.deltaTime * timeDelay;
        timeOfDay %= 24;
        UpdateLight(timeOfDay / 24);
    }

    void UpdateLight(float timePercent)
    {
        RenderSettings.ambientSkyColor = ambientSkyColor.Evaluate(timePercent);
        RenderSettings.ambientEquatorColor = ambientEquatorColor.Evaluate(timePercent);
        RenderSettings.fogColor = fogColor.Evaluate(timePercent);

        cloud.material.SetColor("_Color", cloudColor.Evaluate(timePercent));
        star.material.SetColor("_Alpha", skyStar.Evaluate(timePercent));

        /*for(int i = 0; i <grass.Count; i++)
        {
            grass[i].material.SetColor("_Vect", grassVectorColor.Evaluate(timePercent));
            grass[i].material.SetColor("_Darker", grassDarkerColor.Evaluate(timePercent));
        }*/

        directionalLight.color = directionalColor.Evaluate(timePercent);
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360) - 90f, 170f, 0f));
    }
}
