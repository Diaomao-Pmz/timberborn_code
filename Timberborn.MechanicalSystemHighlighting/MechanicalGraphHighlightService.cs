using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.MechanicalSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.MechanicalSystemHighlighting
{
	// Token: 0x02000007 RID: 7
	public class MechanicalGraphHighlightService : ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public MechanicalGraphHighlightService(Highlighter highlighter, EventBus eventBus, ISpecService specService, EntitySelectionService entitySelectionService, MechanicalGraphIterator mechanicalGraphIterator)
		{
			this._highlighter = highlighter;
			this._eventBus = eventBus;
			this._specService = specService;
			this._entitySelectionService = entitySelectionService;
			this._mechanicalGraphIterator = mechanicalGraphIterator;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000214E File Offset: 0x0000034E
		public void Load()
		{
			this._eventBus.Register(this);
			this._highlightColor = this._specService.GetSingleSpec<MechanicalNodeHighlighterSpec>().HighlightColor;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002172 File Offset: 0x00000372
		public void LateUpdateSingleton()
		{
			if (this._dirty)
			{
				this.RefreshHighlight();
				this._dirty = false;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002189 File Offset: 0x00000389
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			this._dirty = true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002189 File Offset: 0x00000389
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			this._dirty = true;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002189 File Offset: 0x00000389
		[OnEvent]
		public void OnMechanicalGraphCreated(MechanicalGraphCreatedEvent mechanicalGraphCreatedEvent)
		{
			this._dirty = true;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002189 File Offset: 0x00000389
		[OnEvent]
		public void OnMechanicalGraphRemoved(MechanicalGraphRemovedEvent mechanicalGraphRemovedEvent)
		{
			this._dirty = true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002192 File Offset: 0x00000392
		[OnEvent]
		public void OnSelectableObjectSelected(SelectableObjectSelectedEvent selectableObjectSelectedEvent)
		{
			this.HighlightSelectedNode();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000219A File Offset: 0x0000039A
		[OnEvent]
		public void OnSelectableObjectUnselectedEvent(SelectableObjectUnselectedEvent selectableObjectUnselectedEvent)
		{
			this.RemoveAllNodesFromHighlight();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021A2 File Offset: 0x000003A2
		public void AddNodeToHighlight(MechanicalNode mechanicalNode)
		{
			this._rootNodes.Add(mechanicalNode);
			this._dirty = true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021B8 File Offset: 0x000003B8
		public void RemoveAllNodesFromHighlight()
		{
			this._rootNodes.Clear();
			this._dirty = true;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021CC File Offset: 0x000003CC
		public void RefreshHighlight()
		{
			if (this._rootNodes.Count > 0)
			{
				this.HighlightNetworkFromRoot();
				return;
			}
			this._highlighter.UnhighlightAllSecondary();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021F0 File Offset: 0x000003F0
		public void HighlightSelectedNode()
		{
			SelectableObject selectedObject = this._entitySelectionService.SelectedObject;
			if (selectedObject)
			{
				MechanicalNode component = selectedObject.GetComponent<MechanicalNode>();
				if (component != null)
				{
					this.AddNodeToHighlight(component);
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002224 File Offset: 0x00000424
		public void HighlightNetworkFromRoot()
		{
			this._highlighter.UnhighlightAllSecondary();
			this._mechanicalGraphIterator.Iterate(this._rootNodes, this._graphNodes, this.ShouldIncludeUnfinished());
			if (this._graphNodes.Count > 0)
			{
				foreach (MechanicalNode target in this._graphNodes)
				{
					this._highlighter.HighlightSecondary(target, this._highlightColor);
				}
				this._graphNodes.Clear();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022C4 File Offset: 0x000004C4
		public bool ShouldIncludeUnfinished()
		{
			foreach (MechanicalNode mechanicalNode in this._rootNodes)
			{
				BlockObject component = mechanicalNode.GetComponent<BlockObject>();
				if (component.IsUnfinished || component.IsPreview)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000008 RID: 8
		public readonly Highlighter _highlighter;

		// Token: 0x04000009 RID: 9
		public readonly EventBus _eventBus;

		// Token: 0x0400000A RID: 10
		public readonly ISpecService _specService;

		// Token: 0x0400000B RID: 11
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400000C RID: 12
		public readonly MechanicalGraphIterator _mechanicalGraphIterator;

		// Token: 0x0400000D RID: 13
		public readonly HashSet<MechanicalNode> _rootNodes = new HashSet<MechanicalNode>();

		// Token: 0x0400000E RID: 14
		public readonly HashSet<MechanicalNode> _graphNodes = new HashSet<MechanicalNode>();

		// Token: 0x0400000F RID: 15
		public Color _highlightColor;

		// Token: 0x04000010 RID: 16
		public bool _dirty;
	}
}
