﻿using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class PrismSettingsMenuView : BaseView
    {
        [SerializeField] private Slider _sliderHeight;
        [SerializeField] private Slider _sliderRadius;

        private PrismSettingsMenuViewModel _viewModel;

        public void Bind(PrismSettingsMenuViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void HandlerHeightChanged()
        {
            _viewModel.HandlerHeightChanged(_sliderHeight.value);
        }

        public void HandlerRadiusChanged()
        {
            _viewModel.HandlerRadiusChanged(_sliderRadius.value);
        }

        public override void Enable()
        {
            // TODO
        }

        public override void Disable()
        {
            // TODO
        }
    }
}
