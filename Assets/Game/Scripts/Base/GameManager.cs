using DG.Tweening;
using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private int targetFrameRate = 60;
    [SerializeField] private bool multiTouchEnabled = false;
    [SerializeField] private GameObject[] managers;
    private States state = States.None;
    public States State => state;

    protected override void OnAwake() {
        base.OnAwake();
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = targetFrameRate;
        Input.multiTouchEnabled = multiTouchEnabled;
    }

    public void Start() {
        StartCoroutine(IElaunch());
    }

    private IEnumerator IElaunch() {
        state = States.Loaded;
        yield return null;
        foreach(GameObject manager in managers) {
            Instantiate(manager, transform);
            yield return null;
        }
        yield return null;
        state = States.Started;
        EventDispatcher.Dispatch<EventKey.LoadFinal>(new EventKey.LoadFinal());
    }

    private void OnApplicationQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif   
    }

    public enum States {
        None,
        Loaded,
        Started,
        Quiting
    }
}
