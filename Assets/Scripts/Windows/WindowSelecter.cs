using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindowSelecter : MonoBehaviour
{
    [Header("Hello Block")]
    [SerializeField] private Animator helloBlock;
    [SerializeField] private TMP_InputField enterUserName;
    [SerializeField] private Button b_enterName;
    [SerializeField] private TMP_Text t_presentText;

    [Header("Main Block")]
    [SerializeField] private Button b_OpenBaseParamWindow;
    [SerializeField] private Button b_OpenCalculateWindow;

    private IEnumerator Start() {
        if (PlayerPrefs.HasKey(BaseData.UserName)) {
            helloBlock.gameObject.SetActive(true);
            enterUserName.gameObject.SetActive(false);
            b_enterName.gameObject.SetActive(false);

            t_presentText.text = $"Приветствую \n{PlayerPrefs.GetString(BaseData.UserName)}!";

            yield return new WaitForSeconds(1);
            helloBlock.SetTrigger("Enter");
        }
        else {
            helloBlock.gameObject.SetActive(true);
            enterUserName.gameObject.SetActive(true);
            b_enterName.gameObject.SetActive(true);

            t_presentText.text = $"Приветствую тебя юный Продаван!\n Введи имя свое в поле мною указанное:";
            b_enterName.onClick.AddListener(() => {
                PlayerPrefs.SetString(BaseData.UserName, enterUserName.text);
                helloBlock.SetTrigger("Enter");
            });
        }

        while (!AuthController.Instance)
            yield return new WaitForFixedUpdate();

        b_OpenBaseParamWindow.onClick.AddListener(() => AuthController.Instance.OpenWindows(WindowsType.enter_base_param));
        b_OpenCalculateWindow.onClick.AddListener(() => AuthController.Instance.OpenWindows(WindowsType.enter_main_param));

        if (PlayerPrefs.HasKey(BaseData.BaseDataName))
            AuthController.Instance.OpenWindows(WindowsType.enter_main_param);
        else
            AuthController.Instance.OpenWindows(WindowsType.enter_base_param);
    }
}
