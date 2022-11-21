using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WH_Info_String : MonoBehaviour
{
    [SerializeField] private Button b_redactWHInfo;
    [SerializeField] private TMP_Text wh_name;

    private WareHoueseData wh_data;

    public void Init(WareHoueseData data) {
        wh_data = data;

        wh_name.text = wh_data.wh_Name;

        b_redactWHInfo.onClick.AddListener(() => {
            AuthController.Instance.SetCurrentWH(wh_data);
            AuthController.Instance.OpenWindows(WindowsType.create_new_warehouse);
        });
    }
}
