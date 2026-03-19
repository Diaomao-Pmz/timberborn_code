using System;
using System.Collections.Generic;
using Timberborn.Timbermesh;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x02000010 RID: 16
	public class NodeAnimationCache
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000029FC File Offset: 0x00000BFC
		public int CacheAnimations(Node sourceNode)
		{
			int num = this._lastAnimationsId + 1;
			this._lastAnimationsId = num;
			int num2 = num;
			List<NodeAnimation> list = new List<NodeAnimation>();
			foreach (NodeAnimation nodeAnimation in sourceNode.NodeAnimations)
			{
				if (nodeAnimation.Frames.Count == 0)
				{
					throw new InvalidOperationException(string.Concat(new string[]
					{
						"Animation ",
						nodeAnimation.Name,
						" in node ",
						sourceNode.Name,
						" has no frames."
					}));
				}
				NodeAnimation item = NodeAnimationCache.CreateNodeAnimation(nodeAnimation);
				list.Add(item);
			}
			this._animations.Add(num2, list);
			return num2;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002ACC File Offset: 0x00000CCC
		public IEnumerable<NodeAnimation> GetAnimations(int animationsId)
		{
			return this._animations[animationsId];
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002ADC File Offset: 0x00000CDC
		public static NodeAnimation CreateNodeAnimation(NodeAnimation animation)
		{
			int count = animation.Frames.Count;
			Vector3[] array = new Vector3[count];
			Quaternion[] array2 = new Quaternion[count];
			Vector3[] array3 = new Vector3[count];
			for (int i = 0; i < count; i++)
			{
				NodeAnimationFrame nodeAnimationFrame = animation.Frames[i];
				array[i] = nodeAnimationFrame.Position.ToVector3();
				array2[i] = nodeAnimationFrame.Rotation.ToQuaternion();
				array3[i] = nodeAnimationFrame.Scale.ToVector3();
			}
			return NodeAnimation.Create(animation.Name, count, array, array2, array3);
		}

		// Token: 0x04000021 RID: 33
		public readonly Dictionary<int, List<NodeAnimation>> _animations = new Dictionary<int, List<NodeAnimation>>();

		// Token: 0x04000022 RID: 34
		public int _lastAnimationsId;
	}
}
