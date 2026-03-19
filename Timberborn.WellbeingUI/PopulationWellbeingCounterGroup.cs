using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000014 RID: 20
	public class PopulationWellbeingCounterGroup
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003592 File Offset: 0x00001792
		public VisualElement Root { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000359A File Offset: 0x0000179A
		public List<PopulationWellbeingCounter> Counters { get; }

		// Token: 0x06000066 RID: 102 RVA: 0x000035A2 File Offset: 0x000017A2
		public PopulationWellbeingCounterGroup(VisualElement root, IEnumerable<PopulationWellbeingCounter> counters)
		{
			this.Root = root;
			this.Counters = counters.ToList<PopulationWellbeingCounter>();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000035BD File Offset: 0x000017BD
		public bool HasCounters
		{
			get
			{
				return this.Counters.Count > 0;
			}
		}
	}
}
