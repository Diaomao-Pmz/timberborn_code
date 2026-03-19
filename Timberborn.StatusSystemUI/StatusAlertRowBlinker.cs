using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.StatusSystemUI
{
	// Token: 0x02000008 RID: 8
	public class StatusAlertRowBlinker : IUpdatableSingleton
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002652 File Offset: 0x00000852
		public void StartInfiniteBlinking(StatusAlertFragmentRow row)
		{
			this.StartBlinkingInternal(row, int.MaxValue);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002660 File Offset: 0x00000860
		public void StartShortBlinking(StatusAlertFragmentRow row)
		{
			this.StartBlinkingInternal(row, StatusAlertRowBlinker.BliksCount);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002670 File Offset: 0x00000870
		public void UpdateSingleton()
		{
			for (int i = this._activeRows.Count - 1; i >= 0; i--)
			{
				StatusAlertRowBlinker.RowBlinkData rowBlinkData = this._activeRows[i];
				StatusAlertFragmentRow row = rowBlinkData.Row;
				if (rowBlinkData.NextToggleTime <= Time.unscaledTime)
				{
					row.ToggleHighlight();
					if (rowBlinkData.BlinksRemaining <= 0)
					{
						this._activeRows.RemoveAt(i);
					}
					else
					{
						this._activeRows[i] = rowBlinkData.UpdatedData();
					}
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026EC File Offset: 0x000008EC
		public void StopBlinking(StatusAlertFragmentRow row)
		{
			for (int i = this._activeRows.Count - 1; i >= 0; i--)
			{
				StatusAlertFragmentRow row2 = this._activeRows[i].Row;
				if (row == row2)
				{
					this._activeRows.RemoveAt(i);
					row2.DisableHighlight();
					return;
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000273D File Offset: 0x0000093D
		public void StartBlinkingInternal(StatusAlertFragmentRow row, int blinkCount)
		{
			this.StopBlinking(row);
			this._activeRows.Add(new StatusAlertRowBlinker.RowBlinkData(row, blinkCount, Time.unscaledTime + StatusAlertRowBlinker.BlinkInterval));
			row.ToggleHighlight();
		}

		// Token: 0x04000022 RID: 34
		public static readonly float BlinkInterval = 0.44f;

		// Token: 0x04000023 RID: 35
		public static readonly int BliksCount = 10;

		// Token: 0x04000024 RID: 36
		public readonly List<StatusAlertRowBlinker.RowBlinkData> _activeRows = new List<StatusAlertRowBlinker.RowBlinkData>();

		// Token: 0x02000009 RID: 9
		public readonly struct RowBlinkData
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000023 RID: 35 RVA: 0x0000278F File Offset: 0x0000098F
			public StatusAlertFragmentRow Row { get; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000024 RID: 36 RVA: 0x00002797 File Offset: 0x00000997
			public int BlinksRemaining { get; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000025 RID: 37 RVA: 0x0000279F File Offset: 0x0000099F
			public float NextToggleTime { get; }

			// Token: 0x06000026 RID: 38 RVA: 0x000027A7 File Offset: 0x000009A7
			public RowBlinkData(StatusAlertFragmentRow row, int blinksRemaining, float nextToggleTime)
			{
				this.Row = row;
				this.BlinksRemaining = blinksRemaining;
				this.NextToggleTime = nextToggleTime;
			}

			// Token: 0x06000027 RID: 39 RVA: 0x000027BE File Offset: 0x000009BE
			public StatusAlertRowBlinker.RowBlinkData UpdatedData()
			{
				return new StatusAlertRowBlinker.RowBlinkData(this.Row, this.BlinksRemaining - 1, this.NextToggleTime + StatusAlertRowBlinker.BlinkInterval);
			}
		}
	}
}
