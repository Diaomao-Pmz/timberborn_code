using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.TransformControl
{
	// Token: 0x02000008 RID: 8
	public class TransformController : BaseComponent
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000021DC File Offset: 0x000003DC
		public PositionModifier AddPositionModifier()
		{
			PositionModifier positionModifier = new PositionModifier(this);
			this._positionModifiers.Add(positionModifier);
			return positionModifier;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002200 File Offset: 0x00000400
		public ScaleModifier AddScaleModifier()
		{
			ScaleModifier scaleModifier = new ScaleModifier(this);
			this._scaleModifiers.Add(scaleModifier);
			return scaleModifier;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002224 File Offset: 0x00000424
		public RotationModifier AddRotationModifier(int order)
		{
			RotationModifier rotationModifier = new RotationModifier(this);
			if (CollectionExtensions.TryAdd<int, RotationModifier>(this._rotationModifiers, order, rotationModifier))
			{
				return rotationModifier;
			}
			throw new ArgumentException(string.Format("A rotation modifier with order {0} already exists.", order));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000225E File Offset: 0x0000045E
		public void ApplyPosition()
		{
			base.Transform.localPosition = this.CalculatePosition();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002271 File Offset: 0x00000471
		public void ApplyRotation()
		{
			base.Transform.localRotation = this.CalculateRotation();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002284 File Offset: 0x00000484
		public void ApplyScale()
		{
			base.Transform.localScale = this.CalculateScale();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002298 File Offset: 0x00000498
		public Vector3 CalculatePosition()
		{
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < this._positionModifiers.Count; i++)
			{
				vector += this._positionModifiers[i].Value;
			}
			return vector;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022DC File Offset: 0x000004DC
		public Quaternion CalculateRotation()
		{
			Quaternion quaternion = Quaternion.identity;
			IList<RotationModifier> values = this._rotationModifiers.Values;
			for (int i = 0; i < values.Count; i++)
			{
				quaternion = values[i].Value * quaternion;
			}
			return quaternion;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002320 File Offset: 0x00000520
		public Vector3 CalculateScale()
		{
			Vector3 vector = Vector3.one;
			for (int i = 0; i < this._scaleModifiers.Count; i++)
			{
				vector = Vector3.Scale(vector, this._scaleModifiers[i].Value);
			}
			return vector;
		}

		// Token: 0x0400000C RID: 12
		public readonly List<PositionModifier> _positionModifiers = new List<PositionModifier>();

		// Token: 0x0400000D RID: 13
		public readonly SortedList<int, RotationModifier> _rotationModifiers = new SortedList<int, RotationModifier>();

		// Token: 0x0400000E RID: 14
		public readonly List<ScaleModifier> _scaleModifiers = new List<ScaleModifier>();
	}
}
