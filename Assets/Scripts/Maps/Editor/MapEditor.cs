using System.Collections.Generic;
using Fantazee.Maps.Nodes;
using Fantazee.Maps.Nodes.Information;
using Fantazee.Maps.Settings;
using fsi.prototyping.Spacers;
using Fsi.Spline;
using Fsi.Spline.Vectors;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Maps
{
    [CustomEditor(typeof(Map))]
    public class MapEditor : Editor
    {
        // Pref variables
        private const string PrefPrefix = "Map";
        private string LineThicknessPref => $"{PrefPrefix}.{nameof(lineThickness)}";
        private string NodeRadiusPref => $"{PrefPrefix}.{nameof(nodeRadius)}";
        
        private float lineThickness = 5f;
        private float nodeRadius = 0.1f;
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            
            lineThickness = EditorPrefs.GetFloat(LineThicknessPref, lineThickness);
            nodeRadius = EditorPrefs.GetFloat(NodeRadiusPref, nodeRadius);

            if (target is Map map)
            {
                root.Add(new Spacer());
                VisualElement inspector = new();
                InspectorElement.FillDefaultInspector(inspector, serializedObject, this);
                root.Add(inspector);
                
                List<Node> nodes = map.Nodes;
                
                VisualElement connectionGroup = new();
                root.Add(connectionGroup);

                VisualElement dropdownGroup = new() { style = { flexDirection = FlexDirection.Row } };
                root.Add(dropdownGroup);

                List<string> choices = new();
                foreach (Node node in nodes)
                {
                    choices.Add(node.name);
                }

                DropdownField fromSelect = new(choices, 0) { label = "From"};
                DropdownField toSelect = new(choices, 0) { label = "To"};
                dropdownGroup.Add(fromSelect);
                dropdownGroup.Add(new VisualElement(){style = { flexGrow = 1}});
                dropdownGroup.Add(toSelect);

                Button connectButton = new() { text = "Connect" };
                root.Add(connectButton);

                connectButton.clicked += () =>
                                         {
                                             int selectedFrom = fromSelect.index;
                                             int selectedTo = toSelect.index;
                                             
                                             Node from = nodes[selectedFrom];
                                             Node to = nodes[selectedTo];

                                             from.Next.Add(selectedTo);
                                             
                                             Debug.Log($"Map: Connected {from} - {to}");
                                             serializedObject.ApplyModifiedProperties();
                                             SceneView.RepaintAll();
                                         };
            }
            
            // Debugging settings
            root.Add(new Spacer());
            Foldout debug = new() { text = "Debug" };
            root.Add(debug);
            
            Slider lineThicknessField = new("Line Thickness")
                                              { 
                                                  lowValue = 0, 
                                                  highValue = 20, 
                                                  value = lineThickness,
                                                  showInputField = true,
                                              };
            lineThicknessField.RegisterValueChangedCallback(evt =>
                                                                  {
                                                                      lineThickness = evt.newValue;
                                                                      
                                                                      EditorPrefs.SetFloat(LineThicknessPref, lineThickness);
                                                                      
                                                                      SceneView.RepaintAll();
                                                                  });
            debug.Add(lineThicknessField);
            
            Slider nodeRadiusField = new("NodeRadius")
                                        { 
                                            lowValue = 0, 
                                            highValue = 2, 
                                            value = nodeRadius,
                                            showInputField = true,
                                        };
            nodeRadiusField.RegisterValueChangedCallback(evt =>
                                                         {
                                                             nodeRadius = evt.newValue;
                                                             EditorPrefs.SetFloat(NodeRadiusPref, nodeRadius);
                                                             SceneView.RepaintAll();
                                                         });
            debug.Add(nodeRadiusField);

            return root;
        }
        
        private void OnSceneGUI()
        {
            bool changed = false;
            if (target is Map map)
            {
                List<Node> nodes = map.Nodes;
                foreach (Node node in nodes)
                {
                    DrawConnections(node, map.Nodes);
                    if (DrawNode(node))
                    {
                        changed = true;
                    }
                }
            }

            if (changed)
            {
                serializedObject.ApplyModifiedProperties();
                SceneView.RepaintAll();
            }
        }

        private bool DrawNode(Node node)
        {
            bool changed = false;
            
            GUIStyle style = GUIStyle.none;
            style.normal.textColor = Color.black;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 16;
            Vector3 position = node.Point.value + Vector3.up * 0.25f;
            Handles.Label(position, node.name, style);
            
            if (MapSettings.Settings.NodeInformation.TryGetInformation(node.Type, out NodeInformation info))
            {
                Handles.color = info.Color;
                Handles.DrawSolidDisc(node.Point.value, Vector3.back, nodeRadius);
            }
            
            if (node.Point.DrawPointHandles(serializedObject) 
                || node.Point.DrawTangentHandles(serializedObject))
            {
                changed = true;
            }
            return changed;
        }

        private void DrawConnections(Node node, List<Node> nodes)
        {
            foreach (int c in node.Next)
            {
                Handles.color = Color.grey;
                Handles.DrawLine(node.Point.value, nodes[c].Point.value, lineThickness);
                
                Handles.color = Color.black;
                Vector3Spline spline = new(node.Point, nodes[c].Point)
                                       {
                                           closed = false,
                                           curveType = CurveType.Bezier
                                       };
                List<Vector3> points = spline.GetPointsValue(10);
                for (int i = 0; i < points.Count - 1; i++)
                {
                    Vector3 p0 = points[i];
                    Vector3 p1 = points[i + 1];
                    Handles.DrawLine(p0, p1, lineThickness);
                }
            }
        }
    }
}