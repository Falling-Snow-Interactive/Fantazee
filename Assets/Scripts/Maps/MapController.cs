using Fsi.Gameplay;
using ProjectYahtzee.Maps.Nodes;
using ProjectYahtzee.Maps.Nodes.Information;
using ProjectYahtzee.Maps.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

    namespace ProjectYahtzee.Maps
    {
        public class MapController : MbSingleton<MapController>
        {
            [Header("Map")]

            [SerializeField]
            private Transform nodeContainer;

            [SerializeField]
            private Transform connectionContainer;

            private Map debugMap;

            private void Start()
            {
                float offset = (MapSettings.Settings.MapProperties.Size.x - 1) * MapSettings.Settings.NodeSpacing.x / 2f;
                if (Camera.main != null)
                {
                    var vector3 = Camera.main.transform.position;
                    vector3.x = offset;
                    Camera.main.transform.position = vector3;
                }

                ShowMap();
            }

            private void ShowMap()
            {
                PlaceNode(GameController.Instance.GameInstance.Map.Root);
            }

            private void PlaceNode(Node node)
            {
                NodeShape nodeShape = Instantiate(MapSettings.Settings.NodeShape, nodeContainer); ;
                nodeShape.SetNode(node);
                Vector3 pos = MapUtility.NodeToWorldPosition(node.position);
                nodeShape.transform.position = pos;

                foreach (var next in node.Next)
                {
                    NodeConnection connection = Instantiate(MapSettings.Settings.NodeConnection, connectionContainer);
                    connection.SetLine(node, next);
                    PlaceNode(next);
                }
            }

            public void GenerateDebugMap()
            {
                debugMap = new Map(MapSettings.Settings.MapProperties, (uint)Random.Range(0, uint.MaxValue));
            }

            #if UNITY_EDITOR
            private void OnDrawGizmos()
            {
                if (!Application.isPlaying && debugMap != null)
                {
                    DrawNodeGizmos(debugMap.Root);
                }
                else if(GameController.Instance?.GameInstance?.Map != null)
                {
                    DrawNodeGizmos(GameController.Instance.GameInstance.Map.Root);
                }
            }

            private void OnDrawGizmosSelected()
            {
                if (!Application.isPlaying && debugMap != null)
                {
                    DrawNodeGizmos(debugMap.Root);
                }
                else if(GameController.Instance?.GameInstance?.Map != null)
                {
                    DrawNodeGizmos(GameController.Instance.GameInstance.Map.Root);
                }
            }

            private void DrawNodeGizmos(Node node)
            {
                if (node == null)
                {
                    return;
                }
            
                if (MapSettings.Settings.NodeInformation.TryGetInformation(node.type, out NodeInformation info))
                {
                    Gizmos.color = info.Color;
                    Vector3 center = MapUtility.NodeToWorldPosition(node.position);
                    DrawSpherePlaying(center, MapSettings.Settings.NodeRadius);
                    foreach (Node n in node.Next)
                    {
                        Vector3 nextCenter = MapUtility.NodeToWorldPosition(n.position);
                        Gizmos.color = Color.black;
                        Gizmos.DrawLine(center, nextCenter);
                        DrawNodeGizmos(n);
                    }
                }
            }

            private void DrawSpherePlaying(Vector3 center, float radius)
            {
                if (Application.isPlaying)
                {
                    Gizmos.DrawWireSphere(center, radius);
                }
                else
                {
                    Gizmos.DrawSphere(center, radius);
                }
            }
            #endif
        }
    }
