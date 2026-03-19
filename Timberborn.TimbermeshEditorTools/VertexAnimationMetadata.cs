using System;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.TimbermeshEditorTools
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class VertexAnimationMetadata
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000230C File Offset: 0x0000050C
		public VertexAnimationMetadata(string nodeName, VertexAnimation animation)
		{
			this._nodeName = nodeName;
			this._animationName = animation.Name;
			this._framerate = animation.Framerate;
			this._frameCount = animation.Frames.Count;
			this._animatedVertexCount = animation.AnimatedVertexCount;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000235B File Offset: 0x0000055B
		public string NodeName
		{
			get
			{
				return this._nodeName;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002363 File Offset: 0x00000563
		public string AnimationName
		{
			get
			{
				return this._animationName;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000236B File Offset: 0x0000056B
		public float Framerate
		{
			get
			{
				return this._framerate;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002373 File Offset: 0x00000573
		public int FrameCount
		{
			get
			{
				return this._frameCount;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000237B File Offset: 0x0000057B
		public int AnimatedVertexCount
		{
			get
			{
				return this._animatedVertexCount;
			}
		}

		// Token: 0x04000012 RID: 18
		[SerializeField]
		public string _nodeName;

		// Token: 0x04000013 RID: 19
		[SerializeField]
		public string _animationName;

		// Token: 0x04000014 RID: 20
		[SerializeField]
		public float _framerate;

		// Token: 0x04000015 RID: 21
		[SerializeField]
		public int _frameCount;

		// Token: 0x04000016 RID: 22
		[SerializeField]
		public int _animatedVertexCount;
	}
}
