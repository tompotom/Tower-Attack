using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TroupeGestionUI : MonoBehaviour
{
    [SerializeField] Image troupeImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] List<Troop> troopData = new List<Troop>();

    [Header("Stats")]
    [SerializeField] TextMeshProUGUI strengthText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI defenseText;
    [SerializeField] TextMeshProUGUI agilityText;

    private int actualData;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        actualData = 0;

        nameText.text = troopData[actualData].name;
        troupeImage.sprite = troopData[actualData].sprite;

        GetData();
    }

    public void NextOption()
    {
        actualData = actualData + 1;

        if (actualData == troopData.Count)
        {
            actualData = 0;
        }
        nameText.text = troopData[actualData].name;
        troupeImage.sprite = troopData[actualData].sprite;
        GetData();
    }

    public void BackOption()
    {
        actualData = actualData - 1;

        if (actualData < 0)
        {
            actualData = troopData.Count -1;
        }
        nameText.text = troopData[actualData].name;
        troupeImage.sprite = troopData[actualData].sprite;
        GetData();
    }

    void GetData()
    {
        strengthText.text = troopData[actualData].Damage.ToString();
        hpText.text = troopData[actualData].HP.ToString();
        defenseText.text = troopData[actualData].Defense.ToString();
        agilityText.text = troopData[actualData].Agility.ToString();
    }
}
