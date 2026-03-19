using System;
using System.Collections.Generic;
using System.Linq;
using Bindito.Unity;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x0200001B RID: 27
	public class VertexAnimationInitializer
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00003C9B File Offset: 0x00001E9B
		public VertexAnimationInitializer(IInstantiator instantiator, VertexAnimationTextureGenerator vertexAnimationTextureGenerator)
		{
			this._instantiator = instantiator;
			this._vertexAnimationTextureGenerator = vertexAnimationTextureGenerator;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public void InitializeAnimations(GameObject animatedObject, Node source)
		{
			if (source.VertexCount > 0)
			{
				List<VertexAnimation> list = this.CreateAnimations(source);
				if (list.Any<VertexAnimation>())
				{
					this._instantiator.AddComponent<VertexAnimationUpdater>(animatedObject).AssignAnimations(list);
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003CEC File Offset: 0x00001EEC
		public List<VertexAnimation> CreateAnimations(Node source)
		{
			List<VertexAnimation> list = new List<VertexAnimation>();
			foreach (VertexAnimation vertexAnimation in source.VertexAnimations)
			{
				ValueTuple<Texture, Texture> valueTuple = this._vertexAnimationTextureGenerator.CreateAnimationTextures(vertexAnimation);
				Texture item = valueTuple.Item1;
				Texture item2 = valueTuple.Item2;
				VertexAnimation item3 = new VertexAnimation(vertexAnimation.Name, vertexAnimation.Frames.Count, vertexAnimation.AnimatedVertexCount, item, item2);
				list.Add(item3);
			}
			return list;
		}

		// Token: 0x04000055 RID: 85
		public readonly IInstantiator _instantiator;

		// Token: 0x04000056 RID: 86
		public readonly VertexAnimationTextureGenerator _vertexAnimationTextureGenerator;
	}
}
