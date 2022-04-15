
using STU;

public static class EventKey {
    public struct LoadFinal : IEventArgs {

    }

    public struct SceneChange : IEventArgs {
        public string nameScene;

        public SceneChange(string nameScene) {
            this.nameScene = nameScene;
        }
    }
}
