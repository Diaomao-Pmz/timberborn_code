using System;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200001A RID: 26
	public class StatusInstance
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000037C4 File Offset: 0x000019C4
		public string StatusDescription { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000037CC File Offset: 0x000019CC
		public string AlertDescription { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000037D4 File Offset: 0x000019D4
		public bool IsPriorityStatus { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000037DC File Offset: 0x000019DC
		public bool ShowFloatingIcon { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000037E4 File Offset: 0x000019E4
		public StatusSubject StatusSubject { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000037EC File Offset: 0x000019EC
		public Sprite IconLarge { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000037F4 File Offset: 0x000019F4
		public Sprite IconSmall { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000037FC File Offset: 0x000019FC
		public Func<float> StatusValueGetter { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003804 File Offset: 0x00001A04
		public Func<StatusWarningType> StatusWarningTypeGetter { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000380C File Offset: 0x00001A0C
		public string WarningSound { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003814 File Offset: 0x00001A14
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000381C File Offset: 0x00001A1C
		public bool IsActive { get; private set; }

		// Token: 0x0600009A RID: 154 RVA: 0x00003828 File Offset: 0x00001A28
		public StatusInstance(string statusDescription, string alertDescription, bool isPriorityStatus, bool showFloatingIcon, StatusSubject statusSubject, Sprite iconLarge, Sprite iconSmall, Func<float> statusValueGetter, Func<StatusWarningType> statusWarningTypeGetter, string warningSound, IDayNightCycle dayNightCycle, float showDelayInHours)
		{
			this.StatusDescription = statusDescription;
			this.AlertDescription = alertDescription;
			this.IsPriorityStatus = isPriorityStatus;
			this.ShowFloatingIcon = showFloatingIcon;
			this.StatusSubject = statusSubject;
			this.IconLarge = iconLarge;
			this.IconSmall = iconSmall;
			this.StatusValueGetter = statusValueGetter;
			this.StatusWarningTypeGetter = statusWarningTypeGetter;
			this.WarningSound = warningSound;
			this._dayNightCycle = dayNightCycle;
			this._showDelayInDays = showDelayInHours / 24f;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000389E File Offset: 0x00001A9E
		public bool ShowAlert
		{
			get
			{
				return !string.IsNullOrEmpty(this.AlertDescription);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000038AE File Offset: 0x00001AAE
		public void Activate()
		{
			if (!this.IsActive)
			{
				this._lastInactiveTimestamp = this._dayNightCycle.PartialDayNumber;
			}
			this.IsActive = true;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000038D0 File Offset: 0x00001AD0
		public void Deactivate()
		{
			this.IsActive = false;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000038DC File Offset: 0x00001ADC
		public bool IsVisible()
		{
			bool flag = this.IsActive && !this.IsOverriden;
			if (this._showDelayInDays > 0f && flag)
			{
				return this._dayNightCycle.PartialDayNumber - this._lastInactiveTimestamp > this._showDelayInDays;
			}
			return flag;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000392B File Offset: 0x00001B2B
		public bool IsOverriden
		{
			get
			{
				return !this.IsPriorityStatus && this.StatusSubject.InPriorityMode;
			}
		}

		// Token: 0x04000057 RID: 87
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000058 RID: 88
		public readonly float _showDelayInDays;

		// Token: 0x04000059 RID: 89
		public float _lastInactiveTimestamp;
	}
}
