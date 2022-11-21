using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseDataInfoWindow : MonoBehaviour, IWindows {
    [SerializeField] private WindowsType _type = WindowsType.enter_base_param;

    [Header("Main Data")]
    [SerializeField] private TMP_InputField t_CurrencyRate;
    [SerializeField] private TMP_Dropdown logistycType;
    [SerializeField] private TMP_InputField priceInCurrency;
    [SerializeField] private TMP_InputField basePriceMultiplay;
    [SerializeField] private TMP_InputField marketComisy;
    [SerializeField] private TMP_InputField otherTax;

    [Header("WH Table")]
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject WareHouse_Block;

    [Header("Buttons")]
    [SerializeField] private Button b_SaveNewData;
    [SerializeField] private Button b_AddNewWH;

    [Header("Save block")]
    [SerializeField] private GameObject saveBlock;
    [SerializeField] private Button b_Save;
    [SerializeField] private Button b_NotSave;

    private BaseParametrs base_data = new BaseParametrs();

    private IEnumerator Start() {
        while (!AuthController.Instance)
            yield return new WaitForFixedUpdate();

        AuthController.Instance.InitWindows(this);

        b_AddNewWH.onClick.AddListener(() => {
            AuthController.Instance.ClearCurrentWh();
            AuthController.Instance.OpenWindows(WindowsType.create_new_warehouse);
        });

        b_SaveNewData.onClick.AddListener(() => SaveData());
        b_Save.onClick.AddListener(() => {
            saveBlock.SetActive(false);
            SaveData();
        });

        b_NotSave.onClick.AddListener(() => {
            gameObject.SetActive(false);
            saveBlock.SetActive(false);
        });

        gameObject.SetActive(false);
    }

    public void CloseWindows() {
        while(content.childCount > 0)
            Destroy(content.GetChild(0));

        if (PlayerPrefs.GetString(BaseData.BaseDataName) != ConvertBDToString(base_data)) {
            saveBlock.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }

    public WindowsType GetWindowType() {
        return _type;
    }

    public void OpenWindow() {
        gameObject.SetActive(true);
        saveBlock.SetActive(false);

        if (PlayerPrefs.HasKey(BaseData.BaseDataName)) {
            base_data = JsonUtility.FromJson<BaseParametrs>(PlayerPrefs.GetString(BaseData.BaseDataName));

            t_CurrencyRate.text = base_data.currency_rate.ToString();
            logistycType.SetValueWithoutNotify(base_data.logistyc_type);

            priceInCurrency.text = base_data.logistyc_price.ToString();
            basePriceMultiplay.text = base_data.logistyc_multiplay.ToString();
            marketComisy.text = base_data.market_comisy.ToString();
            otherTax.text = base_data.other_tax.ToString();

            for (int i = 0; i < base_data.wrs_list.Count; i++) {
                WH_Info_String block = Instantiate(WareHouse_Block, Vector2.zero, Quaternion.identity, content).GetComponent<WH_Info_String>();
                block.Init(base_data.wrs_list[i]);
            }
        }
    }

    private void SaveData() {
        StartCoroutine(SaveTask());

        PlayerPrefs.SetString(BaseData.BaseDataName, ConvertBDToString(base_data));
    }

    public string ConvertBDToString(BaseParametrs param) {
        param.currency_rate = float.Parse(t_CurrencyRate.text);
        param.logistyc_type = logistycType.value;
        param.logistyc_price = float.Parse(priceInCurrency.text);
        param.logistyc_multiplay = int.Parse(basePriceMultiplay.text);
        param.market_comisy = float.Parse(marketComisy.text);
        param.other_tax = float.Parse(otherTax.text);

        string data = JsonUtility.ToJson(param);

        return data;
    }

    private IEnumerator SaveTask() {
        b_SaveNewData.interactable = false;
        yield return new WaitForSeconds(1);

        b_SaveNewData.interactable = true;
    }
}
