using System;
using Timberborn.Coordinates;
using Timberborn.Debugging;
using Timberborn.SingletonSystem;
using Timberborn.StatusSystem;
using UnityEngine;

namespace Timberborn.StatusSystemUI
{
	// Token: 0x0200000E RID: 14
	public class StatusSystemSlotsDrawer : IDevModule, IUpdatableSingleton
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002B65 File Offset: 0x00000D65
		public StatusSystemSlotsDrawer(IStatusIconOffsetService statusIconOffsetService)
		{
			this._statusIconOffsetService = statusIconOffsetService;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B74 File Offset: 0x00000D74
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle status slots", new Action(this.ToggleStatusSlots))).Build();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B9B File Offset: 0x00000D9B
		public void UpdateSingleton()
		{
			if (this._showingSlots)
			{
				this.DrawStatusSlots();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BAB File Offset: 0x00000DAB
		public void ToggleStatusSlots()
		{
			this._showingSlots = !this._showingSlots;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BBC File Offset: 0x00000DBC
		public void DrawStatusSlots()
		{
			foreach (ValueTuple<StatusSlot, Vector2> valueTuple in this._statusIconOffsetService.GetAllStatusSlots())
			{
				StatusSlot item = valueTuple.Item1;
				Vector2 item2 = valueTuple.Item2;
				StatusSystemSlotsDrawer.DrawStatusSlot(item, item2);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C1C File Offset: 0x00000E1C
		public static void DrawStatusSlot(StatusSlot statusSlot, Vector2 position2D)
		{
			Vector3 vector = CoordinateSystem.GridToWorld(new Vector3(position2D.x, position2D.y, statusSlot.ZCoordinate));
			Vector3 vector2 = vector + new Vector3(-StatusSystemSlotsDrawer.HorizontalLinesLength, 0f, 0f);
			Vector3 vector3 = vector + new Vector3(StatusSystemSlotsDrawer.HorizontalLinesLength, 0f, 0f);
			Vector3 vector4 = vector + new Vector3(0f, -StatusSystemSlotsDrawer.VerticalLineLength, 0f);
			Vector3 vector5 = vector + new Vector3(0f, StatusSystemSlotsDrawer.VerticalLineLength, 0f);
			Vector3 vector6 = vector + new Vector3(0f, 0f, -StatusSystemSlotsDrawer.HorizontalLinesLength);
			Vector3 vector7 = vector + new Vector3(0f, 0f, StatusSystemSlotsDrawer.HorizontalLinesLength);
			Color color = statusSlot.InvalidInConstructionMode ? Color.cyan : Color.blue;
			if (statusSlot.BaseZ != 255)
			{
				color = Color.red;
			}
			Debug.DrawLine(vector2, vector3, color);
			Debug.DrawLine(vector4, vector5, color);
			Debug.DrawLine(vector6, vector7, color);
		}

		// Token: 0x04000034 RID: 52
		public static readonly float VerticalLineLength = 0.4f;

		// Token: 0x04000035 RID: 53
		public static readonly float HorizontalLinesLength = 0.2f;

		// Token: 0x04000036 RID: 54
		public readonly IStatusIconOffsetService _statusIconOffsetService;

		// Token: 0x04000037 RID: 55
		public bool _showingSlots;
	}
}
