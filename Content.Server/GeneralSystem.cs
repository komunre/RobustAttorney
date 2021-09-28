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

        private GameState _gameState;
        private MapId _map;
        private uint _playersJoined = 0;
        public override void Initialize()
        {
            _playerManager.PlayerStatusChanged += PlayerStatusChanged;
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
