using System;
using DG.Tweening;
using Fantazee.Audio;
using Fantazee.Audio.Information;
using Fantazee.Audio.Settings;
using Fantazee.Battle.Characters;
using Fantazee.Environments.Information;
using Fantazee.Environments.Settings;
using Fantazee.Instance;
using Fantazee.Maps.Nodes;
using FMOD.Studio;
using FMODUnity;
using Fsi.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Fantazee.Maps
    {
        public class MapController : MbSingleton<MapController>
        {
            private MapInstance Map => GameInstance.Current.Map;

            [SerializeReference]
            private GameplayCharacterVisuals player;

            [Header("Input")]

            [SerializeField]
            private InputActionReference cursorActionRef;
            private InputAction cursorAction;

            [SerializeField]
            private InputActionReference selectActionRef;
            private InputAction selectAction;

            private bool canInteract = false;

            [Header("Audio")]

            [SerializeField]
            private EventReference mapStartSfx;
            
            [SerializeField]
            private EventReference mapEndSfx;

            [SerializeField]
            private EventReference footstepsSfxRef;
            private EventInstance footstepsSfx;

            [SerializeField]
            private EventReference nodeSelectSfxRef;
            
            [Header("References")]
            
            [SerializeField]
            private new Camera camera;
            
            [SerializeField]
            private Map map;

            [SerializeField]
            private Transform playerSocket;

            protected override void Awake()
            {
                base.Awake();
                
                cursorAction = cursorActionRef.action;
                selectAction = selectActionRef.action;

                selectAction.performed += ctx => OnSelectAction();
                
                footstepsSfx = RuntimeManager.CreateInstance(footstepsSfxRef);
            }

            private void OnEnable()
            {
                cursorAction.Enable();
                selectAction.Enable();
            }

            private void OnDisable()
            {
                cursorAction.Disable();
                selectAction.Disable();
            }

            private void Start()
            {
                Debug.Log("Map - Start");

                Node node = map.Nodes[Map.Node];
                if (Map.ReadyToAdvance)
                {
                    node = map.Nodes[^1];
                }

                player = Instantiate(GameInstance.Current.Character.Data.Visuals, transform);
                player.transform.position = node.transform.position;
                canInteract = false;
                
                RuntimeManager.PlayOneShot(mapStartSfx);

                if (EnvironmentSettings.Settings.Information.TryGetInformation(GameInstance.Current.Map.Environment,
                                                                               out EnvironmentInformation info))
                {
                    MusicController.Instance.PlayMusic(info.MapMusicId);
                }
                
                Debug.Log($"Map - Current Node: {node.name}");
                Debug.Log($"Map - Player to {player.transform.position}");
                Debug.Log("Map - Ready");
                GameController.Instance.MapReady();
            }

            public void StartMap()
            {
                canInteract = !Map.ReadyToAdvance;

                if (Map.ReadyToAdvance)
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
                        if (hit.collider.TryGetComponent(out Node node))
                        {
                            RuntimeManager.PlayOneShot(nodeSelectSfxRef);
                            Node currentNode = map.Nodes[Map.Node];
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

                footstepsSfx.start();
                player.transform.DOMove(node.transform.position, 0.5f)
                      .SetLink(gameObject, LinkBehaviour.CompleteAndKillOnDisable)
                      .OnComplete(() =>
                                  {
                                      footstepsSfx.stop(STOP_MODE.IMMEDIATE);
                                      OnFinishMoving(node);
                                  });
            }

            private void OnFinishMoving(Node node)
            {
                Debug.Log($"Map - Finished move");
                canInteract = true;
                Map.Node = map.Nodes.IndexOf(node);
                Debug.Log($"Map - Node {node.NodeType} [{Map.Node}]");
                RuntimeManager.PlayOneShot(mapEndSfx);
                switch (node.NodeType)
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
                        // GameController.Instance.LoadInn();
                        break;
                    case NodeType.Shop:
                        GameController.Instance.LoadShop();
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
        }
    }
