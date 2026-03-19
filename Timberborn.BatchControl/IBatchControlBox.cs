using System;

namespace Timberborn.BatchControl
{
	// Token: 0x0200001F RID: 31
	public interface IBatchControlBox
	{
		// Token: 0x060000B3 RID: 179
		void OpenBatchControlBox();

		// Token: 0x060000B4 RID: 180
		void OpenCharactersTab();

		// Token: 0x060000B5 RID: 181
		void OpenHousingTab();

		// Token: 0x060000B6 RID: 182
		void OpenWorkplacesTab();

		// Token: 0x060000B7 RID: 183
		void OpenMigrationTab();

		// Token: 0x060000B8 RID: 184
		void OpenDistributionTab();

		// Token: 0x060000B9 RID: 185
		void OpenTab(int index);
	}
}
