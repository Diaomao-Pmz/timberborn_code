using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.PrioritySystem;
using Timberborn.WorldPersistence;

namespace Timberborn.BuilderPrioritySystem
{
	// Token: 0x02000004 RID: 4
	public class BuilderPrioritizable : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<BuilderPrioritizable>, IDuplicable, IPrioritizable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002168 File Offset: 0x00000368
		public event EventHandler PrioritizableEnabled;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000007 RID: 7 RVA: 0x000021A0 File Offset: 0x000003A0
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x000021D8 File Offset: 0x000003D8
		public event EventHandler PrioritizableDisabled;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000220D File Offset: 0x0000040D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002215 File Offset: 0x00000415
		public Priority Priority { get; private set; } = Priority.Normal;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000221E File Offset: 0x0000041E
		public bool IsDuplicable
		{
			get
			{
				return base.Enabled;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002226 File Offset: 0x00000426
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			base.DisableComponent();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000223A File Offset: 0x0000043A
		public void Enable()
		{
			base.EnableComponent();
			EventHandler prioritizableEnabled = this.PrioritizableEnabled;
			if (prioritizableEnabled == null)
			{
				return;
			}
			prioritizableEnabled(this, EventArgs.Empty);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002258 File Offset: 0x00000458
		public void Disable()
		{
			this.Priority = Priority.Normal;
			base.DisableComponent();
			EventHandler prioritizableDisabled = this.PrioritizableDisabled;
			if (prioritizableDisabled == null)
			{
				return;
			}
			prioritizableDisabled(this, EventArgs.Empty);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002280 File Offset: 0x00000480
		public void SetPriority(Priority priority)
		{
			Priority priority2 = this.Priority;
			this.Priority = priority;
			EventHandler<PriorityChangedEventArgs> priorityChanged = this.PriorityChanged;
			if (priorityChanged == null)
			{
				return;
			}
			priorityChanged(this, new PriorityChangedEventArgs(priority2));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022B2 File Offset: 0x000004B2
		public void Save(IEntitySaver entitySaver)
		{
			if (this.Priority != Priority.Normal)
			{
				entitySaver.GetComponent(BuilderPrioritizable.BuilderPrioritizableKey).Set<Priority>(BuilderPrioritizable.PriorityKey, this.Priority);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022D8 File Offset: 0x000004D8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(BuilderPrioritizable.BuilderPrioritizableKey, out objectLoader))
			{
				this.Priority = objectLoader.Get<Priority>(BuilderPrioritizable.PriorityKey);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002305 File Offset: 0x00000505
		public void DuplicateFrom(BuilderPrioritizable source)
		{
			if ((base.Enabled || !this._blockObject.IsFinished) && source.Enabled)
			{
				this.SetPriority(source.Priority);
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly ComponentKey BuilderPrioritizableKey = new ComponentKey("BuilderPrioritizable");

		// Token: 0x04000007 RID: 7
		public static readonly PropertyKey<Priority> PriorityKey = new PropertyKey<Priority>("Priority");

		// Token: 0x0400000C RID: 12
		public BlockObject _blockObject;
	}
}
