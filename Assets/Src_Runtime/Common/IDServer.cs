using System;

namespace Game_Client {

    public class IDServer {
        public int roleID;
        public int bulletID;

        public IDServer() {
            roleID = 0;
            bulletID = 0;
        }

        public int PickRoleID() {
            return ++roleID;
        }

        public int PickBulletID() {
            return ++bulletID;
        }
    }
}