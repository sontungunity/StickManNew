using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectFrame: FrameBase{
        
        [SerializeField] private List<LevelSelectView> levelView;
    
        private void Awake() {
            int i = 0;
            foreach(LevelSelectView view in levelView) {
                view.Init( this, i);
                i++;
            }
        }
        public override void OnShow(Action onCompleted = null, bool instant = false) {
            base.OnShow(onCompleted, instant);
            for(int i = 0; i < levelView.Count; i++)
            {
                levelView[i].enabled = true;
            }
        }
}