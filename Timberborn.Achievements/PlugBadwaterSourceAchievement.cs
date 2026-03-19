using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AchievementSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.TemplateSystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000038 RID: 56
	public abstract class PlugBadwaterSourceAchievement : Achievement
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004136 File Offset: 0x00002336
		public override string Id { get; }

		// Token: 0x060000EC RID: 236 RVA: 0x0000413E File Offset: 0x0000233E
		public PlugBadwaterSourceAchievement(EntityComponentRegistry entityComponentRegistry, bool mustPlugAll, string id)
		{
			this._entityComponentRegistry = entityComponentRegistry;
			this._mustPlugAll = mustPlugAll;
			this.Id = id;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004168 File Offset: 0x00002368
		public override void EnableInternal()
		{
			this._waterSources.AddRange(from source in this._entityComponentRegistry.GetEnabled<WaterSource>()
			where source.GetComponent<TemplateSpec>().TemplateName == PlugBadwaterSourceAchievement.BadwaterSourceTemplate
			select source);
			if (this.IsPlugConditionValidated())
			{
				base.Unlock();
				return;
			}
			foreach (WaterSource waterSource in this._waterSources)
			{
				waterSource.WaterStrengthModifierAdded += this.OnWaterStrengthModifierAdded;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004210 File Offset: 0x00002410
		public override void DisableInternal()
		{
			foreach (WaterSource waterSource in this._waterSources)
			{
				waterSource.WaterStrengthModifierAdded -= this.OnWaterStrengthModifierAdded;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000426C File Offset: 0x0000246C
		public void OnWaterStrengthModifierAdded(object sender, EventArgs eventArgs)
		{
			if (this.IsPlugConditionValidated())
			{
				base.Unlock();
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000427C File Offset: 0x0000247C
		public bool IsPlugConditionValidated()
		{
			return this._waterSources.Count > 0 && ((this._mustPlugAll && this._waterSources.FastAll(new Predicate<WaterSource>(PlugBadwaterSourceAchievement.IsPlugged))) || (!this._mustPlugAll && this._waterSources.FastAny(new Predicate<WaterSource>(PlugBadwaterSourceAchievement.IsPlugged))));
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000042E0 File Offset: 0x000024E0
		public static bool IsPlugged(WaterSource waterSource)
		{
			foreach (IWaterStrengthModifier waterStrengthModifier in waterSource.WaterStrengthModifiers)
			{
				if (waterStrengthModifier is WaterSourceDisabler)
				{
					goto IL_33;
				}
				WaterSourceRegulator waterSourceRegulator = waterStrengthModifier as WaterSourceRegulator;
				if (waterSourceRegulator != null && !waterSourceRegulator.IsOpen)
				{
					goto IL_33;
				}
				bool flag = false;
				IL_3B:
				if (flag)
				{
					return true;
				}
				continue;
				IL_33:
				flag = true;
				goto IL_3B;
			}
			return false;
		}

		// Token: 0x04000080 RID: 128
		public static readonly string BadwaterSourceTemplate = "BadwaterSource";

		// Token: 0x04000082 RID: 130
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x04000083 RID: 131
		public readonly List<WaterSource> _waterSources = new List<WaterSource>();

		// Token: 0x04000084 RID: 132
		public readonly bool _mustPlugAll;
	}
}
