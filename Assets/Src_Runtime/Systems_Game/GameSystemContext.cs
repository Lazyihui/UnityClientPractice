using System;


namespace Game_Client {

    public class GameSystemContext {
        public bool isRunning;
        public GameEntity gameEntity;

        // Inject
        public AssetsModule assetsModule;
        public InputModule inputModule;

        // Repos
        public RoleRepository RoleRepository;

        public GameSystemContext() {
            isRunning = false;
            gameEntity = new GameEntity();

            RoleRepository = new RoleRepository();
        }

        public void Inject(AssetsModule assetsModule, InputModule inputModule) {
            this.assetsModule = assetsModule;
            this.inputModule = inputModule;
        }

        public RoleEntity GetOwner() {
            RoleRepository.TryGet(gameEntity.OwnerIDsig, out RoleEntity role);
            return role;
        }
    }
}