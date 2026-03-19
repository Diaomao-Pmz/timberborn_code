using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BatchControl;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.MechanicalSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.PowerBatchControl
{
	// Token: 0x02000005 RID: 5
	public class MechanicalBatchControlTab : BatchControlTab
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000021B9 File Offset: 0x000003B9
		public MechanicalBatchControlTab(VisualElementLoader visualElementLoader, BatchControlDistrict batchControlDistrict, MechanicalBatchControlRowFactory mechanicalBatchControlRowFactory, EventBus eventBus, BatchControlRowGroupFactory batchControlRowGroupFactory) : base(visualElementLoader, batchControlDistrict, eventBus)
		{
			this._mechanicalBatchControlRowFactory = mechanicalBatchControlRowFactory;
			this._batchControlRowGroupFactory = batchControlRowGroupFactory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021DF File Offset: 0x000003DF
		public override string TabNameLocKey
		{
			get
			{
				return "BatchControl.Mechanical";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021E6 File Offset: 0x000003E6
		public override string TabImage
		{
			get
			{
				return "Mechanical";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021ED File Offset: 0x000003ED
		public override string BindingKey
		{
			get
			{
				return "MechanicalTab";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021F4 File Offset: 0x000003F4
		public override bool IgnoreDistrictSelection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021F7 File Offset: 0x000003F7
		[OnEvent]
		public void OnMechanicalGraphCreated(MechanicalGraphCreatedEvent mechanicalGraphCreatedEvent)
		{
			this.HideAndMarkForRefresh();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F7 File Offset: 0x000003F7
		[OnEvent]
		public void OnMechanicalGraphRemoved(MechanicalGraphRemovedEvent mechanicalGraphRemovedEvent)
		{
			this.HideAndMarkForRefresh();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002200 File Offset: 0x00000400
		public override IEnumerable<BatchControlRowGroup> GetRowGroups(IEnumerable<EntityComponent> entities)
		{
			IEnumerable<MechanicalNode> nodes = from entity in entities.Where(delegate(EntityComponent entity)
			{
				MechanicalBuilding component = entity.GetComponent<MechanicalBuilding>();
				return component != null && component.Enabled;
			})
			select entity.GetComponent<MechanicalNode>();
			this.GatherGraphs(nodes);
			return this.GetRows();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002264 File Offset: 0x00000464
		public void HideAndMarkForRefresh()
		{
			base.HideContent();
			base.IsDirty = true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002274 File Offset: 0x00000474
		public void GatherGraphs(IEnumerable<MechanicalNode> nodes)
		{
			foreach (MechanicalNode mechanicalNode in nodes)
			{
				MechanicalGraph graph = mechanicalNode.Graph;
				if (graph != null)
				{
					this._graphs.GetOrAdd(graph).Add(mechanicalNode);
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022D4 File Offset: 0x000004D4
		public IEnumerable<BatchControlRowGroup> GetRows()
		{
			foreach (MechanicalGraph mechanicalGraph in this._graphs.Keys)
			{
				BatchControlRow header = this._mechanicalBatchControlRowFactory.Create(mechanicalGraph);
				BatchControlRowGroup batchControlRowGroup = this._batchControlRowGroupFactory.CreateUnsorted(header);
				foreach (MechanicalNode mechanicalNode in this._graphs[mechanicalGraph])
				{
					EntityComponent component = mechanicalNode.GetComponent<EntityComponent>();
					batchControlRowGroup.AddRow(this._mechanicalBatchControlRowFactory.Create(component));
				}
				yield return batchControlRowGroup;
			}
			Dictionary<MechanicalGraph, List<MechanicalNode>>.KeyCollection.Enumerator enumerator = default(Dictionary<MechanicalGraph, List<MechanicalNode>>.KeyCollection.Enumerator);
			this._graphs.Clear();
			yield break;
			yield break;
		}

		// Token: 0x0400000D RID: 13
		public readonly MechanicalBatchControlRowFactory _mechanicalBatchControlRowFactory;

		// Token: 0x0400000E RID: 14
		public readonly BatchControlRowGroupFactory _batchControlRowGroupFactory;

		// Token: 0x0400000F RID: 15
		public readonly Dictionary<MechanicalGraph, List<MechanicalNode>> _graphs = new Dictionary<MechanicalGraph, List<MechanicalNode>>();
	}
}
