using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Explosions;
using Timberborn.Illumination;
using Timberborn.Persistence;
using Timberborn.RecoverableGoodSystem;
using Timberborn.UnderstructureSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200000F RID: 15
	public class Detonator : BaseComponent, IAwakableComponent, IPersistentEntity, IAutomatableNeeder, ITerminal
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003095 File Offset: 0x00001295
		public bool NeedsAutomatable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003098 File Offset: 0x00001298
		public void Awake()
		{
			this._automatable = base.GetComponent<Automatable>();
			this._understructureConstraint = base.GetComponent<UnderstructureConstraint>();
			this._recoverableGoodProvider = base.GetComponent<RecoverableGoodProvider>();
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030CF File Offset: 0x000012CF
		public void Save(IEntitySaver entitySaver)
		{
			if (this._isArmed)
			{
				entitySaver.GetComponent(Detonator.ComponentKey).Set(Detonator.IsArmedKey, this._isArmed);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030F4 File Offset: 0x000012F4
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Detonator.ComponentKey, out objectLoader))
			{
				this._isArmed = objectLoader.Get(Detonator.IsArmedKey);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003124 File Offset: 0x00001324
		public void Evaluate()
		{
			if (!this._isArmed && this._automatable.State == ConnectionState.On)
			{
				this.Arm();
				return;
			}
			if (this._isArmed && this._automatable.State != ConnectionState.On && this._timeWhenArmed.Equals(Time.time))
			{
				this.Disarm();
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000317C File Offset: 0x0000137C
		public void Arm()
		{
			this._isArmed = true;
			this._timeWhenArmed = Time.time;
			this._recoverableGoodProvider.DisableGoodRecovery();
			this._illuminatorToggle.TurnOn();
			EntityComponent understructureEntity = this._understructureConstraint.UnderstructureEntity;
			if (understructureEntity == null)
			{
				return;
			}
			Dynamite component = understructureEntity.GetComponent<Dynamite>();
			if (component == null)
			{
				return;
			}
			component.Trigger();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000031D0 File Offset: 0x000013D0
		public void Disarm()
		{
			this._isArmed = false;
			this._recoverableGoodProvider.EnableGoodRecovery();
			this._illuminatorToggle.TurnOff();
			EntityComponent understructureEntity = this._understructureConstraint.UnderstructureEntity;
			if (understructureEntity == null)
			{
				return;
			}
			Dynamite component = understructureEntity.GetComponent<Dynamite>();
			if (component == null)
			{
				return;
			}
			component.Disarm();
		}

		// Token: 0x04000034 RID: 52
		public static readonly ComponentKey ComponentKey = new ComponentKey("Detonator");

		// Token: 0x04000035 RID: 53
		public static readonly PropertyKey<bool> IsArmedKey = new PropertyKey<bool>("IsArmed");

		// Token: 0x04000036 RID: 54
		public Automatable _automatable;

		// Token: 0x04000037 RID: 55
		public UnderstructureConstraint _understructureConstraint;

		// Token: 0x04000038 RID: 56
		public RecoverableGoodProvider _recoverableGoodProvider;

		// Token: 0x04000039 RID: 57
		public IlluminatorToggle _illuminatorToggle;

		// Token: 0x0400003A RID: 58
		public bool _isArmed;

		// Token: 0x0400003B RID: 59
		public float _timeWhenArmed;
	}
}
