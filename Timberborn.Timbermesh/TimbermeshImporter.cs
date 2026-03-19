using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.Timbermesh
{
	// Token: 0x0200000F RID: 15
	public class TimbermeshImporter
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000026EB File Offset: 0x000008EB
		public TimbermeshImporter(StaticMeshBuilder staticMeshBuilder, IEnumerable<IModelPostprocessor> modelPostprocessors)
		{
			this._staticMeshBuilder = staticMeshBuilder;
			this._modelPostprocessors = modelPostprocessors.ToImmutableArray<IModelPostprocessor>();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002706 File Offset: 0x00000906
		public void Import(Stream stream, Transform parent)
		{
			this.CreateAndProcessModel(stream, parent, 0);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002711 File Offset: 0x00000911
		public void ImportAsPreview(Stream stream, Transform parent)
		{
			this.CreateAndProcessModel(stream, parent, 60);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002720 File Offset: 0x00000920
		public ImportDetails Import(Model model, Transform parent)
		{
			ImportDetails importDetails = new ImportDetails(parent);
			this.ProcessModel(model, 0, importDetails);
			return importDetails;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002740 File Offset: 0x00000940
		public void CreateAndProcessModel(Stream stream, Transform parent, HideFlags hideFlags)
		{
			ImportDetails details = new ImportDetails(parent);
			Model model = TimbermeshReader.ReadFromStream(stream);
			this.ProcessModel(model, hideFlags, details);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002764 File Offset: 0x00000964
		public void ProcessModel(Model model, HideFlags hideFlags, ImportDetails details)
		{
			this.CreateMeshes(model, hideFlags, details);
			TimbermeshImporter.CreateRelations(model, details);
			this.PostprocessModel(details);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002780 File Offset: 0x00000980
		public void CreateMeshes(Model model, HideFlags hideFlags, ImportDetails details)
		{
			foreach (Node node in model.Nodes)
			{
				GameObject gameObject = new GameObject(node.Name)
				{
					hideFlags = hideFlags
				};
				this._staticMeshBuilder.BuildMesh(gameObject, node);
				details.AddObject(gameObject, node);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027D0 File Offset: 0x000009D0
		public static void CreateRelations(Model model, ImportDetails details)
		{
			foreach (KeyValuePair<Node, GameObject> keyValuePair in details.CreatedObjectsMap)
			{
				Node node;
				GameObject gameObject;
				keyValuePair.Deconstruct(ref node, ref gameObject);
				Node node2 = node;
				GameObject gameObject2 = gameObject;
				if (node2.Parent >= 0)
				{
					Node key = model.Nodes[node2.Parent];
					Transform transform = details.CreatedObjectsMap[key].transform;
					gameObject2.transform.parent = transform;
				}
				else
				{
					gameObject2.transform.parent = details.Root;
				}
				gameObject2.transform.localPosition = node2.Position.ToVector3();
				gameObject2.transform.localRotation = node2.Rotation.ToQuaternion();
				gameObject2.transform.localScale = node2.Scale.ToVector3();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028BC File Offset: 0x00000ABC
		public void PostprocessModel(ImportDetails details)
		{
			foreach (IModelPostprocessor modelPostprocessor in this._modelPostprocessors)
			{
				modelPostprocessor.Postprocess(details);
			}
		}

		// Token: 0x04000013 RID: 19
		public readonly StaticMeshBuilder _staticMeshBuilder;

		// Token: 0x04000014 RID: 20
		public readonly ImmutableArray<IModelPostprocessor> _modelPostprocessors;
	}
}
