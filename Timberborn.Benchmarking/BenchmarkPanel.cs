using System;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.Benchmarking
{
	// Token: 0x02000007 RID: 7
	public class BenchmarkPanel : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000275A File Offset: 0x0000095A
		public BenchmarkPanel(UILayout uiLayout, VisualElementLoader visualElementLoader, Benchmarker benchmarker, EventBus eventBus)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._benchmarker = benchmarker;
			this._eventBus = eventBus;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002780 File Offset: 0x00000980
		public void Load()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/BenchmarkPanel");
			this._info = UQueryExtensions.Q<Label>(visualElement, "Info", null);
			this._info.ToggleDisplayStyle(false);
			this._uiLayout.AddAbsoluteItem(visualElement);
			this._eventBus.Register(this);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027D4 File Offset: 0x000009D4
		public void UpdateSingleton()
		{
			if (this._benchmarkStarted)
			{
				this._info.text = string.Format("Benchmark time left: {0:0.0}s", this._benchmarker.GetTimeLeft()) + "\nStage: " + (this._benchmarker.WarmUpInProgress ? "warm up" : "sampling");
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002831 File Offset: 0x00000A31
		[OnEvent]
		public void OnBenchmarkStarted(BenchmarkStartedEvent benchmarkStartedEvent)
		{
			this._benchmarkStarted = true;
			this._info.ToggleDisplayStyle(true);
		}

		// Token: 0x0400001A RID: 26
		public readonly UILayout _uiLayout;

		// Token: 0x0400001B RID: 27
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001C RID: 28
		public readonly Benchmarker _benchmarker;

		// Token: 0x0400001D RID: 29
		public readonly EventBus _eventBus;

		// Token: 0x0400001E RID: 30
		public Label _info;

		// Token: 0x0400001F RID: 31
		public bool _benchmarkStarted;
	}
}
