using System;
using Timberborn.CoreUI;
using Timberborn.Effects;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000010 RID: 16
	public class NeedViewFactory
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002DBA File Offset: 0x00000FBA
		public NeedViewFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, NeedEffectDescriptionService needEffectDescriptionService)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._needEffectDescriptionService = needEffectDescriptionService;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public NeedView Create(NeedSpec needSpec, NeedManager needManager)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/NeedView");
			UQueryExtensions.Q<Label>(visualElement, "Name", null).text = needSpec.DisplayName.Value;
			this._tooltipRegistrar.RegisterUpdatable(visualElement, () => this.GetTooltipText(needSpec, needManager));
			VisualElement criticalStateMarker = UQueryExtensions.Q<VisualElement>(visualElement, "Critical", null);
			DoubleSidedProgressBar progressBarBackground = UQueryExtensions.Q<DoubleSidedProgressBar>(visualElement, "ProgressBackground", null);
			DoubleSidedProgressBar doubleSidedProgressBar = UQueryExtensions.Q<DoubleSidedProgressBar>(visualElement, "Progress", null);
			VisualElement progressBarMarker = UQueryExtensions.Q<VisualElement>(visualElement, "ProgressMarker", null);
			doubleSidedProgressBar.SetMinimumLength(5);
			VisualElement controlItems = UQueryExtensions.Q<VisualElement>(visualElement, "ControlItems", null);
			Label exactValue = UQueryExtensions.Q<Label>(visualElement, "ExactValue", null);
			Label wellbeing = UQueryExtensions.Q<Label>(visualElement, "Wellbeing", null);
			UQueryExtensions.Q<Button>(visualElement, "Decrease", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				NeedViewFactory.ChangeNeedValue(needSpec, needManager, true);
			}, 0);
			UQueryExtensions.Q<Button>(visualElement, "Increase", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				NeedViewFactory.ChangeNeedValue(needSpec, needManager, false);
			}, 0);
			return new NeedView(visualElement, needSpec, criticalStateMarker, progressBarBackground, doubleSidedProgressBar, progressBarMarker, controlItems, exactValue, wellbeing);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002F06 File Offset: 0x00001106
		public string GetTooltipText(NeedSpec needSpec, NeedManager needManager)
		{
			return this._needEffectDescriptionService.GetNeedDescription(needSpec, needManager);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F18 File Offset: 0x00001118
		public static void ChangeNeedValue(NeedSpec needSpec, NeedManager needManager, bool increase)
		{
			InstantEffect instantEffect;
			if (increase)
			{
				float points = needSpec.IsNeverPositive ? 0.01f : -0.01f;
				instantEffect = new InstantEffect(needSpec.Id, points, 20);
				needManager.ApplyEffect(instantEffect);
				return;
			}
			float points2 = needSpec.IsNeverPositive ? -0.01f : 0.01f;
			instantEffect = new InstantEffect(needSpec.Id, points2, 20);
			needManager.ApplyEffect(instantEffect);
		}

		// Token: 0x04000046 RID: 70
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000047 RID: 71
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000048 RID: 72
		public readonly NeedEffectDescriptionService _needEffectDescriptionService;
	}
}
