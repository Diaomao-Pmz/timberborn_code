using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200000A RID: 10
	public interface IStatusIconOffsetService
	{
		// Token: 0x06000016 RID: 22
		void AddOffsetter(IStatusIconOffsetter offsetter);

		// Token: 0x06000017 RID: 23
		void RemoveOffsetter(IStatusIconOffsetter offsetter);

		// Token: 0x06000018 RID: 24
		void UpdateAffectedStatusSlot(Vector2Int coordinates);

		// Token: 0x06000019 RID: 25
		void UpdateIcons(IStatusIconOffsetter offsetter);

		// Token: 0x0600001A RID: 26
		void UpdatePositions(IStatusIconOffsetter offsetter);

		// Token: 0x0600001B RID: 27
		float CalculateVerticalPosition(IStatusIconOffsetter offsetter);

		// Token: 0x0600001C RID: 28
		void RepositionAllIcons();

		// Token: 0x0600001D RID: 29
		IEnumerable<ValueTuple<StatusSlot, Vector2>> GetAllStatusSlots();

		// Token: 0x0600001E RID: 30
		void EnablePreviewMode();

		// Token: 0x0600001F RID: 31
		void DisablePreviewMode();
	}
}
