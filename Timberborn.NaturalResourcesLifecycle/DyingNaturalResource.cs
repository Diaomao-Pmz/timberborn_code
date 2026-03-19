using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;

namespace Timberborn.NaturalResourcesLifecycle
{
	// Token: 0x02000004 RID: 4
	public class DyingNaturalResource : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public event EventHandler StartedDying;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002168 File Offset: 0x00000368
		public event EventHandler StoppedDying;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000219D File Offset: 0x0000039D
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000021A5 File Offset: 0x000003A5
		public bool IsDying { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x000021B0 File Offset: 0x000003B0
		public void Awake()
		{
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			base.GetComponents<IDyingProgressProvider>(this._dyingProgressProviders);
			foreach (IDyingProgressProvider dyingProgressProvider in this._dyingProgressProviders)
			{
				dyingProgressProvider.StartedDying += this.OnStartedDying;
				dyingProgressProvider.StoppedDying += this.OnStoppedDying;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002238 File Offset: 0x00000438
		public DyingProgress GetClosestDyingProgress()
		{
			if (this._livingNaturalResource.IsDead)
			{
				return DyingProgress.Dead;
			}
			DyingProgress result = DyingProgress.Healthy;
			foreach (IDyingProgressProvider dyingProgressProvider in this._dyingProgressProviders)
			{
				DyingProgress dyingProgress = dyingProgressProvider.DyingProgress;
				if (dyingProgress.IsDying && dyingProgress.DaysLeft < result.DaysLeft)
				{
					result = dyingProgress;
				}
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022C0 File Offset: 0x000004C0
		public void OnStartedDying(object sender, EventArgs e)
		{
			if (!this.IsDying)
			{
				this.IsDying = true;
				EventHandler startedDying = this.StartedDying;
				if (startedDying == null)
				{
					return;
				}
				startedDying(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022E7 File Offset: 0x000004E7
		public void OnStoppedDying(object sender, EventArgs e)
		{
			if (this.IsDying && this.NoProviderIsDying())
			{
				this.IsDying = false;
				EventHandler stoppedDying = this.StoppedDying;
				if (stoppedDying == null)
				{
					return;
				}
				stoppedDying(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002318 File Offset: 0x00000518
		public bool NoProviderIsDying()
		{
			using (List<IDyingProgressProvider>.Enumerator enumerator = this._dyingProgressProviders.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.DyingProgress.IsDying)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000009 RID: 9
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x0400000A RID: 10
		public readonly List<IDyingProgressProvider> _dyingProgressProviders = new List<IDyingProgressProvider>();
	}
}
