using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class BaseData
{
    private static string base_data_name = "BasicData";
    public static string BaseDataName => base_data_name;

    private static string user_name = "UserName";
    public static string UserName => user_name;
}

public enum WindowsType { enter_base_param, enter_main_param, create_new_warehouse };

public interface IWindows {
    public static WindowsType w_type;

    public WindowsType GetWindowType();

    public void OpenWindow();

    public void CloseWindows();
}

[Serializable]
public class WareHoueseData {
    public string wh_Name = "default";

    public float wh_PriceLogistycToClient;
    public float wh_AddPriceForWeight;
    public float wh_PriceBackLogistycToWH;
}

[Serializable]
public class BaseParametrs {
    public float currency_rate;
    public int logistyc_type;

    public float logistyc_price;
    public int logistyc_multiplay;

    public List<WareHoueseData> wrs_list = new List<WareHoueseData>();
}
