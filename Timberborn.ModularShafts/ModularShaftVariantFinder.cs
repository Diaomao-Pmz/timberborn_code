using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.MechanicalSystem;
using UnityEngine;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000013 RID: 19
	public class ModularShaftVariantFinder : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00003757 File Offset: 0x00001957
		public ModularShaftVariantFinder(IBlockService blockService, PreviewBlockService previewBlockService)
		{
			this._blockService = blockService;
			this._previewBlockService = previewBlockService;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003778 File Offset: 0x00001978
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._mechanicalNode.TransputsInitialized += this.OnTransputsInitialized;
			this._supportedDirections = this.GetSupportedDirections();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000037B8 File Offset: 0x000019B8
		public ShaftVariant FindBestVariant()
		{
			TransputRotation rotation = this.GetRotation(Direction3D.Down);
			TransputRotation rotation2 = this.GetRotation(Direction3D.Left);
			TransputRotation rotation3 = this.GetRotation(Direction3D.Up);
			TransputRotation rotation4 = this.GetRotation(Direction3D.Right);
			TransputRotation rotation5 = this.GetRotation(Direction3D.Bottom);
			TransputRotation rotation6 = this.GetRotation(Direction3D.Top);
			return this.OptimizeVariant(rotation, rotation2, rotation3, rotation4, rotation5, rotation6);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003808 File Offset: 0x00001A08
		public ImmutableHashSet<Direction3D> GetSupportedDirections()
		{
			HashSet<Direction3D> hashSet = new HashSet<Direction3D>();
			foreach (TransputSpec transputSpec in base.GetComponent<TransputProviderSpec>().Transputs)
			{
				foreach (Direction3D item in transputSpec.Directions.GetEnumerator())
				{
					hashSet.Add(item);
				}
			}
			return hashSet.ToImmutableHashSet<Direction3D>();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003878 File Offset: 0x00001A78
		public void OnTransputsInitialized(object sender, EventArgs e)
		{
			foreach (Transput transput in this._mechanicalNode.Transputs)
			{
				if (!this._supportedDirections.Contains(transput.BaseDirection))
				{
					throw new NotSupportedException(string.Format("Unexpected value: {0}.", transput.BaseDirection));
				}
				this._directedTransputs[transput.BaseDirection] = transput;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000038EC File Offset: 0x00001AEC
		public TransputRotation GetRotation(Direction3D direction)
		{
			if (!this._supportedDirections.Contains(direction))
			{
				return TransputRotation.None;
			}
			TransputRotation rotationFromTransputs = this.GetRotationFromTransputs(direction);
			if (rotationFromTransputs != TransputRotation.None)
			{
				return rotationFromTransputs;
			}
			if (!this.CanBeConnectedToMechanicalNode(direction))
			{
				return TransputRotation.None;
			}
			return TransputRotation.Ignored;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003924 File Offset: 0x00001B24
		public TransputRotation GetRotationFromTransputs(Direction3D direction)
		{
			Transput transput;
			if (!this._directedTransputs.TryGetValue(direction, out transput) || !transput.Connected)
			{
				return TransputRotation.None;
			}
			MechanicalNode parentNode = transput.ConnectedTransput.ParentNode;
			if (parentNode.IgnoreRotation || (!parentNode.IsGenerator && !parentNode.IsShaft))
			{
				return TransputRotation.Ignored;
			}
			if (!transput.ReversedRotation)
			{
				return TransputRotation.Normal;
			}
			return TransputRotation.Reversed;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000397C File Offset: 0x00001B7C
		public bool CanBeConnectedToMechanicalNode(Direction3D direction)
		{
			Direction3D direction3D = this._blockObject.TransformDirection(direction);
			Vector3Int coordinates = this._blockObject.Coordinates + direction3D.ToOffset();
			TransputProvider objectAt = this.GetObjectAt(coordinates);
			return objectAt != null && this.CanBeConnectedToAnyTransput(direction3D, objectAt.GetComponent<BlockObject>(), objectAt.TransputSpecs);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000039D0 File Offset: 0x00001BD0
		public TransputProvider GetObjectAt(Vector3Int coordinates)
		{
			TransputProvider firstObjectWithComponentAt = this._blockService.GetFirstObjectWithComponentAt<TransputProvider>(coordinates);
			if (firstObjectWithComponentAt != null)
			{
				return firstObjectWithComponentAt;
			}
			TransputProvider firstObjectWithComponentAt2 = this._previewBlockService.GetFirstObjectWithComponentAt<TransputProvider>(coordinates);
			if (firstObjectWithComponentAt2 != null)
			{
				return firstObjectWithComponentAt2;
			}
			return null;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003A04 File Offset: 0x00001C04
		public bool CanBeConnectedToAnyTransput(Direction3D direction, BlockObject transputOwner, ImmutableArray<TransputSpec> transputSpecs)
		{
			foreach (TransputSpec transputSpec in transputSpecs)
			{
				Vector3Int vector3Int = transputOwner.TransformCoordinates(transputSpec.Coordinates);
				foreach (Direction3D direction3D in transputSpec.Directions.GetEnumerator())
				{
					Direction3D direction3D2 = transputOwner.TransformDirection(direction3D);
					if (vector3Int + direction3D2.ToOffset() == this._blockObject.Coordinates && direction3D2 == direction.Across())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003A98 File Offset: 0x00001C98
		public ShaftVariant OptimizeVariant(TransputRotation down, TransputRotation left, TransputRotation up, TransputRotation right, TransputRotation bottom, TransputRotation top)
		{
			if (down == TransputRotation.None && left == TransputRotation.None && up == TransputRotation.None && right == TransputRotation.None && bottom == TransputRotation.None && top == TransputRotation.None)
			{
				return new ShaftVariant(1, 1, 1, 1, 2, this._supportedDirections.Contains(Direction3D.Top) ? 2 : 0);
			}
			ModularShaftVariantFinder.OptimizeForIgnoredRotation(ref down, ref left, ref up, ref right, ref bottom, ref top);
			return this.OptimizeForInactiveStraightShaft(new ShaftVariant(down.AsByte(), left.AsByte(), up.AsByte(), right.AsByte(), bottom.AsByte(), top.AsByte()));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003B1C File Offset: 0x00001D1C
		public static void OptimizeForIgnoredRotation(ref TransputRotation down, ref TransputRotation left, ref TransputRotation up, ref TransputRotation right, ref TransputRotation bottom, ref TransputRotation top)
		{
			if (down == TransputRotation.Ignored)
			{
				down = up.ReverseOrSetNormal();
			}
			if (up == TransputRotation.Ignored)
			{
				up = down.ReverseOrSetNormal();
			}
			if (right == TransputRotation.Ignored)
			{
				right = left.ReverseOrSetNormal();
			}
			if (left == TransputRotation.Ignored)
			{
				left = right.ReverseOrSetNormal();
			}
			if (bottom == TransputRotation.Ignored)
			{
				bottom = top.ReverseOrSetNormal();
			}
			if (top == TransputRotation.Ignored)
			{
				top = bottom.ReverseOrSetNormal();
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003B84 File Offset: 0x00001D84
		public ShaftVariant OptimizeForInactiveStraightShaft(ShaftVariant variant)
		{
			if (this.IsInactive())
			{
				if (variant.Left > 0 && variant.Right > 0 && variant.Up == 0 && variant.Down == 0 && variant.Bottom == 0 && variant.Top == 0)
				{
					return ModularShaftVariantFinder.LeftRightStraightVariant;
				}
				if (variant.Up > 0 && variant.Down > 0 && variant.Left == 0 && variant.Right == 0 && variant.Bottom == 0 && variant.Top == 0)
				{
					return ModularShaftVariantFinder.UpDownStraightVariant;
				}
				if (variant.Bottom > 0 && variant.Top > 0 && variant.Left == 0 && variant.Right == 0 && variant.Up == 0 && variant.Down == 0)
				{
					return ModularShaftVariantFinder.BottomTopStraightVariant;
				}
			}
			return variant;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003C63 File Offset: 0x00001E63
		public bool IsInactive()
		{
			return this._mechanicalNode.Graph == null || this._mechanicalNode.Graph.NumberOfGenerators == 0;
		}

		// Token: 0x04000045 RID: 69
		public static readonly ShaftVariant BottomTopStraightVariant = new ShaftVariant(0, 0, 0, 0, 1, 2);

		// Token: 0x04000046 RID: 70
		public static readonly ShaftVariant UpDownStraightVariant = new ShaftVariant(1, 0, 2, 0, 0, 0);

		// Token: 0x04000047 RID: 71
		public static readonly ShaftVariant LeftRightStraightVariant = new ShaftVariant(0, 1, 0, 2, 0, 0);

		// Token: 0x04000048 RID: 72
		public readonly IBlockService _blockService;

		// Token: 0x04000049 RID: 73
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x0400004A RID: 74
		public BlockObject _blockObject;

		// Token: 0x0400004B RID: 75
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400004C RID: 76
		public ImmutableHashSet<Direction3D> _supportedDirections;

		// Token: 0x0400004D RID: 77
		public readonly Dictionary<Direction3D, Transput> _directedTransputs = new Dictionary<Direction3D, Transput>();
	}
}
