using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateWareHouseWindow : MonoBehaviour, IWindows {
    [SerializeField] private WindowsType _type = WindowsType.create_new_warehouse;

    [SerializeField] private TMP_InputField wh_Name;
    [SerializeField] private TMP_InputField wh_priceToClient;
    [SerializeField] private TMP_InputField wh_addPriceForVolume;
    [SerializeField] private TMP_InputField wh_priceBackToWH;
    [SerializeField] private Button b_Close;
    [SerializeField] private Button b_Continue;

    [SerializeField] private TMP_Text error_text;

    private BaseParametrs _base;

    private IEnumerator Start() {
        while (!AuthController.Instance)
            yield return new WaitForFixedUpdate();

        AuthController.Instance.InitWindows(this);

        b_Continue.onClick.AddListener(() => { 
        
        });

        b_Close.onClick.AddListener(() => {
            AuthController.Instance.ClearCurrentWh();
            AuthController.Instance.OpenWindows(WindowsType.enter_base_param);
        });

        gameObject.SetActive(false);
    }

    public void Update_WHBlock() {
        WareHoueseData data = AuthController.Instance.GetCurrentWH();
        foreach (WareHoueseData _d in _base.wrs_list) {
            if (_d.wh_Name == data.wh_Name) {

                _d.wh_Name = wh_Name.text;
                _d.wh_PriceLogistycToClient = float.Parse(wh_priceToClient.text);
                _d.wh_AddPriceForWeight = float.Parse(wh_addPriceForVolume.text);
                _d.wh_PriceBackLogistycToWH = float.Parse(wh_priceBackToWH.text);

                PlayerPrefs.SetString(BaseData.BaseDataName, JsonUtility.ToJson(_base));
                break;
            }
        }

        AuthController.Instance.OpenWindows(WindowsType.enter_base_param);
    }

    public void Add_WHBlock() {
        error_text.text = "";

        if (wh_Name.text == "") {
            error_text.text = "Введите название склада";
            return;
        }

        if (wh_priceBackToWH.text == "" || wh_priceToClient.text == "" || wh_addPriceForVolume.text == "") {
            error_text.text = "Какие-то поля не заполнены";
            return;
        }

        WareHoueseData newData = new WareHoueseData();

        newData.wh_Name = wh_Name.text;
        newData.wh_PriceLogistycToClient = float.Parse(wh_priceToClient.text);
        newData.wh_AddPriceForWeight = float.Parse(wh_addPriceForVolume.text);
        newData.wh_PriceBackLogistycToWH = float.Parse(wh_priceBackToWH.text);

        _base.wrs_list.Add(newData);

        PlayerPrefs.SetString(BaseData.BaseDataName, JsonUtility.ToJson(_base));

        AuthController.Instance.OpenWindows(WindowsType.enter_base_param);
    }

    public WindowsType GetWindowType() {
        return _type;
    }

    public void OpenWindow() {
        error_text.text = "";
        b_Continue.onClick.RemoveAllListeners();

        if (PlayerPrefs.HasKey(BaseData.BaseDataName))
            _base = JsonUtility.FromJson<BaseParametrs>(PlayerPrefs.GetString(BaseData.BaseDataName));
        else {
            error_text.text = "Перед созданием нового склада необходимо сохранить базовые настройки. Перейдите в окно базовых настроек и нажмите кнопку \"Сохранить\"";
            return;
        } 

        WareHoueseData data = AuthController.Instance.GetCurrentWH();
        if (data != null) {
            wh_Name.text = "";
            wh_addPriceForVolume.text = "0";
            wh_priceToClient.text = "0";
            wh_priceBackToWH.text = "0";

            b_Continue.onClick.AddListener(() => Add_WHBlock());
        }
        else {
            wh_Name.text = data.wh_Name;
            wh_addPriceForVolume.text = data.wh_AddPriceForWeight.ToString();
            wh_priceToClient.text = data.wh_PriceLogistycToClient.ToString();
            wh_priceBackToWH.text = data.wh_PriceBackLogistycToWH.ToString();

            b_Continue.onClick.AddListener(() => Update_WHBlock());
        }
    }

    public void CloseWindows() {
        AuthController.Instance.ClearCurrentWh();
        gameObject.SetActive(false);
    }
}
