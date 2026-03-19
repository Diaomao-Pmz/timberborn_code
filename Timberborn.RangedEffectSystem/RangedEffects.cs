using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.Effects;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x02000011 RID: 17
	public class RangedEffects
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002AE5 File Offset: 0x00000CE5
		public ReadOnlyList<RangedEffect> ActiveEffects
		{
			get
			{
				return this._activeEffects.AsReadOnlyList<RangedEffect>();
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002AF2 File Offset: 0x00000CF2
		public IEnumerable<RangedEffectApplier> RangedEffectAppliers
		{
			get
			{
				return this._effects.SelectMany((RangedEffect rangedEffect) => rangedEffect.Appliers);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B20 File Offset: 0x00000D20
		public void Add(RangedEffectApplier rangedEffectApplier)
		{
			foreach (ContinuousEffect effect in rangedEffectApplier.Effects)
			{
				RangedEffect orCreate = this.GetOrCreate(effect);
				orCreate.Add(rangedEffectApplier);
				if (rangedEffectApplier.Active)
				{
					this.AddActiveEffect(orCreate);
				}
			}
			rangedEffectApplier.ActiveChanged += this.OnActiveChanged;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002B80 File Offset: 0x00000D80
		public void Remove(RangedEffectApplier rangedEffectApplier)
		{
			rangedEffectApplier.ActiveChanged -= this.OnActiveChanged;
			foreach (ContinuousEffect effect in rangedEffectApplier.Effects)
			{
				RangedEffect rangedEffect;
				if (this.TryGetRangedEffect(effect, out rangedEffect))
				{
					rangedEffect.Remove(rangedEffectApplier);
					if (!rangedEffect.Appliers.Any<RangedEffectApplier>())
					{
						this._effects.Remove(rangedEffect);
						this._activeEffects.Remove(rangedEffect);
					}
					else
					{
						this.RemoveInactiveEffect(rangedEffect);
					}
				}
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002C04 File Offset: 0x00000E04
		public void OnActiveChanged(object sender, ActiveChangedEventArgs e)
		{
			RangedEffectApplier rangedEffectApplier = (RangedEffectApplier)sender;
			bool state = e.State;
			foreach (ContinuousEffect effect in rangedEffectApplier.Effects)
			{
				RangedEffect effect2;
				if (this.TryGetRangedEffect(effect, out effect2))
				{
					if (state)
					{
						this.AddActiveEffect(effect2);
					}
					else
					{
						this.RemoveInactiveEffect(effect2);
					}
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002C60 File Offset: 0x00000E60
		public void AddActiveEffect(RangedEffect effect)
		{
			if (!this._activeEffects.Contains(effect))
			{
				this._activeEffects.Add(effect);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002C7C File Offset: 0x00000E7C
		public void RemoveInactiveEffect(RangedEffect effect)
		{
			if (!effect.IsActive)
			{
				this._activeEffects.Remove(effect);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C94 File Offset: 0x00000E94
		public RangedEffect GetOrCreate(ContinuousEffect effect)
		{
			RangedEffect result;
			if (this.TryGetRangedEffect(effect, out result))
			{
				return result;
			}
			RangedEffect rangedEffect = new RangedEffect(effect);
			this._effects.Add(rangedEffect);
			return rangedEffect;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public bool TryGetRangedEffect(ContinuousEffect effect, out RangedEffect result)
		{
			foreach (RangedEffect rangedEffect in this._effects)
			{
				if (rangedEffect.BaseEffect == effect)
				{
					result = rangedEffect;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x04000028 RID: 40
		public readonly List<RangedEffect> _effects = new List<RangedEffect>();

		// Token: 0x04000029 RID: 41
		public readonly List<RangedEffect> _activeEffects = new List<RangedEffect>();
	}
}
