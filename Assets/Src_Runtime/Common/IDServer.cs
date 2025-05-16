using System;

namespace Game_Client {

    public class IDServer {
        public int roleID;

        public IDServer() {
            roleID = 1;
        }

        public int PickRoleID() {
            return ++roleID;
        }
    }
}