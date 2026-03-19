using System;
using Timberborn.ApplicationLifetime;
using Timberborn.Autosaving;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.Metrics;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.Benchmarking
{
	// Token: 0x02000004 RID: 4
	public class Benchmarker : IUpdatableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public Benchmarker(PerformanceSampler performanceSampler, BenchmarkLogger benchmarkLogger, SpeedManager speedManager, Autosaver autosaver, EdgePanningCameraTargetPicker edgePanningCameraTargetPicker, EventBus eventBus, IMetricsService metricsService)
		{
			this._performanceSampler = performanceSampler;
			this._benchmarkLogger = benchmarkLogger;
			this._speedManager = speedManager;
			this._autosaver = autosaver;
			this._edgePanningCameraTargetPicker = edgePanningCameraTargetPicker;
			this._eventBus = eventBus;
			this._metricsService = metricsService;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000211E File Offset: 0x0000031E
		public bool WarmUpInProgress
		{
			get
			{
				return this._warmUpFinishTimestamp != float.MaxValue;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		public void UpdateSingleton()
		{
			if (this._enabled)
			{
				this.UpdateBenchmark();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002140 File Offset: 0x00000340
		public float GetTimeLeft()
		{
			float unscaledTime = Time.unscaledTime;
			if (this.WarmUpInProgress)
			{
				return this._warmUpFinishTimestamp - unscaledTime + (float)this._samplingLengthInSeconds;
			}
			if (this.SamplingInProgress)
			{
				return this._samplingFinishTimestamp - unscaledTime;
			}
			return 0f;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002184 File Offset: 0x00000384
		public void StartBenchmark(int samplingLengthInSeconds, int warmUpLengthInSeconds, int gameSpeed)
		{
			this._samplingLengthInSeconds = samplingLengthInSeconds;
			this._warmUpLengthInSeconds = warmUpLengthInSeconds;
			this._gameSpeed = gameSpeed;
			this._speedManager.ChangeSpeed((float)gameSpeed);
			this._edgePanningCameraTargetPicker.Suspend();
			this._autosaver.Suspend();
			this._eventBus.Post(new BenchmarkStartedEvent());
			this._enabled = true;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021E0 File Offset: 0x000003E0
		public bool SamplingInProgress
		{
			get
			{
				return this._samplingFinishTimestamp != float.MaxValue;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021F2 File Offset: 0x000003F2
		public bool TimeToStartWarmUp
		{
			get
			{
				return this._frameCounter == 2;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021FD File Offset: 0x000003FD
		public bool TimeToFinishWarmUp
		{
			get
			{
				return Time.unscaledTime > this._warmUpFinishTimestamp;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000220C File Offset: 0x0000040C
		public bool TimeToFinishSampling
		{
			get
			{
				return Time.unscaledTime > this._samplingFinishTimestamp;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000221C File Offset: 0x0000041C
		public void UpdateBenchmark()
		{
			this._frameCounter++;
			if (this.TimeToStartWarmUp)
			{
				this.StartWarmUp();
			}
			if (this.TimeToFinishWarmUp)
			{
				this.FinishWarmUp();
				this.StartSampling();
				this._metricsService.ResetMetrics();
			}
			if (this.TimeToFinishSampling)
			{
				this.FinishSampling();
				this.FinishBenchmark();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002278 File Offset: 0x00000478
		public void StartWarmUp()
		{
			this._warmUpFinishTimestamp = Time.unscaledTime + (float)this._warmUpLengthInSeconds;
			Debug.Log(string.Format("Started a warm up, will finish in {0} seconds", this._warmUpLengthInSeconds));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022A7 File Offset: 0x000004A7
		public void FinishWarmUp()
		{
			this._warmUpFinishTimestamp = float.MaxValue;
			Debug.Log("Finished a warm up");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022BE File Offset: 0x000004BE
		public void StartSampling()
		{
			this._samplingFinishTimestamp = Time.unscaledTime + (float)this._samplingLengthInSeconds;
			this._performanceSampler.StartSampling(this._samplingLengthInSeconds);
			Debug.Log(string.Format("Started sampling, will finish in {0} seconds", this._samplingLengthInSeconds));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022FE File Offset: 0x000004FE
		public void FinishSampling()
		{
			this._samplingFinishTimestamp = float.MaxValue;
			this._performanceSampler.StopSampling();
			Debug.Log("Finished sampling");
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002320 File Offset: 0x00000520
		public void FinishBenchmark()
		{
			ReadOnlyList<PerformanceSample> samples = this._performanceSampler.Samples;
			bool detailedSamplesAvailable = this._performanceSampler.DetailedSamplesAvailable;
			BenchmarkParameters benchmarkParameters = new BenchmarkParameters(this._samplingLengthInSeconds, this._warmUpLengthInSeconds, this._gameSpeed, samples, detailedSamplesAvailable);
			this._benchmarkLogger.LogBenchmark(benchmarkParameters);
			GameQuitter.Quit();
		}

		// Token: 0x04000006 RID: 6
		public readonly PerformanceSampler _performanceSampler;

		// Token: 0x04000007 RID: 7
		public readonly BenchmarkLogger _benchmarkLogger;

		// Token: 0x04000008 RID: 8
		public readonly SpeedManager _speedManager;

		// Token: 0x04000009 RID: 9
		public readonly Autosaver _autosaver;

		// Token: 0x0400000A RID: 10
		public readonly EdgePanningCameraTargetPicker _edgePanningCameraTargetPicker;

		// Token: 0x0400000B RID: 11
		public readonly EventBus _eventBus;

		// Token: 0x0400000C RID: 12
		public readonly IMetricsService _metricsService;

		// Token: 0x0400000D RID: 13
		public int _samplingLengthInSeconds;

		// Token: 0x0400000E RID: 14
		public int _warmUpLengthInSeconds;

		// Token: 0x0400000F RID: 15
		public int _gameSpeed;

		// Token: 0x04000010 RID: 16
		public int _frameCounter;

		// Token: 0x04000011 RID: 17
		public float _warmUpFinishTimestamp = float.MaxValue;

		// Token: 0x04000012 RID: 18
		public float _samplingFinishTimestamp = float.MaxValue;

		// Token: 0x04000013 RID: 19
		public bool _enabled;
	}
}
