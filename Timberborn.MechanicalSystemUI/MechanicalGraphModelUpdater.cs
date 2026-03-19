using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.MechanicalSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000012 RID: 18
	public class MechanicalGraphModelUpdater : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002764 File Offset: 0x00000964
		public MechanicalGraphModelUpdater(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027B5 File Offset: 0x000009B5
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027C4 File Offset: 0x000009C4
		public void UpdateSingleton()
		{
			if (this._dirtyGraphs.Count > 0)
			{
				foreach (MechanicalGraph mechanicalGraph in this._dirtyGraphs)
				{
					if (mechanicalGraph.Valid)
					{
						this.UpdateModels(mechanicalGraph);
					}
				}
				this._dirtyGraphs.Clear();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002838 File Offset: 0x00000A38
		[OnEvent]
		public void OnMechanicalGraphGeneratorAdded(MechanicalGraphGeneratorAddedEvent mechanicalGraphGeneratorAddedEvent)
		{
			this._dirtyGraphs.Add(mechanicalGraphGeneratorAddedEvent.MechanicalGraph);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000284C File Offset: 0x00000A4C
		[OnEvent]
		public void OnMechanicalGraphGeneratorUpdated(MechanicalGraphGeneratorUpdatedEvent mechanicalGraphGeneratorUpdatedEvent)
		{
			this._dirtyGraphs.Add(mechanicalGraphGeneratorUpdatedEvent.MechanicalGraph);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002860 File Offset: 0x00000A60
		public void UpdateModels(MechanicalGraph mechanicalGraph)
		{
			this._untouchedShafts.Clear();
			this._untouchedShafts.AddRange(from node in mechanicalGraph.Nodes
			where node.IsShaft
			select node);
			foreach (MechanicalNode mechanicalNode in this._untouchedShafts)
			{
				mechanicalNode.ResetAllTransputRotations();
			}
			this.TraverseFromGenerators(mechanicalGraph);
			this.TraverseRemainingShafts();
			this.UpdateModels();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002904 File Offset: 0x00000B04
		public void TraverseFromGenerators(MechanicalGraph mechanicalGraph)
		{
			foreach (MechanicalNode node2 in from node in mechanicalGraph.Nodes
			where node.IsGenerator && !node.IgnoreRotation
			select node)
			{
				this.TraverseToNextIntersection(node2);
			}
			this.TraverseRemainingIntersections();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000297C File Offset: 0x00000B7C
		public void TraverseRemainingShafts()
		{
			while (!this._untouchedShafts.IsEmpty<MechanicalNode>())
			{
				this.TraverseToNextIntersection(this._untouchedShafts.First<MechanicalNode>());
				this.TraverseRemainingIntersections();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void TraverseRemainingIntersections()
		{
			while (!this._intersections.IsEmpty<MechanicalNode>())
			{
				this.TraverseToNextIntersection(this._intersections.Dequeue());
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000029C8 File Offset: 0x00000BC8
		public void TraverseToNextIntersection(MechanicalNode node)
		{
			this._untouchedShafts.Remove(node);
			foreach (Transput transput in node.TransputsWithConnections())
			{
				this.TraverseToNextIntersection(transput.ConnectedTransput);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A28 File Offset: 0x00000C28
		public void TraverseToNextIntersection(Transput transput)
		{
			while (transput != null && transput.ParentNode.IsShaft)
			{
				MechanicalNode parentNode = transput.ParentNode;
				this._nodesToUpdate.Add(parentNode);
				if (transput.ConnectedTransput.ReversedRotation == transput.ReversedRotation)
				{
					transput.ReverseRotation();
				}
				Transput nextSingleTransput = this.GetNextSingleTransput(transput);
				if (nextSingleTransput != null)
				{
					if (nextSingleTransput.RotationMatches(transput) && nextSingleTransput.ConnectedNode.IsShaft)
					{
						nextSingleTransput.ReverseRotation();
					}
					transput = nextSingleTransput.ConnectedTransput;
				}
				else
				{
					if (this._untouchedShafts.Contains(parentNode))
					{
						this._untouchedShafts.Remove(parentNode);
						this._intersections.Enqueue(parentNode);
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public void UpdateModels()
		{
			foreach (MechanicalNode mechanicalNode in this._nodesToUpdate)
			{
				mechanicalNode.GetComponent<MechanicalModel>().UpdateModel();
			}
			this._nodesToUpdate.Clear();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B34 File Offset: 0x00000D34
		public Transput GetNextSingleTransput(Transput input)
		{
			foreach (Transput transput in input.ParentNode.TransputsWithConnections())
			{
				if (transput != input)
				{
					this._transputCache.Add(transput);
				}
			}
			if (this._transputCache.Count == 1)
			{
				Transput result = this._transputCache[0];
				this._transputCache.Clear();
				return result;
			}
			this._transputCache.Clear();
			return null;
		}

		// Token: 0x04000028 RID: 40
		public readonly EventBus _eventBus;

		// Token: 0x04000029 RID: 41
		public readonly HashSet<MechanicalNode> _untouchedShafts = new HashSet<MechanicalNode>();

		// Token: 0x0400002A RID: 42
		public readonly HashSet<MechanicalNode> _nodesToUpdate = new HashSet<MechanicalNode>();

		// Token: 0x0400002B RID: 43
		public readonly Queue<MechanicalNode> _intersections = new Queue<MechanicalNode>();

		// Token: 0x0400002C RID: 44
		public readonly HashSet<MechanicalGraph> _dirtyGraphs = new HashSet<MechanicalGraph>();

		// Token: 0x0400002D RID: 45
		public readonly List<Transput> _transputCache = new List<Transput>();
	}
}
