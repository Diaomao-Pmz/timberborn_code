using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x0200001D RID: 29
	public class MechanicalNodeTransformHeight : BaseComponent, IAwakableComponent, IUpdatableComponent, IFinishedStateListener, IPersistentEntity
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00003E5D File Offset: 0x0000205D
		public MechanicalNodeTransformHeight(NonlinearAnimationManager nonlinearAnimationManager)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003E6C File Offset: 0x0000206C
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._mechanicalNodeTransformHeightSpec = base.GetComponent<MechanicalNodeTransformHeightSpec>();
			this._transform = base.GameObject.FindChildTransform(this._mechanicalNodeTransformHeightSpec.TransformName);
			this._initialHeight = this._transform.localPosition.y;
			base.DisableComponent();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002482 File Offset: 0x00000682
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000248A File Offset: 0x0000068A
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003EC9 File Offset: 0x000020C9
		public void Update()
		{
			this.MoveTransform();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003ED1 File Offset: 0x000020D1
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(MechanicalNodeTransformHeight.MechanicalNodeTransformHeightKey).Set(MechanicalNodeTransformHeight.TransformHeightKey, this._transform.localPosition.y);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003EF8 File Offset: 0x000020F8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(MechanicalNodeTransformHeight.MechanicalNodeTransformHeightKey);
			Vector3 localPosition = this._transform.localPosition;
			this._transform.localPosition = new Vector3(localPosition.x, component.Get(MechanicalNodeTransformHeight.TransformHeightKey), localPosition.z);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003F44 File Offset: 0x00002144
		public void MoveTransform()
		{
			float num = Time.deltaTime * this._mechanicalNodeTransformHeightSpec.ChangeSpeed * this._nonlinearAnimationManager.SpeedMultiplier;
			Vector3 localPosition = this._transform.localPosition;
			float num2 = Mathf.SmoothStep(localPosition.y, this.GetTargetHeight(), num);
			this._transform.localPosition = new Vector3(localPosition.x, num2, localPosition.z);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003FAC File Offset: 0x000021AC
		public float GetTargetHeight()
		{
			float num = this._mechanicalNode.ActiveAndPowered ? this._mechanicalNode.PowerEfficiency : 0f;
			return this._initialHeight + this._mechanicalNodeTransformHeightSpec.Range * num;
		}

		// Token: 0x04000057 RID: 87
		public static readonly ComponentKey MechanicalNodeTransformHeightKey = new ComponentKey("MechanicalNodeTransformHeight");

		// Token: 0x04000058 RID: 88
		public static readonly PropertyKey<float> TransformHeightKey = new PropertyKey<float>("TransformHeight");

		// Token: 0x04000059 RID: 89
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x0400005A RID: 90
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400005B RID: 91
		public MechanicalNodeTransformHeightSpec _mechanicalNodeTransformHeightSpec;

		// Token: 0x0400005C RID: 92
		public Transform _transform;

		// Token: 0x0400005D RID: 93
		public float _initialHeight;
	}
}
