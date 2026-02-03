using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
public class DamagePopupGenerator : MonoBehaviour
{
    public static DamagePopupGenerator instance;
    public GameObject prefab;
    public Color32[] colors;

    private void Awake()
    {
        instance = this;
    }

    public void CreatePopup(Vector3 position, float[] damages)
    {
        for(int i = 0; i < damages.Length; i++)
        {
            if (damages[i] > 0)
            {
                SpawnPopup(position, damages[i].ToString("0"), (DamageType)i);
            }
        }
    }

    private void SpawnPopup(Vector3 position, string text, DamageType type)
    {
        Vector3 randomness = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        var popup = Instantiate(prefab, position + randomness, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        temp.isOverlay = true;

        switch (type)
        {
            case DamageType.Physical:
                temp.color = colors[0];
                break;
            case DamageType.Magic:
                temp.color = colors[1];
                break;
            case DamageType.Fire:
                temp.color = colors[2];
                break;
            case DamageType.Lightning:
                temp.color = colors[3];
                break;
            case DamageType.Divine:
                temp.color = colors[4];
                break;
            case DamageType.Occult:
                temp.color = colors[5];
                break;
            case DamageType.True:
                temp.color = colors[6];
                break;
        }

        Destroy(popup, 1f);
    }
}
