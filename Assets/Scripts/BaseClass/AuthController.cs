using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthController : MonoBehaviour {
    private static AuthController instance;
    public static AuthController Instance => instance;

    [SerializeField] private List<IWindows> windows = new List<IWindows>();

    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private IEnumerator Start() {
        yield return new WaitForSeconds(0.5f);

        if (PlayerPrefs.HasKey(BaseData.BaseDataName))
            OpenWindows(WindowsType.enter_main_param);
        else
            OpenWindows(WindowsType.enter_base_param);
    }

    public void InitWindows(IWindows window) {
        if (windows.Contains(window))
            return;

        windows.Add(window);
    }

    public void OpenWindows(WindowsType _type) {
        for (int i = 0; i < windows.Count; i++) {
            if (windows[i].GetWindowType() == _type) {
                windows[i].OpenWindow();
            }
            else
                windows[i].CloseWindows();
        }
    }
}
