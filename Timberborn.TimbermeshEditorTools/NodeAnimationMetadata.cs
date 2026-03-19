using System;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.TimbermeshEditorTools
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class NodeAnimationMetadata
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000227D File Offset: 0x0000047D
		public NodeAnimationMetadata(string nodeName, NodeAnimation animation)
		{
			this._nodeName = nodeName;
			this._animationName = animation.Name;
			this._framerate = animation.Framerate;
			this._frameCount = animation.Frames.Count;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022B5 File Offset: 0x000004B5
		public string NodeName
		{
			get
			{
				return this._nodeName;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022BD File Offset: 0x000004BD
		public string AnimationName
		{
			get
			{
				return this._animationName;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022C5 File Offset: 0x000004C5
		public float Framerate
		{
			get
			{
				return this._framerate;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022CD File Offset: 0x000004CD
		public int FrameCount
		{
			get
			{
				return this._frameCount;
			}
		}

		// Token: 0x0400000B RID: 11
		[SerializeField]
		public string _nodeName;

		// Token: 0x0400000C RID: 12
		[SerializeField]
		public string _animationName;

		// Token: 0x0400000D RID: 13
		[SerializeField]
		public float _framerate;

		// Token: 0x0400000E RID: 14
		[SerializeField]
		public int _frameCount;
	}
}
