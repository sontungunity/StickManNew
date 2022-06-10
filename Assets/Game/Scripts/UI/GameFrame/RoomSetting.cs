using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RoomSetting : MonoBehaviour {
    [SerializeField] private Slider slider;
    [Header("Size")]
    [SerializeField]private float speed = 200f;
    [SerializeField] private RectTransform rectTransform;
    private Tween tweenMove;
    private Vector2 orginPosition;

    private void Awake() {
        slider.onValueChanged.AddListener(SetUpCamera);
        orginPosition = rectTransform.anchoredPosition;
    }

    private void Start() {
        slider.value = ProcameraController.Instance.ValueSetting;
    }

    private void SetUpCamera(float percent) {
        ProcameraController.Instance.SetValueSetting(percent);
    }

    public void StartActive(bool active) {
        if(active) {
            //Move
            float distance = Mathf.Abs(orginPosition.y - rectTransform.anchoredPosition.y);
            float timeMove = distance/speed;
            tweenMove.CheckKillTween();
            tweenMove = rectTransform.DOAnchorPosY(orginPosition.y, timeMove).SetEase(Ease.Linear);
        } else {
            float distance = Mathf.Abs(-orginPosition.y - rectTransform.anchoredPosition.y);
            float timeMove = distance/speed;
            tweenMove.CheckKillTween();
            tweenMove = rectTransform.DOAnchorPosY(-orginPosition.y, timeMove).SetEase(Ease.Linear);
        }
    }
}
