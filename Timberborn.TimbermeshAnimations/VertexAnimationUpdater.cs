using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x0200001D RID: 29
	public class VertexAnimationUpdater : MonoBehaviour, IAnimationUpdater
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00004114 File Offset: 0x00002314
		public void Initialize()
		{
			this._animationsMap = this._animations.ToDictionary((VertexAnimation a) => a.Name, (VertexAnimation a) => a);
			this.SetupMeshRenderer();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004176 File Offset: 0x00002376
		public void AssignAnimations(List<VertexAnimation> animations)
		{
			this._animations = animations;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004180 File Offset: 0x00002380
		public void SetAnimation(string animationName, bool looped)
		{
			this._currentAnimation = null;
			VertexAnimation currentAnimation;
			if (this._animationsMap.TryGetValue(animationName, out currentAnimation))
			{
				this._currentAnimation = currentAnimation;
				this.UpdateMaterialProperties(looped);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000041B2 File Offset: 0x000023B2
		public void UpdateAnimation(float normalizedTime)
		{
			if (this._currentAnimation != null)
			{
				this.Material.SetFloat(VertexAnimationUpdater.AnimationTimeId, normalizedTime);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000041CD File Offset: 0x000023CD
		public Material Material
		{
			get
			{
				return this._meshRenderer.material;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000041DA File Offset: 0x000023DA
		public void SetupMeshRenderer()
		{
			this._meshRenderer = base.GetComponent<MeshRenderer>();
			this.Material.SetFloat(VertexAnimationUpdater.AnimationEnabledId, 1f);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004200 File Offset: 0x00002400
		public void UpdateMaterialProperties(bool looped)
		{
			this.Material.SetFloat(VertexAnimationUpdater.AnimatedVertexCountId, (float)this._currentAnimation.AnimatedVertexCount);
			this.Material.SetFloat(VertexAnimationUpdater.FrameCountId, (float)this._currentAnimation.FrameCount);
			this.Material.SetFloat(VertexAnimationUpdater.Looped, looped ? 1f : 0f);
			this.Material.SetTexture(VertexAnimationUpdater.OffsetsId, this._currentAnimation.Offsets);
			this.Material.SetTexture(VertexAnimationUpdater.RotationsId, this._currentAnimation.Rotations);
		}

		// Token: 0x0400005C RID: 92
		[HideInInspector]
		public static readonly int OffsetsId = Shader.PropertyToID("_Offsets");

		// Token: 0x0400005D RID: 93
		[HideInInspector]
		public static readonly int RotationsId = Shader.PropertyToID("_Rotations");

		// Token: 0x0400005E RID: 94
		[HideInInspector]
		public static readonly int AnimatedVertexCountId = Shader.PropertyToID("_AnimatedVertexCount");

		// Token: 0x0400005F RID: 95
		[HideInInspector]
		public static readonly int FrameCountId = Shader.PropertyToID("_FrameCount");

		// Token: 0x04000060 RID: 96
		[HideInInspector]
		public static readonly int Looped = Shader.PropertyToID("_Looped");

		// Token: 0x04000061 RID: 97
		[HideInInspector]
		public static readonly int AnimationTimeId = Shader.PropertyToID("_AnimationTime");

		// Token: 0x04000062 RID: 98
		[HideInInspector]
		public static readonly int AnimationEnabledId = Shader.PropertyToID("_AnimationEnabled");

		// Token: 0x04000063 RID: 99
		[HideInInspector]
		[SerializeField]
		public List<VertexAnimation> _animations;

		// Token: 0x04000064 RID: 100
		[HideInInspector]
		public Dictionary<string, VertexAnimation> _animationsMap;

		// Token: 0x04000065 RID: 101
		[HideInInspector]
		public VertexAnimation _currentAnimation;

		// Token: 0x04000066 RID: 102
		[HideInInspector]
		public MeshRenderer _meshRenderer;
	}
}
