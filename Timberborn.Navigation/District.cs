using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200000C RID: 12
	public class District
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002D88 File Offset: 0x00000F88
		internal int CenterNodeId { get; }

		// Token: 0x06000054 RID: 84 RVA: 0x00002D90 File Offset: 0x00000F90
		public District(int centerNodeId, Vector3Int centerCoordinates)
		{
			this.CenterNodeId = centerNodeId;
			this._centerCoordinates = centerCoordinates;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public override string ToString()
		{
			return string.Format("{0}: {1}, ", "_centerCoordinates", this._centerCoordinates) + string.Format("{0}: {1}", "CenterNodeId", this.CenterNodeId);
		}

		// Token: 0x04000020 RID: 32
		public readonly Vector3Int _centerCoordinates;
	}
}
