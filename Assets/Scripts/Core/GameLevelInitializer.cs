using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Updater;
using InputReader;
using Items;
using Items.Data;
using Items.Rarity;
using Items.Storage;
using Player;
using UnityEngine;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private GameUIInputView _gameUIInput;
        [SerializeField] private ItemRarityDescriptorsStorage _rarityDescriptorsStorage;
        [SerializeField] private LayerMask _whatIsPlayer;
        [SerializeField] private ItemsStorage _itemsStorage;


        private ExternalDevicesInputReader _externalDevicesInput;
        private PlayerSystem _playerSystem;

        private DropGenerator _dropGenerator;
        private ItemsSystem _itemsSystem;
   
        
        private ProjectUpdater _projectUpdater;
        
        private List<IDisposable> _disposables;

        private bool _onPause;

        private void Awake()
        {
            _disposables = new List<IDisposable>();
            
            if (ProjectUpdater.Instance == null)
                _projectUpdater = new GameObject().AddComponent<ProjectUpdater>();
            else
                _projectUpdater = ProjectUpdater.Instance as ProjectUpdater;
            
            _externalDevicesInput = new ExternalDevicesInputReader();
            _disposables.Add(_externalDevicesInput);
            
            _playerSystem = new PlayerSystem(_playerEntity, new List<IEntityInputSource> 
            { 
                _externalDevicesInput,
                _gameUIInput
            });
            _disposables.Add(_playerSystem);

            ItemsFactory itemsFactory = new ItemsFactory(_playerSystem.StatsController);
            List<IItemRarityColor> rarityColors =
                _rarityDescriptorsStorage.RarityDescriptors.Cast<IItemRarityColor>().ToList();
            _itemsSystem = new ItemsSystem(rarityColors, _whatIsPlayer, itemsFactory);
            List<ItemDescriptor> descriptors =
                _itemsStorage.ItemScriptables.Select(scriptable => scriptable.ItemDescriptor).ToList();
            _dropGenerator = new DropGenerator(descriptors,_playerEntity, _itemsSystem);
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                _projectUpdater.IsPaused = !_projectUpdater.IsPaused;
        }

        private void OnDestroy()
        {
            foreach (var disposiable in _disposables)
            {
                disposiable.Dispose();
            }
        }
    }
}