using System;


namespace Game_Client {

    public class GameSystemContext {
        public bool isRunning;
        public GameEntity gameEntity;

        // Inject
        public AssetsModule assetsModule;

        // Repos
        public RoleRepository RoleRepository;

        public GameSystemContext() {
            isRunning = false;
            gameEntity = new GameEntity();

            RoleRepository = new RoleRepository();
        }

        public void Inject(AssetsModule assetsModule) {
            this.assetsModule = assetsModule;
        }
    }
}