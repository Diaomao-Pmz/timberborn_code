using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.TimbermeshAnimations;
using Timberborn.WalkingSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.CharacterControlSystem
{
	// Token: 0x02000006 RID: 6
	public class ControllableCharacter : BaseComponent, IAwakableComponent, IPostInitializableEntity, IPersistentEntity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021AF File Offset: 0x000003AF
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021B7 File Offset: 0x000003B7
		public bool UnderControl { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021C0 File Offset: 0x000003C0
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021C8 File Offset: 0x000003C8
		public Vector3 Destination { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021D1 File Offset: 0x000003D1
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021D9 File Offset: 0x000003D9
		public string WaitAnimation { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021E2 File Offset: 0x000003E2
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000021EA File Offset: 0x000003EA
		public bool ForcedWalking { get; private set; }

		// Token: 0x06000012 RID: 18 RVA: 0x000021F3 File Offset: 0x000003F3
		public void Awake()
		{
			this._animatorController = base.GetComponent<IAnimatorController>();
			this._animator = base.GetComponentInChildren<IAnimator>(false);
			this._walkingEnforcerToggle = base.GetComponent<WalkingEnforcer>().GetWalkingEnforcerToggle();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000221F File Offset: 0x0000041F
		public void PostInitializeEntity()
		{
			if (this.ForcedWalking)
			{
				this.EnableForcedWalking();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000222F File Offset: 0x0000042F
		public void TakeControlAndMoveTo(Vector3 destination)
		{
			this.Destination = destination;
			this.UnderControl = true;
			this.ToggleAnimationControl(false);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002246 File Offset: 0x00000446
		public void ChangeAnimation(string waitAnimation)
		{
			this.WaitAnimation = waitAnimation;
			if (this.UnderControl)
			{
				this.ToggleAnimationControl(false);
				this.PlayAnimation();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002264 File Offset: 0x00000464
		public void ReleaseControl()
		{
			this.UnderControl = false;
			this.ToggleAnimationControl(false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002274 File Offset: 0x00000474
		public IEnumerable<string> GetAnimationNames()
		{
			return this._animatorController.AnimationNames;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002281 File Offset: 0x00000481
		public void PlayAnimation()
		{
			this.ToggleAnimationControl(true);
			if (this._animator.AnimationName != this.WaitAnimation)
			{
				this._animator.Play(this.WaitAnimation, true);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022B4 File Offset: 0x000004B4
		public void EnableForcedWalking()
		{
			this._walkingEnforcerToggle.EnableForcedWalking();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022C1 File Offset: 0x000004C1
		public void DisableForcedWalking()
		{
			this._walkingEnforcerToggle.DisableForcedWalking();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022D0 File Offset: 0x000004D0
		public void Save(IEntitySaver entitySaver)
		{
			if (this.UnderControl)
			{
				IObjectSaver component = entitySaver.GetComponent(ControllableCharacter.ControllableCharacterKey);
				component.Set(ControllableCharacter.DestinationKey, this.Destination);
				component.Set(ControllableCharacter.WaitAnimationKey, this.WaitAnimation);
				component.Set(ControllableCharacter.ForcedWalkingKey, this._walkingEnforcerToggle.ForcedWalking);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002328 File Offset: 0x00000528
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(ControllableCharacter.ControllableCharacterKey, out objectLoader))
			{
				this.Destination = objectLoader.Get(ControllableCharacter.DestinationKey);
				this.WaitAnimation = objectLoader.Get(ControllableCharacter.WaitAnimationKey);
				this.ForcedWalking = objectLoader.Get(ControllableCharacter.ForcedWalkingKey);
				this.UnderControl = true;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000237E File Offset: 0x0000057E
		public void ToggleAnimationControl(bool controlEnabled)
		{
			if (controlEnabled)
			{
				this._animatorController.Disable();
				return;
			}
			this._animatorController.Enable();
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey ControllableCharacterKey = new ComponentKey("ControllableCharacter");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<Vector3> DestinationKey = new PropertyKey<Vector3>("Destination");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<string> WaitAnimationKey = new PropertyKey<string>("WaitAnimation");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<bool> ForcedWalkingKey = new PropertyKey<bool>("ForcedWalking");

		// Token: 0x04000010 RID: 16
		public IAnimatorController _animatorController;

		// Token: 0x04000011 RID: 17
		public IAnimator _animator;

		// Token: 0x04000012 RID: 18
		public WalkingEnforcerToggle _walkingEnforcerToggle;
	}
}
