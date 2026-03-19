using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.MultithreadingAnalysis;
using UnityEngine;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x02000009 RID: 9
	public class TaskColorProvider
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000027F4 File Offset: 0x000009F4
		public void InitializeFromSamples(ReadOnlyList<TaskSample> samples)
		{
			this._colorMap.Clear();
			foreach (Type key in from type in (from sample in samples
			select sample.GenericType).Distinct<Type>()
			orderby type.Name
			select type)
			{
				this._colorMap[key] = TaskColorProvider.PredefinedPalette[this._colorMap.Count % TaskColorProvider.PredefinedPalette.Length];
			}
			if (this._colorMap.Count > TaskColorProvider.PredefinedPalette.Length)
			{
				Debug.LogWarning("Exceeded predefined task color palette, colors may repeat.");
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028DC File Offset: 0x00000ADC
		public Color GetColor(Type type)
		{
			return this._colorMap[type];
		}

		// Token: 0x0400001A RID: 26
		public static readonly Color[] PredefinedPalette = new Color[]
		{
			new Color(0.51f, 0.2f, 0.2f),
			new Color(0.48f, 0.34f, 0.2f),
			new Color(0.45f, 0.4f, 0.2f),
			new Color(0.54f, 0.7f, 0.2f),
			new Color(0.2f, 0.65f, 0.3f),
			new Color(0.2f, 0.5f, 0.4f),
			new Color(0.22f, 0.42f, 0.72f),
			new Color(0.22f, 0.36f, 0.56f),
			new Color(0.3f, 0.2f, 0.56f),
			new Color(0.62f, 0.4f, 0.76f),
			new Color(0.52f, 0.2f, 0.46f),
			new Color(0.73f, 0.2f, 0.34f),
			new Color(0.4f, 0.26f, 0.26f),
			new Color(0.38f, 0.38f, 0.38f),
			new Color(0.22f, 0.32f, 0.22f),
			new Color(0.1f, 0.16f, 0.09f),
			new Color(0.38f, 0.38f, 0.52f),
			new Color(0.24f, 0.4f, 0.4f),
			new Color(0.36f, 0.52f, 0.36f),
			new Color(0.52f, 0.36f, 0.3f)
		};

		// Token: 0x0400001B RID: 27
		public readonly Dictionary<Type, Color> _colorMap = new Dictionary<Type, Color>();
	}
}
