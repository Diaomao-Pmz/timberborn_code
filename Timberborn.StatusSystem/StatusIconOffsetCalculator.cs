using System;
using Timberborn.Common;
using Timberborn.LevelVisibilitySystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000017 RID: 23
	public class StatusIconOffsetCalculator
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00002EC3 File Offset: 0x000010C3
		public StatusIconOffsetCalculator(StatusSlotUpdateService statusSlotUpdateService, ILevelVisibilityService levelVisibilityService)
		{
			this._statusSlotUpdateService = statusSlotUpdateService;
			this._levelVisibilityService = levelVisibilityService;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002ED9 File Offset: 0x000010D9
		public float CalculatePreviewVerticalPosition(ReadOnlyList<IStatusIconOffsetter> offsetters, IStatusIconOffsetter offsetter)
		{
			this._previewMode = true;
			float result = this.CalculateVerticalPosition(offsetters, offsetter);
			this._previewMode = false;
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002EF4 File Offset: 0x000010F4
		public float CalculateVerticalPosition(ReadOnlyList<IStatusIconOffsetter> offsetters, IStatusIconOffsetter offsetter)
		{
			ReadOnlyList<StatusSlot> statusSlots = this._statusSlotUpdateService.GetStatusSlots(offsetter.Key);
			int num = this.GetFirstAvailableSlotIndex(offsetter, offsetters, statusSlots);
			if (num >= statusSlots.Count)
			{
				return statusSlots[statusSlots.Count - 1].ZCoordinate + Mathf.Max(StatusIconOffsetCalculator.OffsetFromMaxLevel, (float)(num - statusSlots.Count));
			}
			num = this.GetFirstSlotIndexAboveBase(statusSlots, offsetter, num);
			if (num >= statusSlots.Count)
			{
				return statusSlots[statusSlots.Count - 1].ZCoordinate + StatusIconOffsetCalculator.OffsetFromMaxLevel;
			}
			return statusSlots[num].ZCoordinate;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002F98 File Offset: 0x00001198
		public int GetFirstAvailableSlotIndex(IStatusIconOffsetter currentOffsetter, ReadOnlyList<IStatusIconOffsetter> offsetters, ReadOnlyList<StatusSlot> slots)
		{
			int num = this.GetNextSlotIndex(slots, -1, 1);
			foreach (IStatusIconOffsetter statusIconOffsetter in offsetters)
			{
				if (StatusIconOffsetCalculator.IsActiveAndBelow(currentOffsetter, statusIconOffsetter))
				{
					if (num < slots.Count)
					{
						num = this.GetFirstFreeSlotIndexAboveOffsetter(slots, num, statusIconOffsetter);
					}
					else
					{
						num = this.GetNextSlotIndex(slots, num, 1);
					}
				}
			}
			return num;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003014 File Offset: 0x00001214
		public int GetFirstSlotIndexAboveBase(ReadOnlyList<StatusSlot> slots, IStatusIconOffsetter offsetter, int currentSlotIndex)
		{
			while (currentSlotIndex < slots.Count && (slots[currentSlotIndex].ZCoordinate < offsetter.TopBound || this.SkipInvalidSlots(slots, currentSlotIndex)))
			{
				currentSlotIndex = this.GetNextSlotIndex(slots, currentSlotIndex, 1);
			}
			return currentSlotIndex;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000305C File Offset: 0x0000125C
		public int GetFirstFreeSlotIndexAboveOffsetter(ReadOnlyList<StatusSlot> slots, int currentSlotIndex, IStatusIconOffsetter statusIconOffsetter)
		{
			while (currentSlotIndex < slots.Count && slots[currentSlotIndex].ZCoordinate < (float)statusIconOffsetter.BlockObject.CoordinatesAtBaseZ.z)
			{
				currentSlotIndex = this.GetNextSlotIndex(slots, currentSlotIndex, 1);
			}
			return this.GetNextSlotIndex(slots, currentSlotIndex, StatusIconOffsetCalculator.NextIndexOffsetAfterUsedSlot);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030B3 File Offset: 0x000012B3
		public int GetNextSlotIndex(ReadOnlyList<StatusSlot> slots, int currentSlotIndex, int change = 1)
		{
			currentSlotIndex += change;
			while (currentSlotIndex < slots.Count && this.SkipInvalidSlots(slots, currentSlotIndex))
			{
				currentSlotIndex++;
			}
			return currentSlotIndex;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030D6 File Offset: 0x000012D6
		public static bool IsActiveAndBelow(IStatusIconOffsetter currentOffsetter, IStatusIconOffsetter offsetter)
		{
			return offsetter.StatusActive && offsetter.Position.z < currentOffsetter.Position.z;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000030FC File Offset: 0x000012FC
		public bool SkipInvalidSlots(ReadOnlyList<StatusSlot> slots, int currentIndex)
		{
			StatusSlot statusSlot = slots[currentIndex];
			return (this._previewMode && statusSlot.InvalidInConstructionMode && (int)statusSlot.UnfinishedBaseZ <= this._levelVisibilityService.MaxVisibleLevel) || (int)statusSlot.BaseZ <= this._levelVisibilityService.MaxVisibleLevel;
		}

		// Token: 0x04000040 RID: 64
		public static readonly float OffsetFromMaxLevel = 1f;

		// Token: 0x04000041 RID: 65
		public static readonly int NextIndexOffsetAfterUsedSlot = 2;

		// Token: 0x04000042 RID: 66
		public readonly StatusSlotUpdateService _statusSlotUpdateService;

		// Token: 0x04000043 RID: 67
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000044 RID: 68
		public bool _previewMode;
	}
}
