using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelicInfo : MonoBehaviour
{
    public Relic relic;
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    Image img;
    [SerializeField]
    TextMeshProUGUI effectText;
    [SerializeField]
    TextMeshProUGUI infoText;

    public RelicInfo(Relic _relic)
    {
        relic = _relic;
    }
    private void Start()
    {
        if (gameObject.tag.Equals("RelicBtn"))
        {
            img = gameObject.transform.GetChild(1).GetComponent<Image>();
        }
    }
    private void FixedUpdate()
    {
        if (gameObject.tag.Equals("RelicBtn"))
        {
            if (relic != null)
            {
                title.text = relic.title;
                img.sprite = relic.img;
            }
        }
        if (gameObject.tag.Equals("Relic"))
        {
            if (relic != null)
            {
                gameObject.GetComponent<Image>().sprite = relic.img;
            }
        }
        if (gameObject.tag.Equals("RelicInfo"))
        {
            if (relic != null)
            {
                title.text = relic.title;
                effectText.text = relic.text;
                infoText.text = relic.infoText;
                img.sprite = relic.img;
            }
        }

    }
}
