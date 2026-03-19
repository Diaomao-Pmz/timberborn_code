using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.TemplateSystem;

namespace Timberborn.EntityNaming
{
	// Token: 0x0200000D RID: 13
	public class NamedEntityGameObjectSynchronizer : BaseComponent, IAwakableComponent, IPostInitializableEntity
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000025BA File Offset: 0x000007BA
		public void Awake()
		{
			this._templateSpec = base.GetComponent<TemplateSpec>();
			this._namedEntity = base.GetComponent<NamedEntity>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025D4 File Offset: 0x000007D4
		public void PostInitializeEntity()
		{
			this.UpdateGameObjectName();
			this._namedEntity.EntityNameChanged += this.OnEntityNameChanged;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025F3 File Offset: 0x000007F3
		public void OnEntityNameChanged(object sender, EventArgs e)
		{
			this.UpdateGameObjectName();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025FB File Offset: 0x000007FB
		public void UpdateGameObjectName()
		{
			base.GameObject.name = this._templateSpec.TemplateName + " " + this._namedEntity.EntityName;
		}

		// Token: 0x04000016 RID: 22
		public TemplateSpec _templateSpec;

		// Token: 0x04000017 RID: 23
		public NamedEntity _namedEntity;
	}
}
