using System;
using DG.Tweening;
using Fsi.Gameplay;
using ProjectYahtzee.Maps.Nodes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectYahtzee.Maps
    {
        public class MapController : MbSingleton<MapController>
        {
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

            private bool isMoving = false;

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
                
                int index = GameController.Instance.GameInstance.CurrentMapIndex;
                Node node = map.Nodes[index];

                player.transform.position = node.transform.position;
                isMoving = false;
                
                Debug.Log($"Map - Current Node: {node.name}");
                Debug.Log($"Map - Player to {player.transform.position}");
            }

            private void OnSelectAction()
            {
                if (isMoving)
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
                            Node currentNode = map.Nodes[GameController.Instance.GameInstance.CurrentMapIndex];
                            if (currentNode == node)
                            {
                                return;
                            }

                            if (!currentNode.Connections.Contains(node))
                            {
                                return;
                            }
                            
                            Debug.Log($"Map - Move to {node.name}", node.gameObject);

                            player.transform.DOMove(node.transform.position, 0.5f)
                                  .OnComplete(OnFinishMoving);
                            int index = map.Nodes.IndexOf(node);
                            GameController.Instance.GameInstance.CurrentMapIndex = index;
                        }
                    }
                }
            }

            private void OnFinishMoving()
            {
                Debug.Log($"Map - Finished move");
                isMoving = false;
                Node node = map.Nodes[GameController.Instance.GameInstance.CurrentMapIndex];
                Debug.Log($"Map - Node {node.NodeType}");
                switch (node.NodeType)
                {
                    case NodeType.None:
                        break;
                    case NodeType.Battle:
                        ProjectSceneManager.Instance.LoadBattle();
                        break;
                    case NodeType.Blacksmith:
                        ProjectSceneManager.Instance.LoadBlacksmith();
                        break;
                    case NodeType.Inn:
                        // ProjectSceneManager.Instance.LoadInn();
                        break;
                    case NodeType.Shop:
                        // ProjectSceneManager.Instance.LoadShop();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
