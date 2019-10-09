using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    sealed class UIComponent:IEcsAutoResetComponent
    {
        public UnityEngine.UI.Text UIHPText = null;
        public UnityEngine.UI.Slider UIHPSlider = null;
        public UnityEngine.UI.Text UILoadLevelText = null;

        void IEcsAutoResetComponent.Reset()
        {
            UIHPText = null;
            UIHPSlider = null;
            UILoadLevelText = null;
        }
    }
}