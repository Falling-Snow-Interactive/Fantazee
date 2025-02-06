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
                int index = GameController.Instance.GameInstance.CurrentMapIndex;
                Node node = map.Nodes[index];

                player.transform.position = node.transform.position;
                isMoving = false;
            }

            private void OnSelectAction()
            {
                if (isMoving)
                {
                    return;
                }
                
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
                Node node = map.Nodes[GameController.Instance.GameInstance.CurrentMapIndex];
                switch (node.NodeType)
                {
                    case NodeType.None:
                        isMoving = false;
                        break;
                    case NodeType.Battle:
                        ProjectSceneManager.Instance.LoadBattle();
                        break;
                    case NodeType.Blacksmith:
                        isMoving = false;
                        break;
                    case NodeType.Inn:
                        isMoving = false;
                        break;
                    case NodeType.Shop:
                        isMoving = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
