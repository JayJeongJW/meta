using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using StackMiniGame;

namespace StackMiniGame
{


    public abstract class BaseUI : MonoBehaviour
    {
        protected UIManager uiManager;

        public virtual void Init(UIManager uiManager)
        {
            this.uiManager = uiManager;
        }

        protected abstract UIState GetUIstate();

        public void SetActive(UIState state)
        {
            gameObject.SetActive(GetUIstate() == state);
        }
    }
}
