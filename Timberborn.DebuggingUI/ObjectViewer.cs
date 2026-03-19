using System;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000010 RID: 16
	public class ObjectViewer : IUpdatableSingleton
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003685 File Offset: 0x00001885
		public ObjectViewer(ObjectViewerNodeFactory objectViewerNodeFactory)
		{
			this._objectViewerNodeFactory = objectViewerNodeFactory;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003694 File Offset: 0x00001894
		public void Initialize(ScrollView root)
		{
			this._root = root;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000036A0 File Offset: 0x000018A0
		public void SetObject(object viewedObject)
		{
			this._root.Clear();
			this._viewedObject = viewedObject;
			this._rootNode = this._objectViewerNodeFactory.CreateRoot(viewedObject.GetType().Name);
			this._rootNode.Update(viewedObject);
			this._root.Add(this._rootNode.Root);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000036FD File Offset: 0x000018FD
		public void UpdateSingleton()
		{
			ObjectViewerNode rootNode = this._rootNode;
			if (rootNode == null)
			{
				return;
			}
			rootNode.Update(this._viewedObject);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003715 File Offset: 0x00001915
		public void Clear()
		{
			this._root.Clear();
			this._viewedObject = null;
			this._rootNode = null;
		}

		// Token: 0x04000050 RID: 80
		public readonly ObjectViewerNodeFactory _objectViewerNodeFactory;

		// Token: 0x04000051 RID: 81
		public ScrollView _root;

		// Token: 0x04000052 RID: 82
		public object _viewedObject;

		// Token: 0x04000053 RID: 83
		public ObjectViewerNode _rootNode;
	}
}
