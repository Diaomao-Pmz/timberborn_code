using System;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using Timberborn.PrefabOptimization;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.TimbermeshAnimations;
using UnityEngine;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000016 RID: 22
	public class ShaftModelFactory : ILoadableSingleton
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00004280 File Offset: 0x00002480
		public ShaftModelFactory(OptimizedPrefabInstantiator optimizedPrefabInstantiator, ShaftFrameFactory shaftFrameFactory, TemplateService templateService)
		{
			this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
			this._shaftFrameFactory = shaftFrameFactory;
			this._templateService = templateService;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000429D File Offset: 0x0000249D
		public void Load()
		{
			this._modularShaftPartsSpec = this._templateService.GetSingle<ModularShaftPartsSpec>();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000042B0 File Offset: 0x000024B0
		public void BuildNonStackable(ShaftVariant variant, GameObject root)
		{
			this.Build(variant, root, false);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000042BB File Offset: 0x000024BB
		public void BuildStackable(ShaftVariant variant, GameObject root)
		{
			this.Build(variant, root, true);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000042C8 File Offset: 0x000024C8
		public void Build(ShaftVariant variant, GameObject root, bool isStackable)
		{
			ShaftAssembly assembly = default(ShaftAssembly);
			bool isReversed = false;
			variant = ShaftModelFactory.GetVariantWithSymmetricalHorizontalEnds(variant);
			Direction3D direction3D;
			if (ShaftModelFactory.TryGetMainDirection(variant, out direction3D))
			{
				Direction3D direction = direction3D.RotateHorizontally(Orientation.Cw90);
				Direction3D direction2 = direction3D.RotateHorizontally(Orientation.Cw270);
				byte rotation = variant.GetRotation(direction);
				byte rotation2 = variant.GetRotation(direction2);
				byte rotation3 = variant.GetRotation(direction3D);
				isReversed = (rotation3 == 2);
				if (rotation > 0)
				{
					assembly.ConnectLeft(rotation != rotation3);
				}
				if (rotation2 > 0)
				{
					assembly.ConnectRight(rotation2 != rotation3);
				}
				byte rotation4 = variant.GetRotation(Direction3D.Top);
				if (rotation4 > 0)
				{
					assembly.ConnectTop(rotation4 != rotation3);
				}
				byte rotation5 = variant.GetRotation(Direction3D.Bottom);
				if (rotation5 > 0)
				{
					assembly.ConnectBottom(rotation5 != rotation3);
				}
				byte rotation6 = variant.GetRotation(direction3D.Across());
				if (rotation6 > 0)
				{
					assembly.ConnectOpposite(rotation6 != rotation3);
				}
			}
			else
			{
				byte rotation7 = variant.GetRotation(Direction3D.Top);
				byte rotation8 = variant.GetRotation(Direction3D.Bottom);
				if (rotation7 > 0 && rotation8 > 0)
				{
					assembly.ConnectTopAndBottomOnly(rotation7 != rotation8);
					isReversed = ((rotation7 == rotation8) ? (rotation8 == 1) : (rotation7 == 2));
				}
				else if (rotation8 > 0)
				{
					assembly.ConnectBottomOnly();
					isReversed = (rotation8 == 1);
				}
				else if (rotation7 > 0)
				{
					assembly.ConnectTopOnly();
					isReversed = (rotation7 == 1);
				}
			}
			assembly.Optimize();
			this.InstantiateMovingParts(assembly, direction3D, root, isReversed);
			this.InstantiateFrame(assembly, variant, direction3D, root, isStackable);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004440 File Offset: 0x00002640
		public static bool TryGetMainDirection(ShaftVariant variant, out Direction3D mainDirection)
		{
			if (variant.Down > 0)
			{
				mainDirection = Direction3D.Down;
				return true;
			}
			if (variant.Left > 0)
			{
				mainDirection = Direction3D.Left;
				return true;
			}
			if (variant.Up > 0)
			{
				mainDirection = Direction3D.Up;
				return true;
			}
			if (variant.Right > 0)
			{
				mainDirection = Direction3D.Right;
				return true;
			}
			mainDirection = Direction3D.Down;
			return false;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004490 File Offset: 0x00002690
		public static ShaftVariant GetVariantWithSymmetricalHorizontalEnds(ShaftVariant variant)
		{
			if (variant.Top == 0 && variant.Bottom == 0)
			{
				if (variant.Left > 0 && variant.Right == 0 && variant.Up == 0 && variant.Down == 0)
				{
					return variant.AddSymmetryRight();
				}
				if (variant.Right > 0 && variant.Left == 0 && variant.Up == 0 && variant.Down == 0)
				{
					return variant.AddSymmetryLeft();
				}
				if (variant.Up > 0 && variant.Down == 0 && variant.Left == 0 && variant.Right == 0)
				{
					return variant.AddSymmetryDown();
				}
				if (variant.Down > 0 && variant.Up == 0 && variant.Left == 0 && variant.Right == 0)
				{
					return variant.AddSymmetryUp();
				}
			}
			return variant;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004578 File Offset: 0x00002778
		public void InstantiateMovingParts(ShaftAssembly assembly, Direction3D mainDirection, GameObject root, bool isReversed)
		{
			Direction3D direction = mainDirection.RotateHorizontally(Orientation.Cw90);
			Direction3D direction2 = mainDirection.RotateHorizontally(Orientation.Cw270);
			Direction3D direction3 = mainDirection.Across();
			if (assembly.ShowMainGearSmall && !assembly.ShowMainGearLarge)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearSmall, root, mainDirection, isReversed);
			}
			else if (assembly.ShowMainGearLarge)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearLarge, root, mainDirection, isReversed);
			}
			if (assembly.ShowGearInner)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearInner, root, mainDirection, isReversed);
			}
			else if (assembly.ShowGearInnerLong)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearInnerLong, root, mainDirection, isReversed);
			}
			else if (assembly.ShowAxleInnerLong)
			{
				this.Instantiate(this._modularShaftPartsSpec.AxleInnerLong, root, direction3, !isReversed);
			}
			else if (assembly.ShowGearInnerThrough)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearInnerThrough, root, mainDirection, isReversed);
			}
			if (assembly.ShowGearInnerOpposite)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearInnerOpposite, root, mainDirection, isReversed);
			}
			if (assembly.ShowBottomGearBase && !assembly.ShowGearBottomLarge)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearBottomBase, root, mainDirection, !isReversed);
			}
			if (assembly.ShowOppositeGearSmall)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearSmall, root, direction3, isReversed);
			}
			if (assembly.ShowLeftGearSmall)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearSmall, root, direction, isReversed);
			}
			if (assembly.ShowLeftGearMedium)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearMedium, root, direction, !isReversed);
			}
			if (assembly.ShowRightGearSmall)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearSmall, root, direction2, isReversed);
			}
			if (assembly.ShowRightGearMedium)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearMedium, root, direction2, !isReversed);
			}
			if (assembly.ShowGearBottomSmall)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearBottomSmall, root, Direction3D.Up, isReversed);
			}
			if (assembly.ShowGearBottomLarge)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearBottomLarge, root, Direction3D.Up, !isReversed);
			}
			if (assembly.ShowGearTopSmall)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearTopSmall, root, Direction3D.Up, !isReversed);
			}
			if (assembly.ShowGearTopLarge)
			{
				this.Instantiate(this._modularShaftPartsSpec.GearTopLarge, root, Direction3D.Up, !isReversed);
			}
			if (assembly.ShowAxleVertical)
			{
				this.Instantiate(this._modularShaftPartsSpec.AxleVertical, root, Direction3D.Up, !isReversed);
			}
			if (assembly.ShowAxleHorizontal)
			{
				this.Instantiate(this._modularShaftPartsSpec.AxleHorizontal, root, mainDirection, isReversed);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004808 File Offset: 0x00002A08
		public void InstantiateFrame(ShaftAssembly assembly, ShaftVariant variant, Direction3D mainDirection, GameObject root, bool isStackable)
		{
			bool showMainGearSmall = assembly.ShowMainGearSmall;
			bool down = variant.Down > 0 || (showMainGearSmall && mainDirection == Direction3D.Down);
			bool left = variant.Left > 0 || (showMainGearSmall && mainDirection == Direction3D.Left);
			bool up = variant.Up > 0 || (showMainGearSmall && mainDirection == Direction3D.Up);
			bool right = variant.Right > 0 || (showMainGearSmall && mainDirection == Direction3D.Right);
			bool bottom = assembly.ShowBottomGearBase || assembly.ShowGearBottomLarge || assembly.ShowGearBottomSmall || assembly.ShowAxleVertical;
			FrameVariant variant2 = new FrameVariant(down, left, up, right, bottom, isStackable);
			GameObject frame = this._shaftFrameFactory.GetFrame(variant2);
			frame.transform.SetParent(root.transform);
			frame.SetActive(true);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000048DC File Offset: 0x00002ADC
		public void Instantiate(AssetRef<GameObject> gameObject, GameObject root, Direction3D direction, bool reverseRotation)
		{
			GameObject gameObject2 = this._optimizedPrefabInstantiator.Instantiate(gameObject.Asset, root.transform);
			Quaternion quaternion = Quaternion.AngleAxis(direction.ToHorizontalAngle(), Vector3.up);
			gameObject2.transform.SetLocalPositionAndRotation(Vector3.zero, quaternion);
			gameObject2.GetComponent<IAnimator>().PlayBackwards = reverseRotation;
			gameObject2.SetActive(true);
		}

		// Token: 0x0400006C RID: 108
		public readonly OptimizedPrefabInstantiator _optimizedPrefabInstantiator;

		// Token: 0x0400006D RID: 109
		public readonly ShaftFrameFactory _shaftFrameFactory;

		// Token: 0x0400006E RID: 110
		public readonly TemplateService _templateService;

		// Token: 0x0400006F RID: 111
		public ModularShaftPartsSpec _modularShaftPartsSpec;
	}
}
