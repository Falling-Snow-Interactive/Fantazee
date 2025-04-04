using System;
using DG.Tweening;
using Fantazee.Audio;
using Fantazee.Battle.Characters;
using Fantazee.Instance;
using Fantazee.Maps.Nodes;
using FMODUnity;
using Fsi.Gameplay;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
using Spline = UnityEngine.Splines.Spline;

namespace Fantazee.Maps
    {
        public class MapController : MbSingleton<MapController>
        {
            private EnvironmentInstance Environment => GameInstance.Current.Environment;

            [SerializeReference]
            private GameplayCharacterVisuals player;

            [Header("Input")]

            [SerializeField]
            private InputActionReference cursorActionRef;
            private InputAction cursorAction;

            [SerializeField]
            private InputActionReference selectActionRef;
            private InputAction selectAction;

            [SerializeField]
            private InputActionReference nextActionRef;
            private InputAction nextAction;
            
            [SerializeField]
            private InputActionReference previousActionRef;
            private InputAction previousAction;
            
            [SerializeField]
            private InputActionReference selectNodeActionRef;
            private InputAction selectNode;

            private bool canInteract = false;
            
            [Header("Prefabs")]
            
            [SerializeField]
            private ConnectionLine connectionLinePrefab;

            [Header("Animation")]

            [Header("Move")]

            [SerializeField]
            private float moveTime = 1f;
            
            [SerializeField]
            private Ease moveEase = Ease.Linear;
            
            [Header("Audio")]

            [SerializeField]
            private EventReference mapStartSfx;
            
            [SerializeField]
            private EventReference mapEndSfx;

            [SerializeField]
            private EventReference nodeSelectSfxRef;
            
            [Header("References")]
            
            [SerializeField]
            private new Camera camera;
            
            [SerializeField]
            private Map map;

            [SerializeField]
            private Transform playerSocket;
            
            // Selection
            private int selectIndex = 0;
            
            [CanBeNull]
            private Node selectedNode;

            protected override void Awake()
            {
                base.Awake();
                
                cursorAction = cursorActionRef.action;
                selectAction = selectActionRef.action;

                selectAction.performed += ctx => OnSelectAction();
                cursorAction.performed += ctx => OnCursor();

                nextAction = nextActionRef.ToInputAction();
                previousAction = previousActionRef.ToInputAction();
                selectNode = selectNodeActionRef.ToInputAction();

                nextAction.performed += ctx => NextNode();
                previousAction.performed += ctx => PrevNode();
                selectNode.performed += ctx => SelectNode();
            }

            private void OnEnable()
            {
                cursorAction.Enable();
                selectAction.Enable();
                
                nextAction.Enable();
                previousAction.Enable();
                selectNode.Enable();
            }

            private void OnDisable()
            {
                cursorAction.Disable();
                selectAction.Disable();
                
                nextAction.Disable();
                previousAction.Disable();
                selectNode.Disable();
            }

            private void Start()
            {
                Debug.Log("Map - Start");

                Node node = map.Nodes[Environment.Node];
                if (Environment.ReadyToAdvance)
                {
                    node = map.Nodes[^1];
                }

                player = Instantiate(GameInstance.Current.Character.Data.Visuals, playerSocket);
                player.transform.position = node.transform.position;
                canInteract = false;
                
                RuntimeManager.PlayOneShot(mapStartSfx);
                MusicController.Instance.PlayMusic(GameInstance.Current.Environment.Data.GeneralMusic);

                if (node.Next.Count > selectIndex)
                {
                    node.Next[0].SetHighlight(true);
                    selectedNode = node.Next[0];
                }
                
                Debug.Log($"Map - Current Node: {node.transform.position}");
                Debug.Log($"Map - Player to {player.transform.position}");
                Debug.Log("Map - Ready");
                GameController.Instance.MapReady();
            }

            public void StartMap()
            {
                canInteract = !Environment.ReadyToAdvance;

                if (Environment.ReadyToAdvance)
                {
                    AdvanceToNextMap();
                }
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
                        if (hit.collider.TryGetComponent(out Node clickedNode))
                        {
                            RuntimeManager.PlayOneShot(nodeSelectSfxRef);
                            Node currentNode = map.Nodes[Environment.Node];

                            if (currentNode.Next.Contains(clickedNode))
                            {
                                MoveToNode(clickedNode);
                            }
                        }
                    }
                }
            }

            private void MoveToNode(Node node)
            {
                Debug.Log($"Map - Move to {node}", node.gameObject);
                
                canInteract = false;

                int curr = GameInstance.Current.Environment.Node;
                Node currentNode = map.Nodes[curr];
                int nextIndex = currentNode.Next.IndexOf(node);
                Spline spline = currentNode.SplineContainer.Splines[nextIndex];
                
                float t = 0;
                DOTween.To(() => t, x =>
                                    {
                                        t = x;
                                        float3 p = spline.EvaluatePosition(t);
                                        player.transform.position = new Vector3(p.x, p.y, p.z) 
                                                                    + currentNode.transform.position;
                                    }, 
                           1, 
                           moveTime)
                       .SetEase(moveEase)
                       .OnComplete(() =>
                                   {
                                       float3 p = spline.EvaluatePosition(1f);
                                       player.transform.position = new Vector3(p.x, p.y, p.z)
                                           + currentNode.transform.position;
                                       OnFinishMoving(node);
                                   });

            }

            private void OnFinishMoving(Node node)
            {
                Debug.Log($"Map - Finished move");
                canInteract = true;
                Environment.Node = map.Nodes.IndexOf(node);
                Debug.Log($"Map - Node {node.Type} [{Environment.Node}]");
                RuntimeManager.PlayOneShot(mapEndSfx);
                
                switch (node.Type)
                {
                    case NodeType.None:
                        break;
                    case NodeType.Battle:
                    case NodeType.MiniBoss:
                        GameController.Instance.LoadBattle();
                        break;
                    case NodeType.Boss:
                        GameController.Instance.LoadBossBattle();
                        break;
                    case NodeType.Blacksmith:
                        GameController.Instance.LoadBlacksmith();
                        break;
                    case NodeType.Inn:
                        GameController.Instance.LoadInn();
                        break;
                    case NodeType.Shop:
                        GameController.Instance.LoadShop();
                        break;
                    case NodeType.Encounter:
                        GameController.Instance.LoadEncounter();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            private void AdvanceToNextMap()
            {
                Debug.Log("Map - Advance to next map");
                player.transform.position = map.Nodes[^1].transform.position;
                player.transform.DOMove(map.Nodes[^1].transform.position + Vector3.right * 10f, 0.5f)
                      .SetEase(Ease.InSine)
                      .SetDelay(0.5f)
                      .SetLink(gameObject, LinkBehaviour.CompleteAndKillOnDisable)
                      .OnComplete(() =>
                                  {
                                      GameController.Instance.AdvanceMap(StartNewMap);
                                  });
            }

            private void StartNewMap()
            {
                player.transform.position = map.Nodes[0].transform.position + Vector3.right * -10;
                player.transform.DOLocalMoveX(map.Nodes[0].transform.position.x, 0.5f)
                      .SetEase(Ease.InSine)
                      .SetDelay(0.5f)
                      .SetLink(gameObject, LinkBehaviour.CompleteAndKillOnDisable)
                      .OnComplete(StartMap); 
            }
            
            private void OnCursor()
            {
                camera ??= Camera.main;

                selectedNode?.SetHighlight(false);
                if (camera)
                {
                    Ray ray = camera.ScreenPointToRay(cursorAction.ReadValue<Vector2>());
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.collider.TryGetComponent(out Node highlightNode))
                        {
                            Node currentNode = map.Nodes[Environment.Node];
                            if (currentNode.Next.Contains(highlightNode))
                            {
                                
                                selectedNode = highlightNode;
                            }
                        }
                    }
                }
                selectedNode?.SetHighlight(true);
            }

            private void NextNode()
            {
                var currentNode = map.Nodes[Environment.Node];
                
                selectIndex++;
                selectIndex %= currentNode.Next.Count;
                
                selectedNode?.SetHighlight(false);
                selectedNode = currentNode.Next[selectIndex];
                selectedNode?.SetHighlight(true);
            }

            private void PrevNode()
            {
                var currentNode = map.Nodes[Environment.Node];
                
                selectIndex--;
                if (selectIndex < 0)
                {
                    selectIndex += currentNode.Next.Count;
                }
                
                selectedNode?.SetHighlight(false);
                selectedNode = currentNode.Next[selectIndex];
                selectedNode?.SetHighlight(true);
            }

            private void SelectNode()
            {
                if (selectedNode)
                {
                    RuntimeManager.PlayOneShot(nodeSelectSfxRef);
                    Node currentNode = map.Nodes[Environment.Node];

                    if (currentNode.Next.Contains(selectedNode))
                    {
                        MoveToNode(selectedNode);
                    }
                }
            }
        }
    }
