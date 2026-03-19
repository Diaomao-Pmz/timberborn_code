using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.ApplicationLifetime;
using Timberborn.Autosaving;
using Timberborn.CameraSystem;
using Timberborn.GameSaveRuntimeSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Benchmarking
{
	// Token: 0x02000013 RID: 19
	public class SavingBenchmarker : IUpdatableSingleton
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000034BC File Offset: 0x000016BC
		public SavingBenchmarker(Autosaver autosaver, GameSaver gameSaver, EdgePanningCameraTargetPicker edgePanningCameraTargetPicker, StatisticsCalculator statisticsCalculator)
		{
			this._autosaver = autosaver;
			this._gameSaver = gameSaver;
			this._edgePanningCameraTargetPicker = edgePanningCameraTargetPicker;
			this._statisticsCalculator = statisticsCalculator;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000034EC File Offset: 0x000016EC
		public void UpdateSingleton()
		{
			if (this._enabled)
			{
				this.UpdateBenchmark();
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000034FC File Offset: 0x000016FC
		public void StartBenchmark(int warmUpLengthInSeconds, int saveCount)
		{
			this._warmUpLengthInSeconds = warmUpLengthInSeconds;
			this._saveCount = saveCount;
			this._edgePanningCameraTargetPicker.Suspend();
			this._autosaver.Suspend();
			this.StartWarmUp();
			this._enabled = true;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000352F File Offset: 0x0000172F
		public bool WarmUpFinished
		{
			get
			{
				return Time.unscaledTime > this._warmUpFinishTimestamp;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000353E File Offset: 0x0000173E
		public void UpdateBenchmark()
		{
			if (this.WarmUpFinished)
			{
				this.Save();
				GameQuitter.Quit();
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003553 File Offset: 0x00001753
		public void StartWarmUp()
		{
			this._warmUpFinishTimestamp = Time.unscaledTime + (float)this._warmUpLengthInSeconds;
			Debug.Log(string.Format("Started a warm up, will finish in {0} seconds", this._warmUpLengthInSeconds));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003584 File Offset: 0x00001784
		public void Save()
		{
			Debug.Log("Saving...");
			ImmutableArray<float> immutableArray = (from timeSpan in this._gameSaver.BenchmarkSavingToMemory(this._saveCount)
			select (float)timeSpan.TotalSeconds).ToImmutableArray<float>();
			Debug.Log(string.Concat(new string[]
			{
				"Finished saving benchmark:",
				string.Format("\n  Number of saves: {0}", immutableArray.Length),
				string.Format("\n  Average: {0:0.00}s", immutableArray.Average()),
				string.Format("\n  Median: {0:0.00}s", this._statisticsCalculator.Median(immutableArray)),
				string.Format("\n  90th: {0:0.00}s", this._statisticsCalculator.Percentile(immutableArray, 90f)),
				string.Format("\n  Min: {0:0.00}s", immutableArray.Min()),
				string.Format("\n  Max: {0:0.00}s", immutableArray.Max())
			}));
		}

		// Token: 0x04000054 RID: 84
		public readonly Autosaver _autosaver;

		// Token: 0x04000055 RID: 85
		public readonly GameSaver _gameSaver;

		// Token: 0x04000056 RID: 86
		public readonly EdgePanningCameraTargetPicker _edgePanningCameraTargetPicker;

		// Token: 0x04000057 RID: 87
		public readonly StatisticsCalculator _statisticsCalculator;

		// Token: 0x04000058 RID: 88
		public int _warmUpLengthInSeconds;

		// Token: 0x04000059 RID: 89
		public int _saveCount;

		// Token: 0x0400005A RID: 90
		public float _warmUpFinishTimestamp = float.MaxValue;

		// Token: 0x0400005B RID: 91
		public bool _enabled;
	}
}
