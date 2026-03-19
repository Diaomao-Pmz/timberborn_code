using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.TutorialSystemUI
{
	// Token: 0x0200000B RID: 11
	public class TutorialPanelBlinker : IUpdatableSingleton
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000027F4 File Offset: 0x000009F4
		public void StartBlinking(VisualElement root, bool keepBlinking)
		{
			float timeRemaining = keepBlinking ? float.MaxValue : TutorialPanelBlinker.DefaultBlinkLength;
			this._blinkInfos.Add(new TutorialPanelBlinker.BlinkInfo(root, timeRemaining));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002824 File Offset: 0x00000A24
		public void StopBlinking(VisualElement root)
		{
			this._blinkInfos.RemoveAll((TutorialPanelBlinker.BlinkInfo item) => item.Root == root);
			root.RemoveFromClassList(TutorialPanelBlinker.HighlightClass);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002868 File Offset: 0x00000A68
		public void UpdateSingleton()
		{
			for (int i = this._blinkInfos.Count - 1; i >= 0; i--)
			{
				TutorialPanelBlinker.BlinkInfo blinkInfo = this._blinkInfos[i];
				blinkInfo.Root.EnableInClassList(TutorialPanelBlinker.HighlightClass, HighlightTimer.IsTimeForSteadyHighlight());
				float num = blinkInfo.TimeRemaining - Time.unscaledDeltaTime;
				if (num <= 0f)
				{
					this.StopBlinking(blinkInfo.Root);
				}
				else
				{
					this._blinkInfos[i] = new TutorialPanelBlinker.BlinkInfo(blinkInfo.Root, num);
				}
			}
		}

		// Token: 0x04000027 RID: 39
		public static readonly float DefaultBlinkLength = 5f;

		// Token: 0x04000028 RID: 40
		public static readonly string HighlightClass = "tutorial-panel--highlighted";

		// Token: 0x04000029 RID: 41
		public readonly List<TutorialPanelBlinker.BlinkInfo> _blinkInfos = new List<TutorialPanelBlinker.BlinkInfo>();

		// Token: 0x0200000C RID: 12
		public readonly struct BlinkInfo
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000034 RID: 52 RVA: 0x00002917 File Offset: 0x00000B17
			public VisualElement Root { get; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000035 RID: 53 RVA: 0x0000291F File Offset: 0x00000B1F
			public float TimeRemaining { get; }

			// Token: 0x06000036 RID: 54 RVA: 0x00002927 File Offset: 0x00000B27
			public BlinkInfo(VisualElement root, float timeRemaining)
			{
				this.Root = root;
				this.TimeRemaining = timeRemaining;
			}
		}
	}
}
