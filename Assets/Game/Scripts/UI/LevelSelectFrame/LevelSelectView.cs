using UnityEngine;
using UnityEngine.UI;

public class LevelSelectView : MonoBehaviour {

    [SerializeField] private Button btn_Close;
    [SerializeField] private int Index;
    [Space]
    [SerializeField] private GameObject imgActive;
    private LevelSelectFrame levelSelectFrame;
    protected Status _status;
    private void Awake() {
        btn_Close.onClick.AddListener(SetUpInActive);    
    }

    public void Init(LevelSelectFrame levelFrame, int index) {
        this.levelSelectFrame = levelFrame;
        this.Index = index;
    }

    #region Status
    private void SetUpInActive() {
        imgActive.SetActive(false);
    }

    private void SetUpActive() {
        imgActive.SetActive(true);
    }
    #endregion

    protected void Show(Status status) {
        switch(status) {
            case Status.NOACTIVE:
                SetUpInActive();
                break;
            case Status.ACTIVE:
                SetUpActive();
                break;
        }
    }

    public enum Status {
        NONE,
        NOACTIVE,
        ACTIVE,
        RECEIVED,
    }
}
