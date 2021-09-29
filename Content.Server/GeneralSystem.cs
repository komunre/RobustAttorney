using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Server.Player;
using Robust.Shared.Player;
using Robust.Shared.Enums;
using Robust.Shared.Map;
using Robust.Shared.Maths;
using Content.Shared.Player;
using Robust.Shared.ContentPack;
using Robust.Shared.Random;

namespace Content.Server
{
    public enum GameState
    {
        Start,
        InProgress,
        End,
    }
    class GeneralSystem : EntitySystem
    {
        [Dependency] private readonly IPlayerManager _playerManager = default!;
        [Dependency] private readonly IMapManager _mapManager = default!;
        [Dependency] private readonly IResourceManager _resManager = default!;
        [Dependency] private readonly IRobustRandom _random = default!;

        private GameState _gameState;
        private MapId _map;
        private uint _playersJoined = 0;
        private string[] _names;
        public override void Initialize()
        {
            _playerManager.PlayerStatusChanged += PlayerStatusChanged;
            _names = _resManager.ContentFileReadAllText("/names.txt").Split("\n");
        }

        private void PlayerStatusChanged(object sender, SessionStatusEventArgs e)
        {
            if (e.NewStatus == SessionStatus.Connected)
            {
                e.Session.JoinGame();
            }

            if (e.NewStatus == SessionStatus.InGame)
            {
                var attorney = EntityManager.SpawnEntity(null, new MapCoordinates(Vector2.Zero, _map));
                var comp = attorney.AddComponent<AttorneyComponent>();
                comp.NetSyncEnabled = true;
                comp.AttorneyName = _names[_random.Next(_names.Length - 1)];
                comp.Color = new Color(_random.NextFloat(1), _random.NextFloat(1), _random.NextFloat(1));
                e.Session.AttachToEntity(attorney);
                _playersJoined++;
                if (_playersJoined % 2 == 0)
                {
                    comp.Defense = true;
                }
            }
        }

        public override void Update(float frameTime)
        {
            switch (_gameState)
            {
                case GameState.Start:
                    _map = _mapManager.CreateMap();
                    _gameState = GameState.InProgress;
                    break;
                case GameState.End:
                    _gameState = GameState.Start;
                    break;
            }
        }
    }
}
