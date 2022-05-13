using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSetting : MonoBehaviour
{
    public const string SETTING_ROOM = "SettingRoom";
    [SerializeField] private Slider slider;
    [Header("Size")]
    [SerializeField] private float minSize,maxSize;
    private float valueRoom;
    private void Awake() {
        slider.onValueChanged.AddListener(SetUpCamera);    
    }

    private void Start() {
        LoadData();
        slider.value = Mathf.Clamp01(valueRoom);
        SetUpCamera(slider.value);
    }

    private void SetUpCamera(float percent) {
        float plusSize = (maxSize-minSize)*percent;
        float newSize = minSize + plusSize;
        if(InGameManager.Instance!=null) {
            InGameManager.Instance.Camera.orthographicSize = newSize;
        }
    }

    #region Save And Load
    private void SaveData() {
        PlayerPrefs.SetFloat(SETTING_ROOM, slider.value);
    }

    private void LoadData() {
        if(PlayerPrefs.HasKey(SETTING_ROOM)) {
            valueRoom = PlayerPrefs.GetFloat(SETTING_ROOM);
        } else {
            valueRoom = 0f;
            SaveData();
        }
    }
    #endregion
    #region EventGame
    private void OnDisable() {
        SaveData();
    }
    private void OnApplicationQuit() {
        SaveData();
    }

    private void OnApplicationFocus(bool focus) {
        if(!focus) {
            SaveData();
        }
    }

    private void OnApplicationPause(bool pause) {
        if(pause) {
            SaveData();
        }
    }
    #endregion
}
