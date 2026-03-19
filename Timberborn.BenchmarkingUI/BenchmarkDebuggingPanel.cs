using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.Benchmarking;
using Timberborn.Common;
using Timberborn.DebuggingUI;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BenchmarkingUI
{
	// Token: 0x02000004 RID: 4
	public class BenchmarkDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BC File Offset: 0x000002BC
		public BenchmarkDebuggingPanel(DebuggingPanel debuggingPanel)
		{
			this._debuggingPanel = debuggingPanel;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002123 File Offset: 0x00000323
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Performance");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002138 File Offset: 0x00000338
		public string GetText()
		{
			this._frameSampler.UpdateSamples();
			this._cpuTotalTimes.Add(this._frameSampler.CpuTotalTime);
			this._cpuMainThreadTimes.Add(this._frameSampler.CpuMainThreadTime);
			this._cpuRenderThreadTimes.Add(this._frameSampler.CpuRenderThreadTime);
			this._cpuWaitTimes.Add(this._frameSampler.CpuWaitTime);
			this._gpuTimes.Add(this._frameSampler.GpuTime);
			if (Time.unscaledTime > this._lastMeasurementTime + BenchmarkDebuggingPanel.UpdateInterval)
			{
				this._lastMeasurementTime = Time.unscaledTime;
				this._description.Clear();
				this.AddTimeValue("CPU (Total)", this._cpuTotalTimes);
				this.AddTimeValue("CPU (Main)", this._cpuMainThreadTimes);
				this.AddTimeValue("CPU (Render)", this._cpuRenderThreadTimes);
				this.AddTimeValue("CPU (Wait)", this._cpuWaitTimes);
				this.AddTimeValue("GPU", this._gpuTimes);
				return this._description.ToStringWithoutNewLineEnd();
			}
			return null;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000224C File Offset: 0x0000044C
		public void AddTimeValue(string text, IList<float> values)
		{
			double num = 1000.0 * BenchmarkDebuggingPanel.GetAverageAndClear(values);
			this._description.AppendLine(string.Format("{0}: {1:0.0}ms", text, num));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002288 File Offset: 0x00000488
		public static double GetAverageAndClear(IList<float> values)
		{
			if (values.Count == 0)
			{
				return 0.0;
			}
			double num = 0.0;
			for (int i = 0; i < values.Count; i++)
			{
				num += (double)values[i];
			}
			double result = num / (double)values.Count;
			values.Clear();
			return result;
		}

		// Token: 0x04000006 RID: 6
		public static readonly float UpdateInterval = 0.5f;

		// Token: 0x04000007 RID: 7
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000008 RID: 8
		public readonly List<float> _cpuMainThreadTimes = new List<float>();

		// Token: 0x04000009 RID: 9
		public readonly List<float> _cpuRenderThreadTimes = new List<float>();

		// Token: 0x0400000A RID: 10
		public readonly List<float> _cpuWaitTimes = new List<float>();

		// Token: 0x0400000B RID: 11
		public readonly List<float> _cpuTotalTimes = new List<float>();

		// Token: 0x0400000C RID: 12
		public readonly List<float> _gpuTimes = new List<float>();

		// Token: 0x0400000D RID: 13
		public readonly FrameTimingSampler _frameSampler = new FrameTimingSampler();

		// Token: 0x0400000E RID: 14
		public readonly StringBuilder _description = new StringBuilder();

		// Token: 0x0400000F RID: 15
		public float _lastMeasurementTime;
	}
}
