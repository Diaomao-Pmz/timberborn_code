using System;
using System.Collections.Generic;
using System.Linq;
using Bindito.Unity;
using Timberborn.Timbermesh;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x02000007 RID: 7
	public class AnimationInitializer : IModelPostprocessor
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AnimationInitializer(IInstantiator instantiator, VertexAnimationInitializer vertexAnimationInitializer, NodeAnimationInitializer nodeAnimationInitializer)
		{
			this._instantiator = instantiator;
			this._vertexAnimationInitializer = vertexAnimationInitializer;
			this._nodeAnimationInitializer = nodeAnimationInitializer;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
		public void Postprocess(ImportDetails details)
		{
			this.InitializeAllAnimations(details);
			if (this._animationMap.Any<KeyValuePair<string, AnimationMetadata>>())
			{
				this.AddAnimator(details.Root.gameObject);
				this._animationMap.Clear();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000215C File Offset: 0x0000035C
		public void InitializeAllAnimations(ImportDetails importDetails)
		{
			foreach (KeyValuePair<Node, GameObject> keyValuePair in importDetails.CreatedObjectsMap)
			{
				Node node;
				GameObject gameObject;
				keyValuePair.Deconstruct(ref node, ref gameObject);
				Node node2 = node;
				GameObject animatedObject = gameObject;
				this.AddAnimations(node2.VertexAnimations);
				this.AddAnimations(node2.NodeAnimations);
				this._vertexAnimationInitializer.InitializeAnimations(animatedObject, node2);
				this._nodeAnimationInitializer.InitializeAnimations(animatedObject, node2);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E8 File Offset: 0x000003E8
		public void AddAnimations(IReadOnlyList<IAnimation> animations)
		{
			for (int i = 0; i < animations.Count; i++)
			{
				IAnimation animation = animations[i];
				if (!this._animationMap.ContainsKey(animation.Name))
				{
					this._animationMap.Add(animation.Name, new AnimationMetadata(animation.Name, animation.Length));
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002243 File Offset: 0x00000443
		public void AddAnimator(GameObject animatedObject)
		{
			this._instantiator.AddComponent<TimbermeshAnimator>(animatedObject).SetAnimations(this._animationMap.Values);
		}

		// Token: 0x04000008 RID: 8
		public readonly IInstantiator _instantiator;

		// Token: 0x04000009 RID: 9
		public readonly VertexAnimationInitializer _vertexAnimationInitializer;

		// Token: 0x0400000A RID: 10
		public readonly NodeAnimationInitializer _nodeAnimationInitializer;

		// Token: 0x0400000B RID: 11
		public readonly Dictionary<string, AnimationMetadata> _animationMap = new Dictionary<string, AnimationMetadata>();
	}
}
