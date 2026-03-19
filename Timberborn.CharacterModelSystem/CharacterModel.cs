using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.MortalComponents;
using Timberborn.Persistence;
using Timberborn.SelectionSystem;
using Timberborn.StatusSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x02000009 RID: 9
	public class CharacterModel : BaseComponent, IAwakableComponent, ILateUpdatableComponent, IPersistentEntity, ICameraTarget, IDeadNeededComponent, IChildhoodInfluenced
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022BF File Offset: 0x000004BF
		public Transform Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022C7 File Offset: 0x000004C7
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000022D4 File Offset: 0x000004D4
		public Vector3 Position
		{
			get
			{
				return this._model.position;
			}
			set
			{
				this._model.position = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022E2 File Offset: 0x000004E2
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000022EF File Offset: 0x000004EF
		public Quaternion Rotation
		{
			get
			{
				return this._model.rotation;
			}
			set
			{
				this._model.rotation = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022FD File Offset: 0x000004FD
		public Vector3 CameraTargetPosition
		{
			get
			{
				return this.Position;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002308 File Offset: 0x00000508
		public void Awake()
		{
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			StatusIconCycler componentInChildren = base.GetComponentInChildren<StatusIconCycler>(true);
			this._statusVisibilityToggle = componentInChildren.GetStatusVisibilityToggle();
			string modelName = base.GetComponent<CharacterModelSpec>().ModelName;
			this._model = base.GameObject.FindChildTransform(modelName);
			this._modelGameObject = this._model.gameObject;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002364 File Offset: 0x00000564
		public void LateUpdate()
		{
			this.FollowTarget();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000236C File Offset: 0x0000056C
		public void InfluenceByChildhood(Character child)
		{
			CharacterModel component = child.GetComponent<CharacterModel>();
			this.Rotation = component.Rotation;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000238C File Offset: 0x0000058C
		public void Show()
		{
			if (this._isHidden)
			{
				this._isHidden = false;
				this.UpdateVisibility();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023A3 File Offset: 0x000005A3
		public void Hide()
		{
			if (!this._isHidden)
			{
				this._isHidden = true;
				this.UpdateVisibility();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023BA File Offset: 0x000005BA
		public void BlockModel()
		{
			if (!this._blockModel)
			{
				this._blockModel = true;
				this.UpdateVisibility();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023D1 File Offset: 0x000005D1
		public void UnblockModel()
		{
			if (this._blockModel)
			{
				this._blockModel = false;
				this.UpdateVisibility();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023E8 File Offset: 0x000005E8
		public void IgnoreModelBlockade()
		{
			if (!this._ignoreModelBlockade)
			{
				this._ignoreModelBlockade = true;
				this.UpdateVisibility();
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023FF File Offset: 0x000005FF
		public void UnignoreModelBlockade()
		{
			if (this._ignoreModelBlockade)
			{
				this._ignoreModelBlockade = false;
				this.UpdateVisibility();
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002416 File Offset: 0x00000616
		public void ResetModelPosition()
		{
			this._model.localPosition = Vector3.zero;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002428 File Offset: 0x00000628
		public void AnimateFollowingTarget(Transform target, string animationName)
		{
			this.Animate(animationName);
			this._target = target;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002438 File Offset: 0x00000638
		public void PositionAtTarget(Transform target)
		{
			this.Position = target.position;
			this.Rotation = target.rotation;
			this.Show();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002458 File Offset: 0x00000658
		public void StopAnimating()
		{
			this.UnsetActiveAnimation();
			this._model.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
			this._target = null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000247C File Offset: 0x0000067C
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(CharacterModel.CharacterModelKey).Set(CharacterModel.RotationKey, this.Rotation);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000249C File Offset: 0x0000069C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(CharacterModel.CharacterModelKey);
			this.Rotation = component.Get(CharacterModel.RotationKey);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024C6 File Offset: 0x000006C6
		public void LookToward(Vector3 target)
		{
			target.y = this.Position.y;
			this._model.transform.LookAt(target);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024EB File Offset: 0x000006EB
		public bool ModelIsBlocked
		{
			get
			{
				return this._blockModel && !this._ignoreModelBlockade;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002500 File Offset: 0x00000700
		public void UpdateVisibility()
		{
			bool flag = !this.ModelIsBlocked && !this._isHidden;
			this._modelGameObject.SetActive(flag);
			if (flag)
			{
				this._statusVisibilityToggle.Show();
				return;
			}
			this._statusVisibilityToggle.Hide();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002548 File Offset: 0x00000748
		public void FollowTarget()
		{
			if (this._target)
			{
				this.Position = this._target.position;
				this.Rotation = this._target.rotation;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002579 File Offset: 0x00000779
		public void Animate(string animationName)
		{
			this.StopAnimating();
			this.Show();
			this.SetActiveAnimation(animationName);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000258E File Offset: 0x0000078E
		public void SetActiveAnimation(string animationName)
		{
			this._characterAnimator.SetBool(animationName, true);
			this._characterAnimator.SetFloat("WalkingSpeed", 1f);
			this._activeAnimationName = animationName;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025B9 File Offset: 0x000007B9
		public void UnsetActiveAnimation()
		{
			if (this._activeAnimationName != null)
			{
				this._characterAnimator.SetBool(this._activeAnimationName, false);
				this._activeAnimationName = null;
			}
		}

		// Token: 0x04000012 RID: 18
		public static readonly ComponentKey CharacterModelKey = new ComponentKey("CharacterModel");

		// Token: 0x04000013 RID: 19
		public static readonly PropertyKey<Quaternion> RotationKey = new PropertyKey<Quaternion>("Rotation");

		// Token: 0x04000014 RID: 20
		public CharacterAnimator _characterAnimator;

		// Token: 0x04000015 RID: 21
		public StatusIconCycler _statusIconCycler;

		// Token: 0x04000016 RID: 22
		public string _activeAnimationName;

		// Token: 0x04000017 RID: 23
		public Transform _target;

		// Token: 0x04000018 RID: 24
		public Transform _model;

		// Token: 0x04000019 RID: 25
		public GameObject _modelGameObject;

		// Token: 0x0400001A RID: 26
		public StatusVisibilityToggle _statusVisibilityToggle;

		// Token: 0x0400001B RID: 27
		public bool _blockModel;

		// Token: 0x0400001C RID: 28
		public bool _ignoreModelBlockade;

		// Token: 0x0400001D RID: 29
		public bool _isHidden;
	}
}
