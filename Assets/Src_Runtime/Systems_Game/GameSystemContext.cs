using System;
using Telepathy;


namespace Game_Client {

    public class GameSystemContext {
        public bool isRunning;
        public GameEntity gameEntity;
        public IDServer iDServer;

        public string roleName;


        // Inject
        public AssetsModule assetsModule;
        public InputModule inputModule;

        public Client client;

        // Repos
        public RoleRepository RoleRepository;

        public GameSystemContext() {
            isRunning = false;
            gameEntity = new GameEntity();
            iDServer = new IDServer();

            RoleRepository = new RoleRepository();
        }

        public void Inject(AssetsModule assetsModule, InputModule inputModule, Client client) {
            this.assetsModule = assetsModule;
            this.inputModule = inputModule;
            this.client = client;
        }

        public RoleEntity GetOwner() {
            RoleRepository.TryGet(gameEntity.OwnerIDsig, out RoleEntity role);
            return role;
        }
    }
}