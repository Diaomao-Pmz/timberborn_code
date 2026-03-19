using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.ZiplineMovementSystem
{
	// Token: 0x0200000F RID: 15
	public class ZiplineVisitorBoundsScaler : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00003164 File Offset: 0x00001364
		public void Awake()
		{
			this._meshRenderers = base.GameObject.GetComponentsInChildren<MeshRenderer>(true).ToImmutableArray<MeshRenderer>();
			ZiplineVisitor component = base.GetComponent<ZiplineVisitor>();
			component.EnteredZipline += this.OnEnteredZipline;
			component.ExitedZipline += this.OnExitedZipline;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000031B4 File Offset: 0x000013B4
		public void OnEnteredZipline(object sender, EventArgs e)
		{
			foreach (MeshRenderer meshRenderer in this._meshRenderers)
			{
				Bounds localBounds = meshRenderer.localBounds;
				localBounds.size *= ZiplineVisitorBoundsScaler.Scale;
				meshRenderer.localBounds = localBounds;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003204 File Offset: 0x00001404
		public void OnExitedZipline(object sender, EventArgs e)
		{
			foreach (MeshRenderer meshRenderer in this._meshRenderers)
			{
				meshRenderer.ResetLocalBounds();
			}
		}

		// Token: 0x04000032 RID: 50
		public static readonly float Scale = 4f;

		// Token: 0x04000033 RID: 51
		public ImmutableArray<MeshRenderer> _meshRenderers;
	}
}
