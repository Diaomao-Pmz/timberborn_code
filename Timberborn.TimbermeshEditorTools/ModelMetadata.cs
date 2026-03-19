using System;
using System.Collections.Generic;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.TimbermeshEditorTools
{
	// Token: 0x02000004 RID: 4
	public class ModelMetadata : MonoBehaviour
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public static void Create(GameObject container, Model model)
		{
			ModelMetadata modelMetadata = container.AddComponent<ModelMetadata>();
			modelMetadata._name = model.Name;
			modelMetadata._version = model.Version;
			foreach (Node node in model.Nodes)
			{
				int nodeDepth = ModelMetadata.GetNodeDepth(node, model.Nodes);
				ModelMetadata.AddNode(modelMetadata, node, nodeDepth);
				ModelMetadata.AddAnimations(modelMetadata, node);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002123 File Offset: 0x00000323
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000212B File Offset: 0x0000032B
		public int Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002133 File Offset: 0x00000333
		public List<NodeMetadata> Nodes
		{
			get
			{
				return this._nodes;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000213B File Offset: 0x0000033B
		public List<VertexAnimationMetadata> VertexAnimations
		{
			get
			{
				return this._vertexAnimations;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002143 File Offset: 0x00000343
		public List<NodeAnimationMetadata> NodeAnimations
		{
			get
			{
				return this._nodeAnimations;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000214B File Offset: 0x0000034B
		public static void AddNode(ModelMetadata modelMetadata, Node node, int nodeDepth)
		{
			modelMetadata._nodes.Add(new NodeMetadata(node.Name, nodeDepth, node.VertexCount));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216C File Offset: 0x0000036C
		public static int GetNodeDepth(Node node, IReadOnlyList<Node> allNodes)
		{
			int num = 0;
			while (node.Parent >= 0)
			{
				node = allNodes[node.Parent];
				num++;
			}
			return num;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000219C File Offset: 0x0000039C
		public static void AddAnimations(ModelMetadata modelMetadata, Node node)
		{
			foreach (VertexAnimation animation in node.VertexAnimations)
			{
				modelMetadata._vertexAnimations.Add(new VertexAnimationMetadata(node.Name, animation));
			}
			foreach (NodeAnimation animation2 in node.NodeAnimations)
			{
				modelMetadata._nodeAnimations.Add(new NodeAnimationMetadata(node.Name, animation2));
			}
		}

		// Token: 0x04000006 RID: 6
		[HideInInspector]
		[SerializeField]
		public string _name;

		// Token: 0x04000007 RID: 7
		[HideInInspector]
		[SerializeField]
		public int _version;

		// Token: 0x04000008 RID: 8
		[HideInInspector]
		[SerializeField]
		public List<NodeMetadata> _nodes = new List<NodeMetadata>();

		// Token: 0x04000009 RID: 9
		[HideInInspector]
		[SerializeField]
		public List<VertexAnimationMetadata> _vertexAnimations = new List<VertexAnimationMetadata>();

		// Token: 0x0400000A RID: 10
		[HideInInspector]
		[SerializeField]
		public List<NodeAnimationMetadata> _nodeAnimations = new List<NodeAnimationMetadata>();
	}
}
