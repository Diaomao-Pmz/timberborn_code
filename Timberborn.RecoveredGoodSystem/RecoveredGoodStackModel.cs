using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000015 RID: 21
	public class RecoveredGoodStackModel : BaseComponent, IAwakableComponent, IPersistentEntity
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003300 File Offset: 0x00001500
		public void Awake()
		{
			string modelName = base.GetComponent<RecoveredGoodStackModelSpec>().ModelName;
			this._model = base.GameObject.FindChildTransform(modelName);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000332B File Offset: 0x0000152B
		public void SetRotation(int rotation)
		{
			this._model.localRotation = Quaternion.Euler(0f, (float)rotation, 0f);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000334C File Offset: 0x0000154C
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(RecoveredGoodStackModel.RecoveredGoodStackModelKey).Set(RecoveredGoodStackModel.RotationKey, (int)this._model.rotation.eulerAngles.y);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003388 File Offset: 0x00001588
		public void Load(IEntityLoader entityLoader)
		{
			int rotation = entityLoader.GetComponent(RecoveredGoodStackModel.RecoveredGoodStackModelKey).Get(RecoveredGoodStackModel.RotationKey);
			this.SetRotation(rotation);
		}

		// Token: 0x04000048 RID: 72
		public static readonly ComponentKey RecoveredGoodStackModelKey = new ComponentKey("RecoveredGoodStackModel");

		// Token: 0x04000049 RID: 73
		public static readonly PropertyKey<int> RotationKey = new PropertyKey<int>("Rotation");

		// Token: 0x0400004A RID: 74
		public Transform _model;
	}
}
