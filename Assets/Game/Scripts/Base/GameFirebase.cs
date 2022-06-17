using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STU;

#if GM_FIREBASE
using Firebase;
using Firebase.Extensions;

#if GM_FIREBASE_REMOTE_CONFIG
using Firebase.RemoteConfig;
#endif

#if GM_FIREBASE_ANALYTICS
using Firebase.Analytics;
#endif

#endif

public class GameFirebase : Singleton<GameFirebase> {
    private bool isInitialized;

#if GM_FIREBASE
    private FirebaseApp firebase;

#if GM_FIREBASE_REMOTE_CONFIG
    private FirebaseRemoteConfig remoteConfig;
#endif

#endif

    public bool IsInitialized => isInitialized;

    protected override void Awake() {
        base.Awake();

        Initialize();
    }

    private void Initialize() {
#if GM_FIREBASE
        if (IsInitialized) return;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) {
                Logs.Log($"[GameFirebase] Initialized");
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                firebase = FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                isInitialized = true;

#if GM_FIREBASE_REMOTE_CONFIG
                InitializeRemoteConfig();
#endif

#if GM_FIREBASE_ANALYTICS
                InitializeAnalytics();
#endif

            }
            else {
                Logs.LogError($"[GameFirebase] Could not resolve all Firebase dependencies: {dependencyStatus}");
                // Firebase Unity SDK is not safe to use here.
            }
        });
#endif
    }


    #region Remote Config
#if GM_FIREBASE_REMOTE_CONFIG
    private void InitializeRemoteConfig() {
        remoteConfig = FirebaseRemoteConfig.DefaultInstance;

        Dictionary<string, object> defaults = new Dictionary<string, object>();
        //defaults.Add(PlayerGift.requireAdsStackKey, PlayerGift.defaultRequireAdsStack);
        //defaults.Add(GameConfig.cappingInterstitialAdDelayKey, GameConfig.cappingInterstitialAdDelay);
        //defaults.Add(GameConfig.cappingInterstitialAdRepeatKey, GameConfig.cappingInterstitialAdRepeat);

        Logs.Log($"[FirebaseRemoteConfig] Set defaults value");
        remoteConfig.SetDefaultsAsync(defaults).ContinueWithOnMainThread(task => {
            if (task.IsCompleted) {
                Logs.Log($"[FirebaseRemoteConfig] Set defaults value completed");
            }
            else {
                Logs.LogError($"[FirebaseRemoteConfig] Set defaults value failed");
            }

            Logs.Log($"[FirebaseRemoteConfig] Fetch");
            System.TimeSpan delay = System.TimeSpan.Zero;
            remoteConfig.FetchAndActivateAsync().ContinueWithOnMainThread((task) => {
                if (task.IsCompleted) {
                    Logs.Log($"[FirebaseRemoteConfig] Fetch and activate completed");
                }
                else {
                    Logs.LogError($"[FirebaseRemoteConfig] Fetch and activate failed");
                }
            });
        });
    }
#endif

    public string GetStringValue(string key, string defaultValue = "") {
        if (!IsInitialized) return defaultValue;

#if GM_FIREBASE_REMOTE_CONFIG
        return remoteConfig.GetValue(key).StringValue;
#endif
        return string.Empty;
    }

    public long GetLongValue(string key, long defaultValue = 0) {
        if (!IsInitialized) return defaultValue;

#if GM_FIREBASE_REMOTE_CONFIG
        return remoteConfig.GetValue(key).LongValue;
#endif
        return 0;
    }

    public bool GetBooleanValue(string key, bool defaultValue = false) {
        if (!IsInitialized) return defaultValue;

#if GM_FIREBASE_REMOTE_CONFIG
        return remoteConfig.GetValue(key).BooleanValue;
#endif
        return false;
    }

    public double GetDoubleValue(string key, double defaultValue = 0) {
        if (!IsInitialized) return defaultValue;

#if GM_FIREBASE_REMOTE_CONFIG
        return remoteConfig.GetValue(key).DoubleValue;
#endif
        return 0;
    }
    #endregion

    #region Analytics

    public class GameEvent {
        private string name;
        private Dictionary<string, string> eventParamaters;

        public string Name => name;

        private GameEvent(string name) {
            this.name = name;
            eventParamaters = new Dictionary<string, string>(4);
        }

        public static GameEvent Create(string name) {
            return new GameEvent(name);
        }

        public GameEvent Add(string parameterName, object parameterValue) {
            eventParamaters[parameterName] = parameterValue.ToString();
            return this;
        }

#if GM_FIREBASE_ANALYTICS
        public Parameter[] BuildFirebase() {
            Parameter[] paramaters = new Parameter[eventParamaters.Count];

            int index = 0;
            foreach (var item in eventParamaters) {
                paramaters[index] = new Parameter(item.Key, item.Value);
                index++;
            }

            return paramaters;
        }
#endif

        public override string ToString() {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(Name);
            foreach (var item in eventParamaters) {
                sb.Append($"\n{item.Key}: {item.Value}");
            }
            return sb.ToString();
        }
    }

#if GM_FIREBASE_ANALYTICS
    private void InitializeAnalytics() {
    }
#endif

    public static void SetProperty(string prpopertyName, object propertValue) {
        if (HasInstance && !Instance.IsInitialized) return;

#if GM_FIREBASE_ANALYTICS
        FirebaseAnalytics.SetUserProperty(prpopertyName, propertValue.ToString());
#endif
    }

    public static void LogEvent(GameEvent gameEvent) {
        if (HasInstance && !Instance.IsInitialized) return;

#if GM_FIREBASE_ANALYTICS
        FirebaseAnalytics.LogEvent(gameEvent.Name, gameEvent.BuildFirebase());
#endif

        if (Logs.Enabled) {
            Logs.Log($"[FirebaseAnalytics] Log Event: {gameEvent}");
        }
    }

    #endregion
}
