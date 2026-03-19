using System;
using Timberborn.AchievementSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000034 RID: 52
	public abstract class PlantTreesAchievement : Achievement
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x0000409C File Offset: 0x0000229C
		public PlantTreesAchievement(TreePlantingCounter treePlantingCounter, int threshold)
		{
			this._treePlantingCounter = treePlantingCounter;
			this._threshold = threshold;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000040B2 File Offset: 0x000022B2
		public override string Id
		{
			get
			{
				return string.Format("PLANT_{0}_TREES", this._threshold);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000040C9 File Offset: 0x000022C9
		public override void EnableInternal()
		{
			this._treePlantingCounter.CountChanged += this.OnCountChanged;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000040E2 File Offset: 0x000022E2
		public override void DisableInternal()
		{
			this._treePlantingCounter.CountChanged -= this.OnCountChanged;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000040FB File Offset: 0x000022FB
		public void OnCountChanged(object sender, int plantedCount)
		{
			if (plantedCount >= this._threshold)
			{
				base.Unlock();
			}
		}

		// Token: 0x0400007E RID: 126
		public readonly TreePlantingCounter _treePlantingCounter;

		// Token: 0x0400007F RID: 127
		public readonly int _threshold;
	}
}
