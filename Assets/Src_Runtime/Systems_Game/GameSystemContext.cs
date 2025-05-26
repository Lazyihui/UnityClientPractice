using System;
using System.Collections.Generic;
using Telepathy;
using UnityEngine;
using MyTelepathy;


namespace Game_Client {

    public class GameSystemContext {
        public bool isRunning;
        public GameEntity gameEntity;
        public IDServer iDServer;

        public string roleName;

        public float lastMoveTimestamp;// 上次移动的时间戳
        public Vector2 localPlayerPos; // 本地玩家位置


        // Inject
        public AssetsModule assetsModule;
        public InputModule inputModule;

        public Client client;

        // Repos
        public RoleRepository roleRepository;
        public BulletRepository bulletRepository;
        public StuffRepository stuffRepository;
        public GameSystemContext() {

            isRunning = false;
            gameEntity = new GameEntity();
            iDServer = new IDServer();

            roleRepository = new RoleRepository();
            bulletRepository = new BulletRepository();
            stuffRepository = new StuffRepository();
        }

        public void Inject(AssetsModule assetsModule, InputModule inputModule, Client client) {
            this.assetsModule = assetsModule;
            this.inputModule = inputModule;
            this.client = client;
        }

        public RoleEntity GetOwner() {
            bool has = roleRepository.TryGet(gameEntity.OwnerIDsig, out RoleEntity role);
            // int len = RoleRepository.TakeAll(out RoleEntity[] roles);
            // Debug.Log(len);
            if (!has) {
            }
            return role;
        }
    }
}