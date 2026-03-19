using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.ActivatorSystem
{
	// Token: 0x02000009 RID: 9
	public class ActivationWarningStatus : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002499 File Offset: 0x00000699
		public ActivationWarningStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024A8 File Offset: 0x000006A8
		public void Awake()
		{
			this._activationWarningStatusSpec = base.GetComponent<ActivationWarningStatusSpec>();
			LabeledEntitySpec component = base.GetComponent<LabeledEntitySpec>();
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon(this._activationWarningStatusSpec.StatusSpriteName, this._loc.T(this._activationWarningStatusSpec.StatusLocKey), this._loc.T(component.DisplayNameLocKey) + ": " + this._loc.T(ActivationWarningStatus.StatusLocKey), 0f);
			this._timedComponentActivator = base.GetComponent<TimedComponentActivator>();
			if (!this._timedComponentActivator.CountdownIsActive)
			{
				this._timedComponentActivator.CountdownActivated += delegate(object _, EventArgs _)
				{
					this.ActivateToggleIfPossible();
				};
			}
			if (!this._timedComponentActivator.IsPastActivationTime)
			{
				this._timedComponentActivator.Activated += delegate(object _, EventArgs _)
				{
					this._statusToggle.Deactivate();
				};
			}
			this._blockableObject = base.GetComponent<BlockableObject>();
			if (this._blockableObject)
			{
				this._blockableObject.ObjectBlocked += this.OnObjectBlocked;
				this._blockableObject.ObjectUnblocked += this.OnObjectUnblocked;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025C0 File Offset: 0x000007C0
		public void Start()
		{
			if (!this._timedComponentActivator.IsPastActivationTime)
			{
				string warningSound = this._activationWarningStatusSpec.WarningSound;
				base.GetComponent<StatusSubject>().RegisterDynamicStatus(this._statusToggle, new Func<float>(this.GetDaysLeftUntilActivation), new Func<StatusWarningType>(this.GetStatusWarningType), warningSound);
				this.ActivateToggleIfPossible();
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002618 File Offset: 0x00000818
		public float GetDaysLeftUntilActivation()
		{
			float daysLeftUntilActivation = this._timedComponentActivator.DaysLeftUntilActivation;
			if (!this.IsCloseToActivation())
			{
				return (float)Math.Ceiling((double)daysLeftUntilActivation);
			}
			float daysUntilActivation = this._timedComponentActivator.DaysUntilActivation;
			return (float)Math.Ceiling((double)((daysUntilActivation - daysUntilActivation * this._timedComponentActivator.ActivationProgress) / ActivationWarningStatus.Step)) * ActivationWarningStatus.Step;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000266E File Offset: 0x0000086E
		public bool IsCloseToActivation()
		{
			return this.GetStatusWarningType() > StatusWarningType.None;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002679 File Offset: 0x00000879
		public StatusWarningType GetStatusWarningType()
		{
			if (this._activationWarningStatusSpec.UseInfiniteWarning && this._timedComponentActivator.DaysLeftUntilActivation * 24f <= (float)ActivationWarningStatus.CloseToActivationHoursCount)
			{
				return StatusWarningType.Infinite;
			}
			if (this._timedComponentActivator.DaysLeftUntilActivation > (float)ActivationWarningStatus.CloseToActivationDayCount)
			{
				return StatusWarningType.None;
			}
			return StatusWarningType.Short;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026B9 File Offset: 0x000008B9
		public void OnObjectUnblocked(object sender, EventArgs eventArgs)
		{
			this.ActivateToggleIfPossible();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026C1 File Offset: 0x000008C1
		public void OnObjectBlocked(object sender, EventArgs eventArgs)
		{
			this._statusToggle.Deactivate();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026CE File Offset: 0x000008CE
		public void ActivateToggleIfPossible()
		{
			if (this._timedComponentActivator.CountdownIsActive)
			{
				BlockableObject blockableObject = this._blockableObject;
				if (blockableObject == null || blockableObject.IsUnblocked)
				{
					this._statusToggle.Activate();
				}
			}
		}

		// Token: 0x0400000F RID: 15
		public static readonly int CloseToActivationDayCount = 3;

		// Token: 0x04000010 RID: 16
		public static readonly int CloseToActivationHoursCount = 3;

		// Token: 0x04000011 RID: 17
		public static readonly float Step = 0.1f;

		// Token: 0x04000012 RID: 18
		public static readonly string StatusLocKey = "Status.TimedComponentActivator.Short";

		// Token: 0x04000013 RID: 19
		public readonly ILoc _loc;

		// Token: 0x04000014 RID: 20
		public StatusToggle _statusToggle;

		// Token: 0x04000015 RID: 21
		public ActivationWarningStatusSpec _activationWarningStatusSpec;

		// Token: 0x04000016 RID: 22
		public TimedComponentActivator _timedComponentActivator;

		// Token: 0x04000017 RID: 23
		public BlockableObject _blockableObject;
	}
}
