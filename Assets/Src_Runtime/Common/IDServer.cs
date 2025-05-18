using System;

namespace Game_Client {

    public class IDServer {
        public int roleID;

        public IDServer() {
            roleID = 0;
        }

        public int PickRoleID() {
            return ++roleID;
        }
    }
}