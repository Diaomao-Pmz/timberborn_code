using System;
using System.Collections.Generic;
using Timberborn.Beavers;

namespace Timberborn.BeaverContaminationSystem
{
	// Token: 0x02000004 RID: 4
	public class BeaverContaminationRegistry
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public int NumberOfContaminatedAdults
		{
			get
			{
				return this._contaminatedAdults.Count;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		public int NumberOfContaminatedChildren
		{
			get
			{
				return this._contaminatedChildren.Count;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DA File Offset: 0x000002DA
		public void AddContaminable(Contaminable contaminable)
		{
			contaminable.ContaminationChanged += this.OnContaminationChanged;
			this.UpdateContaminated(contaminable);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F5 File Offset: 0x000002F5
		public void RemoveContaminable(Contaminable contaminable)
		{
			contaminable.ContaminationChanged -= this.OnContaminationChanged;
			this._contaminatedChildren.Remove(contaminable);
			this._contaminatedAdults.Remove(contaminable);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002123 File Offset: 0x00000323
		public void OnContaminationChanged(object sender, EventArgs e)
		{
			this.UpdateContaminated((Contaminable)sender);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002131 File Offset: 0x00000331
		public void UpdateContaminated(Contaminable contaminable)
		{
			BeaverContaminationRegistry.UpdateContaminated(contaminable, contaminable.GetComponent<Child>() ? this._contaminatedChildren : this._contaminatedAdults);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002154 File Offset: 0x00000354
		public static void UpdateContaminated(Contaminable contaminable, ICollection<Contaminable> contaminated)
		{
			if (contaminable.IsContaminated)
			{
				contaminated.Add(contaminable);
				return;
			}
			contaminated.Remove(contaminable);
		}

		// Token: 0x04000006 RID: 6
		public readonly List<Contaminable> _contaminatedAdults = new List<Contaminable>();

		// Token: 0x04000007 RID: 7
		public readonly List<Contaminable> _contaminatedChildren = new List<Contaminable>();
	}
}
