using System;
using Timberborn.ActivatorSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.MapStateSystem;
using UnityEngine.UIElements;

namespace Timberborn.ActivatorSystemUI
{
	// Token: 0x0200000B RID: 11
	public class TimedComponentActivatorFragment : IEntityPanelFragment
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000023DF File Offset: 0x000005DF
		public TimedComponentActivatorFragment(VisualElementLoader visualElementLoader, TimedActivatorProgressBarFactory progressBarFactory, MapEditorMode mapEditorMode)
		{
			this._visualElementLoader = visualElementLoader;
			this._progressBarFactory = progressBarFactory;
			this._mapEditorMode = mapEditorMode;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023FC File Offset: 0x000005FC
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/TimedComponentActivatorFragment");
			this._progressBar = this._progressBarFactory.Create(this._root, new Func<float>(this.GetActivationProgress), new Func<string>(this.GetDaysLeftUntilActivation), new Func<bool>(this.CountdownIsActive));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000246C File Offset: 0x0000066C
		public void ShowFragment(BaseComponent entity)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				TimedComponentActivator component = entity.GetComponent<TimedComponentActivator>();
				if (component && component.IsEnabled)
				{
					this._timedComponentActivator = component;
					TimedComponentActivatorSpec component2 = this._timedComponentActivator.GetComponent<TimedComponentActivatorSpec>();
					this._progressBar.Initialize(component2.ProgressBarActiveLabelLocKey, component2.ProgressBarNotActiveLabelLocKey, component2.IsHazardousActivator);
					this._activationWarningStatus = entity.GetComponent<ActivationWarningStatus>();
					this._root.ToggleDisplayStyle(true);
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024E5 File Offset: 0x000006E5
		public void ClearFragment()
		{
			this._timedComponentActivator = null;
			this._activationWarningStatus = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002501 File Offset: 0x00000701
		public void UpdateFragment()
		{
			if (this._timedComponentActivator)
			{
				if (this._timedComponentActivator.IsPastActivationTime)
				{
					this._root.ToggleDisplayStyle(false);
					return;
				}
				this._progressBar.UpdateState();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002535 File Offset: 0x00000735
		public float GetActivationProgress()
		{
			return this._timedComponentActivator.ActivationProgress;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002544 File Offset: 0x00000744
		public string GetDaysLeftUntilActivation()
		{
			string format = this._activationWarningStatus.IsCloseToActivation() ? "F1" : "F0";
			return this._activationWarningStatus.GetDaysLeftUntilActivation().ToString(format);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000257F File Offset: 0x0000077F
		public bool CountdownIsActive()
		{
			return this._timedComponentActivator.CountdownIsActive;
		}

		// Token: 0x0400001C RID: 28
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001D RID: 29
		public readonly TimedActivatorProgressBarFactory _progressBarFactory;

		// Token: 0x0400001E RID: 30
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x0400001F RID: 31
		public VisualElement _root;

		// Token: 0x04000020 RID: 32
		public TimedActivatorProgressBar _progressBar;

		// Token: 0x04000021 RID: 33
		public TimedComponentActivator _timedComponentActivator;

		// Token: 0x04000022 RID: 34
		public ActivationWarningStatus _activationWarningStatus;
	}
}
