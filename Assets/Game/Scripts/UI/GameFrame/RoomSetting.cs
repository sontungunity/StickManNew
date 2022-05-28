using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RoomSetting : MonoBehaviour {
    public const string SETTING_ROOM = "SettingRoom";
    [SerializeField] private Slider slider;
    [Header("Size")]
    [SerializeField]private float speed = 200f;
    [SerializeField] private float minSize,maxSize;
    [SerializeField] private RectTransform rectTransform;
    private Tween tweenMove;
    private Tween tweenRoom;
    private Vector2 orginPosition;

    private void Awake() {
        slider.onValueChanged.AddListener(SetUpCamera);
        orginPosition = rectTransform.anchoredPosition;
    }

    public void StartActive(bool active) {
        if(active) {
            //Move
            float distance = Mathf.Abs(orginPosition.y - rectTransform.anchoredPosition.y);
            float timeMove = distance/speed;
            tweenMove.CheckKillTween();
            tweenMove = rectTransform.DOAnchorPosY(orginPosition.y, timeMove).SetEase(Ease.Linear);

            //Room
            float roomSave = LoadData();
            float plusSize = (maxSize-minSize)*roomSave;
            float newSize = minSize + plusSize;
            SetUpOrtho(newSize,roomSave);
        } else {
            float distance = Mathf.Abs(-orginPosition.y - rectTransform.anchoredPosition.y);
            float timeMove = distance/speed;
            tweenMove.CheckKillTween();
            tweenMove = rectTransform.DOAnchorPosY(-orginPosition.y, timeMove).SetEase(Ease.Linear);
        }
    }

    private void SetUpCamera(float percent) {
        tweenRoom.CheckKillTween();
        float plusSize = (maxSize-minSize)*percent;
        float newSize = minSize + plusSize;
        if(InGameManager.Instance != null) {
            InGameManager.Instance.Camera.orthographicSize = newSize;
        }
    }

    public void SetUpOrtho(float newSize,float value = -1) {
        float distanceRoom = Mathf.Abs(newSize - InGameManager.Instance.Camera.orthographicSize);
        tweenRoom.CheckKillTween();
        tweenRoom = InGameManager.Instance.Camera.DOOrthoSize(newSize, distanceRoom/2).SetEase(Ease.Linear).OnComplete(()=> {
            if(value != -1) {
                slider.value = value;
            }
        });
    }

    #region Save And Load
    private void SaveData() {
        PlayerPrefs.SetFloat(SETTING_ROOM, slider.value);
    }

    private float LoadData() {
        if(PlayerPrefs.HasKey(SETTING_ROOM)) {
            return PlayerPrefs.GetFloat(SETTING_ROOM);
        } else {
            slider.value = 0f;
            SaveData();
            return 0f;
        }
    }
    #endregion
    #region EventGame
    private void OnDisable() {
        tweenMove.CheckKillTween();
        tweenRoom.CheckKillTween();
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
