using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;

namespace Timberborn.EntityNaming
{
	// Token: 0x02000016 RID: 22
	public class UniquelyNamedEntity : BaseComponent, IAwakableComponent, IPostInitializableEntity, IDeletableEntity
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600007C RID: 124 RVA: 0x00003070 File Offset: 0x00001270
		// (remove) Token: 0x0600007D RID: 125 RVA: 0x000030A8 File Offset: 0x000012A8
		public event EventHandler IsUniqueChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600007E RID: 126 RVA: 0x000030E0 File Offset: 0x000012E0
		// (remove) Token: 0x0600007F RID: 127 RVA: 0x00003118 File Offset: 0x00001318
		public event EventHandler EntityNameChanged;

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000314D File Offset: 0x0000134D
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003155 File Offset: 0x00001355
		public bool IsUnique { get; private set; }

		// Token: 0x06000082 RID: 130 RVA: 0x0000315E File Offset: 0x0000135E
		public UniquelyNamedEntity(UniquelyNamedEntityService uniquelyNamedEntityService)
		{
			this._uniquelyNamedEntityService = uniquelyNamedEntityService;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000316D File Offset: 0x0000136D
		public string EntityName
		{
			get
			{
				return this._namedEntity.EntityName;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000317A File Offset: 0x0000137A
		public void Awake()
		{
			this._namedEntity = base.GetComponent<NamedEntity>();
			this.IsUnique = true;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000318F File Offset: 0x0000138F
		public void PostInitializeEntity()
		{
			this.RegisterName();
			this._namedEntity.EntityNameChanged += this.OnEntityNameChanged;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000031AE File Offset: 0x000013AE
		public void DeleteEntity()
		{
			this._namedEntity.EntityNameChanged -= this.OnEntityNameChanged;
			this.UnregisterName();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000031CD File Offset: 0x000013CD
		public void SetUnique()
		{
			this.SetUnique(true);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000031D6 File Offset: 0x000013D6
		public void SetNonUnique()
		{
			this.SetUnique(false);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000031DF File Offset: 0x000013DF
		public void SetUnique(bool value)
		{
			if (this.IsUnique != value)
			{
				this.IsUnique = value;
				EventHandler isUniqueChanged = this.IsUniqueChanged;
				if (isUniqueChanged == null)
				{
					return;
				}
				isUniqueChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003207 File Offset: 0x00001407
		public void OnEntityNameChanged(object sender, EventArgs e)
		{
			this.UnregisterName();
			this.RegisterName();
			EventHandler entityNameChanged = this.EntityNameChanged;
			if (entityNameChanged == null)
			{
				return;
			}
			entityNameChanged(this, EventArgs.Empty);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000322B File Offset: 0x0000142B
		public void RegisterName()
		{
			this._registeredName = this._namedEntity.EntityName;
			if (this._registeredName != null)
			{
				this._uniquelyNamedEntityService.RegisterName(this._registeredName, this);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003258 File Offset: 0x00001458
		public void UnregisterName()
		{
			if (this._registeredName != null)
			{
				this._uniquelyNamedEntityService.UnregisterName(this._registeredName, this);
			}
		}

		// Token: 0x04000037 RID: 55
		public readonly UniquelyNamedEntityService _uniquelyNamedEntityService;

		// Token: 0x04000038 RID: 56
		public NamedEntity _namedEntity;

		// Token: 0x04000039 RID: 57
		public string _registeredName;
	}
}
