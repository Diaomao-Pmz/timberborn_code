using System;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.UILayoutSystem;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.TimeSystemUI
{
	// Token: 0x02000005 RID: 5
	public class ClockPanel : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002162 File Offset: 0x00000362
		public ClockPanel(IDayNightCycle dayNightCycle, WorkingHoursManager workingHoursManager, UILayout uiLayout, VisualElementLoader visualElementLoader, EventBus eventBus)
		{
			this._dayNightCycle = dayNightCycle;
			this._workingHoursManager = workingHoursManager;
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._eventBus = eventBus;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002190 File Offset: 0x00000390
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/ClockPanel");
			this._needle = UQueryExtensions.Q<RotatingBackground>(this._root, "TimeNeedle", null);
			this._workTimeEndMarker = UQueryExtensions.Q<RotatingBackground>(this._root, "WorkingHoursNeedle", null);
			this.InitializeBorder();
			this.UpdateMovingParts();
			this._eventBus.Register(this);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F9 File Offset: 0x000003F9
		public void UpdateSingleton()
		{
			this.UpdateMovingParts();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002201 File Offset: 0x00000401
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopRight(this._root, int.MaxValue);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002219 File Offset: 0x00000419
		public void UpdateMovingParts()
		{
			this._needle.SetRotation(ClockPanel.NormalizeRotation(this._dayNightCycle.DayProgress));
			this._workTimeEndMarker.SetRotation(ClockPanel.NormalizeRotation(this._workingHoursManager.EndHours / 24f));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002258 File Offset: 0x00000458
		public void InitializeBorder()
		{
			float num = this._dayNightCycle.DaytimeLengthInHours / 24f;
			float startingAngleOffset = ClockPanel.StartingAngleOffset;
			float rotation = startingAngleOffset + num / 2f * 360f;
			float num2 = this._dayNightCycle.NighttimeLengthInHours / 24f;
			float num3 = startingAngleOffset + num * 360f;
			float rotation2 = num3 + num2 / 2f * 360f;
			UQueryExtensions.Q<RotatingBackground>(this._root, "DaytimeStart", null).SetRotation(startingAngleOffset);
			UQueryExtensions.Q<RotatingBackground>(this._root, "Daytime", null).SetRotation(rotation);
			UQueryExtensions.Q<RotatingBackground>(this._root, "NighttimeStart", null).SetRotation(num3);
			UQueryExtensions.Q<RotatingBackground>(this._root, "Nighttime", null).SetRotation(rotation2);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000231A File Offset: 0x0000051A
		public static float NormalizeRotation(float angle)
		{
			return angle * 360f + ClockPanel.StartingAngleOffset;
		}

		// Token: 0x04000008 RID: 8
		public static readonly float StartingAngleOffset = -60f;

		// Token: 0x04000009 RID: 9
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400000A RID: 10
		public readonly WorkingHoursManager _workingHoursManager;

		// Token: 0x0400000B RID: 11
		public readonly UILayout _uiLayout;

		// Token: 0x0400000C RID: 12
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000D RID: 13
		public readonly EventBus _eventBus;

		// Token: 0x0400000E RID: 14
		public RotatingBackground _needle;

		// Token: 0x0400000F RID: 15
		public RotatingBackground _workTimeEndMarker;

		// Token: 0x04000010 RID: 16
		public VisualElement _root;
	}
}
