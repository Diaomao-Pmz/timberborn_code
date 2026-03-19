using System;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public class VertexAnimation
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00003C46 File Offset: 0x00001E46
		public VertexAnimation(string name, int frameCount, int animatedVertexCount, Texture offsets, Texture rotations)
		{
			this._name = name;
			this._frameCount = frameCount;
			this._animatedVertexCount = animatedVertexCount;
			this._offsets = offsets;
			this._rotations = rotations;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003C73 File Offset: 0x00001E73
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003C7B File Offset: 0x00001E7B
		public int AnimatedVertexCount
		{
			get
			{
				return this._animatedVertexCount;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003C83 File Offset: 0x00001E83
		public int FrameCount
		{
			get
			{
				return this._frameCount;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003C8B File Offset: 0x00001E8B
		public Texture Offsets
		{
			get
			{
				return this._offsets;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003C93 File Offset: 0x00001E93
		public Texture Rotations
		{
			get
			{
				return this._rotations;
			}
		}

		// Token: 0x04000050 RID: 80
		[SerializeField]
		public string _name;

		// Token: 0x04000051 RID: 81
		[SerializeField]
		public int _frameCount;

		// Token: 0x04000052 RID: 82
		[SerializeField]
		public int _animatedVertexCount;

		// Token: 0x04000053 RID: 83
		[SerializeField]
		public Texture _offsets;

		// Token: 0x04000054 RID: 84
		[SerializeField]
		public Texture _rotations;
	}
}
