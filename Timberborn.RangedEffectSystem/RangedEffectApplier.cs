using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Effects;
using Timberborn.NeedSpecs;
using UnityEngine;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x0200000E RID: 14
	public class RangedEffectApplier : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000032 RID: 50 RVA: 0x00002598 File Offset: 0x00000798
		// (remove) Token: 0x06000033 RID: 51 RVA: 0x000025D0 File Offset: 0x000007D0
		public event EventHandler<ActiveChangedEventArgs> ActiveChanged;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002605 File Offset: 0x00000805
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000260D File Offset: 0x0000080D
		public bool Active { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002616 File Offset: 0x00000816
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000261E File Offset: 0x0000081E
		public float Efficiency { get; private set; } = 1f;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002627 File Offset: 0x00000827
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000262F File Offset: 0x0000082F
		public ImmutableArray<ContinuousEffect> Effects { get; private set; }

		// Token: 0x0600003A RID: 58 RVA: 0x00002638 File Offset: 0x00000838
		public RangedEffectApplier(RangedEffectService rangedEffectService)
		{
			this._rangedEffectService = rangedEffectService;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002652 File Offset: 0x00000852
		public void Awake()
		{
			base.DisableComponent();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000265C File Offset: 0x0000085C
		public void Enable(IEnumerable<ContinuousEffectSpec> specs, IEnumerable<Vector2Int> radius, bool active)
		{
			this.Effects = specs.Select(new Func<ContinuousEffectSpec, ContinuousEffect>(ContinuousEffect.FromSpec)).ToImmutableArray<ContinuousEffect>();
			this._effectAreaCoords = radius.ToImmutableArray<Vector2Int>();
			this._rangedEffectService.SetApplier(this);
			base.EnableComponent();
			this.UpdateActiveState(active);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026AB File Offset: 0x000008AB
		public void Disable()
		{
			this._rangedEffectService.UnsetApplier(this);
			base.DisableComponent();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026BF File Offset: 0x000008BF
		public void UpdateActiveState(bool active)
		{
			this.Active = active;
			EventHandler<ActiveChangedEventArgs> activeChanged = this.ActiveChanged;
			if (activeChanged == null)
			{
				return;
			}
			activeChanged(this, new ActiveChangedEventArgs(active));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026DF File Offset: 0x000008DF
		public void UpdateEfficiency(float efficiency)
		{
			this.Efficiency = efficiency;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026E8 File Offset: 0x000008E8
		public IEnumerable<Vector2Int> EffectAreaCoords()
		{
			return this._effectAreaCoords;
		}

		// Token: 0x0400001B RID: 27
		public readonly RangedEffectService _rangedEffectService;

		// Token: 0x0400001C RID: 28
		public ImmutableArray<Vector2Int> _effectAreaCoords;
	}
}
