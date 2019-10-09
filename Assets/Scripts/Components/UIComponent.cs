using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    sealed class UIComponent:IEcsAutoResetComponent
    {
        public UnityEngine.UI.Text UIText = null;
        public UnityEngine.UI.Slider UISlider = null;

        void IEcsAutoResetComponent.Reset()
        {
            UIText = null;
            UISlider = null;
        }
    }
}