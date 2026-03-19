using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BeaverContaminationSystem;
using Timberborn.BonusSystem;
using Timberborn.CharacterMovementSystem;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000013 RID: 19
	public class RunningStateUpdater : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public void Awake()
		{
			this._bonusManager = base.GetComponent<BonusManager>();
			this._bonusManager.BonusValueChanged += this.OnBonusValueChanged;
			this._runningProhibitor = base.GetComponent<RunningProhibitor>();
			this._walkingEnforcer = base.GetComponent<WalkingEnforcer>();
			WalkingEnforcer walkingEnforcer = this._walkingEnforcer;
			walkingEnforcer.ForcedWalkingChanged = (EventHandler)Delegate.Combine(walkingEnforcer.ForcedWalkingChanged, new EventHandler(this.OnForcedWalkingChanged));
			this._contaminable = base.GetComponent<Contaminable>();
			this._runningStateUpdaterSpec = base.GetComponent<RunningStateUpdaterSpec>();
			if (this._contaminable)
			{
				this._contaminable.ContaminationChanged += this.OnContaminationChanged;
			}
			base.GetComponent<Walker>().StartedNewPath += this.OnStartedNewPath;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B66 File Offset: 0x00000D66
		public void OnStartedNewPath(object sender, StartedNewPathEventArgs e)
		{
			this._isPathShort = (e.Distance < this._runningStateUpdaterSpec.ShortWalkingDistanceThreshold);
			this.UpdateRunningProhibition();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B88 File Offset: 0x00000D88
		public void OnBonusValueChanged(object sender, BonusValueChangedEventArgs e)
		{
			if (e.BonusId == RunningStateUpdater.MovementSpeedBonusName)
			{
				this._isWalkingSpeedTooLow = (e.Value < this._runningStateUpdaterSpec.WalkingSpeedThreshold);
				this.UpdateRunningProhibition();
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BBD File Offset: 0x00000DBD
		public void OnForcedWalkingChanged(object sender, EventArgs e)
		{
			this.UpdateRunningProhibition();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BBD File Offset: 0x00000DBD
		public void OnContaminationChanged(object sender, EventArgs e)
		{
			this.UpdateRunningProhibition();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public void UpdateRunningProhibition()
		{
			this._runningProhibitor.RunningProhibited = (this._isPathShort || this._isWalkingSpeedTooLow || this._walkingEnforcer.ForcedWalking || (this._contaminable && this._contaminable.IsContaminated));
		}

		// Token: 0x04000024 RID: 36
		public static readonly string MovementSpeedBonusName = "MovementSpeed";

		// Token: 0x04000025 RID: 37
		public BonusManager _bonusManager;

		// Token: 0x04000026 RID: 38
		public RunningProhibitor _runningProhibitor;

		// Token: 0x04000027 RID: 39
		public WalkingEnforcer _walkingEnforcer;

		// Token: 0x04000028 RID: 40
		public Contaminable _contaminable;

		// Token: 0x04000029 RID: 41
		public RunningStateUpdaterSpec _runningStateUpdaterSpec;

		// Token: 0x0400002A RID: 42
		public bool _isPathShort;

		// Token: 0x0400002B RID: 43
		public bool _isWalkingSpeedTooLow;
	}
}
