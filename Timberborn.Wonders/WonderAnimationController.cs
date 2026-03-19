using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.TimbermeshAnimations;
using Timberborn.WorldPersistence;

namespace Timberborn.Wonders
{
	// Token: 0x02000010 RID: 16
	public class WonderAnimationController : BaseComponent, IAwakableComponent, IUpdatableComponent, IPersistentEntity, IInitializableEntity
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000030 RID: 48 RVA: 0x00002530 File Offset: 0x00000730
		// (remove) Token: 0x06000031 RID: 49 RVA: 0x00002568 File Offset: 0x00000768
		public event EventHandler StartAnimationFinished;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000259D File Offset: 0x0000079D
		public bool IsAnimating
		{
			get
			{
				return this._animator.Enabled;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025AC File Offset: 0x000007AC
		public void Awake()
		{
			this._animator = base.GetComponentInChildren<IAnimator>(true);
			this._wonder = base.GetComponent<Wonder>();
			this._wonder.WonderActivated += this.OnWonderActivated;
			this._wonder.WonderDeactivated += this.OnWonderDeactivated;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002600 File Offset: 0x00000800
		public void Update()
		{
			if (this.IsAnimating && this._animator.PlayingFinished)
			{
				this.InvokeAnimationFinishedEvent();
				this._animator.Enabled = false;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000262C File Offset: 0x0000082C
		public void InitializeEntity()
		{
			this.StartAnimation(!this._wonder.IsActive);
			if (this._loadedAnimatorTime != null)
			{
				this._animator.SetTime(this._loadedAnimatorTime.Value);
				return;
			}
			this._animator.SetTime(this._animator.AnimationLength);
			this._animator.Enabled = false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002694 File Offset: 0x00000894
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(WonderAnimationController.WonderAnimationControllerKey);
			component.Set(WonderAnimationController.IsAnimatingKey, this.IsAnimating);
			if (this.IsAnimating)
			{
				component.Set(WonderAnimationController.AnimationTimeKey, this._animator.Time);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026DC File Offset: 0x000008DC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WonderAnimationController.WonderAnimationControllerKey);
			this._animator.Enabled = component.Get(WonderAnimationController.IsAnimatingKey);
			if (component.Has<float>(WonderAnimationController.AnimationTimeKey))
			{
				this._loadedAnimatorTime = new float?(component.Get(WonderAnimationController.AnimationTimeKey));
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000272E File Offset: 0x0000092E
		public void InvokeAnimationFinishedEvent()
		{
			if (this._wonder.IsActive)
			{
				EventHandler startAnimationFinished = this.StartAnimationFinished;
				if (startAnimationFinished == null)
				{
					return;
				}
				startAnimationFinished(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002753 File Offset: 0x00000953
		public void OnWonderActivated(object sender, EventArgs e)
		{
			this.StartAnimation(false);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000275C File Offset: 0x0000095C
		public void OnWonderDeactivated(object sender, EventArgs e)
		{
			this.StartAnimation(true);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002765 File Offset: 0x00000965
		public void StartAnimation(bool backwards)
		{
			this._animator.Enabled = true;
			this._animator.PlayBackwards = backwards;
			this._animator.Play(WonderAnimationController.AnimationName, false);
		}

		// Token: 0x0400001B RID: 27
		public static readonly string AnimationName = "Default";

		// Token: 0x0400001C RID: 28
		public static readonly ComponentKey WonderAnimationControllerKey = new ComponentKey("WonderAnimationController");

		// Token: 0x0400001D RID: 29
		public static readonly PropertyKey<bool> IsAnimatingKey = new PropertyKey<bool>("IsAnimating");

		// Token: 0x0400001E RID: 30
		public static readonly PropertyKey<float> AnimationTimeKey = new PropertyKey<float>("AnimationTime");

		// Token: 0x04000020 RID: 32
		public IAnimator _animator;

		// Token: 0x04000021 RID: 33
		public Wonder _wonder;

		// Token: 0x04000022 RID: 34
		public float? _loadedAnimatorTime;
	}
}
