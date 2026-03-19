using System;
using Timberborn.CoreUI;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200000F RID: 15
	public class NeedView
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000029F4 File Offset: 0x00000BF4
		public VisualElement Root { get; }

		// Token: 0x06000037 RID: 55 RVA: 0x000029FC File Offset: 0x00000BFC
		public NeedView(VisualElement root, NeedSpec needSpec, VisualElement criticalStateMarker, DoubleSidedProgressBar progressBarBackground, DoubleSidedProgressBar progressBar, VisualElement progressBarMarker, VisualElement controlItems, Label exactValue, Label wellbeing)
		{
			this.Root = root;
			this._needSpec = needSpec;
			this._criticalStateMarker = criticalStateMarker;
			this._progressBarBackground = progressBarBackground;
			this._progressBarMarker = progressBarMarker;
			this._progressBar = progressBar;
			this._controlItems = controlItems;
			this._exactValue = exactValue;
			this._wellbeing = wellbeing;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A54 File Offset: 0x00000C54
		public void Update(NeedManager needManager, bool showControls)
		{
			this.UpdateProgress(needManager);
			this.UpdateCriticalStateMarker(needManager);
			this.UpdateWellbeing(needManager);
			this._controlItems.ToggleDisplayStyle(showControls);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A77 File Offset: 0x00000C77
		public void UpdateVisibility(NeedManager needManager, bool alwaysVisible)
		{
			this.Root.ToggleDisplayStyle(alwaysVisible || this.ShouldBeVisible(needManager));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A94 File Offset: 0x00000C94
		public void UpdateProgress(NeedManager needManager)
		{
			bool isActive = needManager.NeedIsActive(this._needSpec.Id);
			float needPoints = needManager.GetNeedPoints(this._needSpec.Id);
			bool isNegative = needPoints < 0f;
			this._exactValue.text = string.Format("{0:F2}", needPoints);
			this.UpdateProgressBarBackground(isActive, isNegative);
			this.UpdateProgressBar(needPoints, isNegative);
			this.UpdateProgressBarMarker();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B00 File Offset: 0x00000D00
		public void UpdateProgressBarBackground(bool isActive, bool isNegative)
		{
			this._progressBarBackground.UpdateProgress(isNegative ? this._needSpec.MinimumValue : this._needSpec.MaximumValue, this._needSpec.MinimumValue, this._needSpec.MaximumValue);
			this._progressBarBackground.ToggleDisplayStyle(isActive);
			this._progressBarBackground.EnableInClassList(NeedView.UnfavorableNeedClass, isNegative);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B66 File Offset: 0x00000D66
		public void UpdateProgressBar(float needPoints, bool isNegative)
		{
			this._progressBar.UpdateProgress(needPoints, this._needSpec.MinimumValue, this._needSpec.MaximumValue);
			this._progressBar.EnableInClassList(NeedView.UnfavorableNeedClass, isNegative);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B9C File Offset: 0x00000D9C
		public void UpdateProgressBarMarker()
		{
			float num = this.CalculateMarkerPosition(this._needSpec.IsNeverNegative || this._needSpec.IsNeverPositive);
			this._progressBarMarker.style.left = new StyleLength(Length.Percent(num * 100f));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BEC File Offset: 0x00000DEC
		public float CalculateMarkerPosition(bool isSingleSided)
		{
			if (!isSingleSided)
			{
				return Mathf.Clamp01(Mathf.InverseLerp(this._needSpec.MinimumValue, this._needSpec.MaximumValue, 0f));
			}
			return 0f;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C1C File Offset: 0x00000E1C
		public void UpdateCriticalStateMarker(NeedManager needManager)
		{
			string id = this._needSpec.Id;
			bool flag = needManager.NeedIsInCriticalState(this._needSpec.Id);
			bool flag2 = this._needSpec.HasSpec<PunitiveNeedSpec>() && !needManager.NeedIsFavorable(id);
			bool visible = flag || flag2;
			this._criticalStateMarker.ToggleDisplayStyle(visible);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C70 File Offset: 0x00000E70
		public void UpdateWellbeing(NeedManager needManager)
		{
			if (needManager.NeedIsActive(this._needSpec.Id) || needManager.NeedIsCritical(this._needSpec.Id))
			{
				int needWellbeing = needManager.GetNeedWellbeing(this._needSpec.Id);
				bool flag = needManager.NeedIsFavorable(this._needSpec.Id);
				this._wellbeing.text = needWellbeing.ToString(NeedView.WellbeingFormat);
				this._wellbeing.EnableInClassList(NeedView.FavorableNeedClass, flag);
				this._wellbeing.EnableInClassList(NeedView.UnfavorableNeedClass, !flag);
				return;
			}
			int num = this._needSpec.IsNeverNegative ? this._needSpec.GetFavorableWellbeing() : this._needSpec.GetUnfavorableWellbeing();
			this._wellbeing.text = num.ToString(NeedView.WellbeingFormat);
			this._wellbeing.EnableInClassList(NeedView.FavorableNeedClass, false);
			this._wellbeing.EnableInClassList(NeedView.UnfavorableNeedClass, false);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D63 File Offset: 0x00000F63
		public bool ShouldBeVisible(NeedManager needManager)
		{
			return needManager.NeedIsEnabled(this._needSpec.Id) && (!this._needSpec.HasSpec<InactiveHiddenNeedSpec>() || needManager.NeedIsActive(this._needSpec.Id));
		}

		// Token: 0x0400003A RID: 58
		public static readonly string FavorableNeedClass = "need-view--green";

		// Token: 0x0400003B RID: 59
		public static readonly string UnfavorableNeedClass = "need-view--red";

		// Token: 0x0400003C RID: 60
		public static readonly string WellbeingFormat = "+#;-#;0";

		// Token: 0x0400003E RID: 62
		public readonly VisualElement _criticalStateMarker;

		// Token: 0x0400003F RID: 63
		public readonly DoubleSidedProgressBar _progressBarBackground;

		// Token: 0x04000040 RID: 64
		public readonly DoubleSidedProgressBar _progressBar;

		// Token: 0x04000041 RID: 65
		public readonly VisualElement _progressBarMarker;

		// Token: 0x04000042 RID: 66
		public readonly VisualElement _controlItems;

		// Token: 0x04000043 RID: 67
		public readonly NeedSpec _needSpec;

		// Token: 0x04000044 RID: 68
		public readonly Label _exactValue;

		// Token: 0x04000045 RID: 69
		public readonly Label _wellbeing;
	}
}
