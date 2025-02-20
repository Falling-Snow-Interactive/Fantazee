using System;
using DG.Tweening;
using Fantazhee.Instance;
using Fantazhee.Maps.Nodes;
using Fsi.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fantazhee.Maps
    {
        public class MapController : MbSingleton<MapController>
        {
            private GameInstance GameInstance => GameController.Instance.GameInstance;
            
            [SerializeField]
            private new Camera camera;
            
            [SerializeField]
            private Map map;

            [SerializeField]
            private Transform player;

            [Header("Input")]

            [SerializeField]
            private InputActionReference cursorActionRef;
            private InputAction cursorAction;

            [SerializeField]
            private InputActionReference selectActionRef;
            private InputAction selectAction;

            private bool canInteract = false;

            protected override void Awake()
            {
                base.Awake();
                
                cursorAction = cursorActionRef.action;
                selectAction = selectActionRef.action;

                selectAction.performed += ctx => OnSelectAction();
            }

            private void Start()
            {
                Debug.Log("Map - Start");
                
                int index = GameInstance.MapNodeIndex;
                Node node = map.Nodes[index];

                player.transform.position = node.transform.position;
                canInteract = false;
                
                Debug.Log($"Map - Current Node: {node.name}");
                Debug.Log($"Map - Player to {player.transform.position}");
                Debug.Log("Map - Ready");
                GameController.Instance.MapReady();
            }

            public void StartMap()
            {
                canInteract = true;
            }

            private void OnSelectAction()
            {
                if (!canInteract)
                {
                    return;
                }

                Debug.Log($"Map - Select Action");
                
                camera ??= Camera.main;

                if (camera)
                {
                    Ray ray = camera.ScreenPointToRay(cursorAction.ReadValue<Vector2>());
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.collider.TryGetComponent(out Node node))
                        {
                            Node currentNode = map.Nodes[GameInstance.MapNodeIndex];
                            if (currentNode == node)
                            {
                                return;
                            }

                            if (!currentNode.Connections.Contains(node))
                            {
                                return;
                            }

                            MoveToNode(node);
                        }
                    }
                }
            }

            private void MoveToNode(Node node)
            {
                Debug.Log($"Map - Move to {node.name}", node.gameObject);
                
                canInteract = false;

                player.transform.DOMove(node.transform.position, 0.5f)
                      .OnComplete(OnFinishMoving);
                int index = map.Nodes.IndexOf(node);
                GameInstance.MapNodeIndex = index;
            }

            private void OnFinishMoving()
            {
                Debug.Log($"Map - Finished move");
                canInteract = false;
                Node node = map.Nodes[GameInstance.MapNodeIndex];
                Debug.Log($"Map - Node {node.NodeType}");
                switch (node.NodeType)
                {
                    case NodeType.None:
                        break;
                    case NodeType.Battle:
                    case NodeType.Boss:
                    case NodeType.MiniBoss:
                        GameController.Instance.LoadBattle();
                        break;
                    case NodeType.Blacksmith:
                        GameController.Instance.LoadBlacksmith();
                        break;
                    case NodeType.Inn:
                        // GameController.Instance.LoadInn();
                        break;
                    case NodeType.Shop:
                        GameController.Instance.LoadShop();
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
